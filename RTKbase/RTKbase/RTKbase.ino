#include <NativeEthernet.h>
#include <NativeEthernetUdp.h>

#include "FXUtil.h"		// read_ascii_line(), hex file support
extern "C" {
#include "FlashTxx.h"		// TLC/T3x/T4x/TMM flash primitives
}

#include "arduino_secrets.h"

#define InoDescription "RTKbase   25-Apr-2025"
const uint16_t InoID = 25045;	// change to send defaults to eeprom, ddmmy, no leading 0
const uint8_t InoType = 7;		// 0 - Teensy AutoSteer, 1 - Teensy Rate, 2 - Nano Rate, 3 - Nano SwitchBox, 4 - ESP Rate, 7 RTKbase

const char* ntripServer = "rtkdata.online"; 
const int   ntripPort = 2101;                    
const char* ntripMountPoint = Secret_MountPoint;
const char* casterPassword = Secret_Password;

byte mac[] = { 0xDE, 0xAD, 0xBE, 0xEF, 0xFE, 0xED };
EthernetClient ntripClient;

unsigned long lastConnectionAttempt = 0;
const unsigned long connectionInterval = 5000;  // Reattempt connection every 5 seconds

uint32_t lastSentRTCM_ms = 0;           //Time of last data pushed to socket
uint16_t maxTimeBeforeHangup_ms = 10000; //If we fail to get a complete RTCM frame after 10s, then disconnect from caster

uint32_t BytesSent = 0; //Just a running total
uint32_t lastReport_ms = 0;       //Time of last report of bytes sent

// ethernet comm
EthernetUDP UDPcomm;
uint16_t ListeningPort = 5710;
uint16_t DestinationPort = 5350;
IPAddress DestinationIP(192,168,1,255);

const uint16_t SendTime = 1000;
uint32_t SendLast = SendTime;

// firmware update
EthernetUDP UpdateComm;
uint16_t UpdateReceivePort = 29100;
uint16_t UpdateSendPort = 29000;
uint32_t buffer_addr, buffer_size;
bool UpdateMode = false;

//******************************************************************************
// hex_info_t struct for hex record and hex file info
//******************************************************************************
typedef struct {  //
    char* data;   // pointer to array allocated elsewhere
    unsigned int addr;  // address in intel hex record
    unsigned int code;  // intel hex record type (0=data, etc.)
    unsigned int num; // number of data bytes in intel hex record

    uint32_t base;  // base address to be added to intel hex 16-bit addr
    uint32_t min;   // min address in hex file
    uint32_t max;   // max address in hex file

    int eof;    // set true on intel hex EOF (code = 1)
    int lines;    // number of hex records received
} hex_info_t;

static char data[16];// buffer for hex data

hex_info_t hex =
{ // intel hex info struct
  data, 0, 0, 0,        //   data,addr,num,code
  0, 0xFFFFFFFF, 0,     //   base,min,max,
  0, 0					//   eof,lines
};

void ConnectCaster()
{
    if (ntripClient.connected() == false)
    {
        Serial.printf("Opening socket to %s\n", ntripServer);

        if (ntripClient.connect(ntripServer, ntripPort) == true) //Attempt connection
        {
            Serial.printf("Connected to %s:%d\n", ntripServer, ntripPort);

            const int SERVER_BUFFER_SIZE = 512;
            char serverRequest[SERVER_BUFFER_SIZE];

            snprintf(serverRequest,
                SERVER_BUFFER_SIZE,
                "SOURCE %s /%s\r\nSource-Agent: Teensy NTRIP\r\n\r\n",
                casterPassword, ntripMountPoint);

            Serial.println(F("Sending server request:"));
            Serial.println(serverRequest);
            ntripClient.write(serverRequest, strlen(serverRequest));

            //Wait for response
            unsigned long timeout = millis();
            while (ntripClient.available() == 0)
            {
                if (millis() - timeout > 5000)
                {
                    Serial.println("Caster timed out!");
                    ntripClient.stop();
                    return;
                }
                delay(10);
            }

            //Check reply
            bool connectionSuccess = false;
            char response[512];
            int responseSpot = 0;
            while (ntripClient.available())
            {
                response[responseSpot++] = ntripClient.read();
                if (strstr(response, "200") != nullptr) //Look for 'ICY 200 OK'
                    connectionSuccess = true;
                if (responseSpot == 512 - 1)
                    break;
            }
            response[responseSpot] = '\0';

            if (connectionSuccess)
            {
                Serial.println("Connected to Caster");
            }
            else
            {
                Serial.printf("Failed to connect to Caster: %s", response);
                return;
            }
        } //End attempt to connect
        else
        {
            Serial.println("Connection to host failed");
            return;
        }
    } //End connected == false

    lastConnectionAttempt = millis();
}

String ipAddressToString(IPAddress ip)
{
    return String(ip[0]) + "." + String(ip[1]) + "." + String(ip[2]) + "." + String(ip[3]);
}

uint16_t HangUpCount = 0;

void setup() 
{
    Serial.begin(38400);
    delay(5000);
    Serial.println();
    Serial.println(InoDescription);
    Serial.println();

    Serial.print("Initializing Ethernet...");
    if (Ethernet.begin(mac) == 0)
    {
        Serial.println("DHCP failed.");
    }
    delay(1000);

    Serial.print("IP Address: ");
    Serial.println(ipAddressToString(Ethernet.localIP()));

    DestinationIP = IPAddress(Ethernet.localIP()[0], Ethernet.localIP()[1], Ethernet.localIP()[2], 255);

    UDPcomm.begin(ListeningPort);
    UpdateComm.begin(UpdateReceivePort);

    Serial8.begin(115200);

    Serial.println("Finished Setup.");
}

void loop()
{
    if (!ntripClient.connected())
    {
        if (millis() - lastConnectionAttempt > connectionInterval) ConnectCaster();
        lastSentRTCM_ms = millis();
    }
    else 
    {
        delay(10);
        while (Serial.available())
            Serial.read(); //Flush any endlines or carriage returns

        while (Serial8.available())
        {
            char rtcmByte = Serial8.read();
            if (ntripClient.connected())
            {
                ntripClient.write(rtcmByte);
                BytesSent++;
                lastSentRTCM_ms = millis();
            }
        }

        if (millis() - lastReport_ms > 1000)
        {
            lastReport_ms = millis();
            Serial.printf("Total sent: %d\n", BytesSent);
        }

        if (millis() - lastSentRTCM_ms > maxTimeBeforeHangup_ms)
        {
            Serial.println("RTCM timeout. Disconnecting...");
            ntripClient.stop();
            HangUpCount++;
        }
    }
    ReceiveUpdate();
    SendComm();
}

bool GoodCRC(byte Data[], byte Length)
{
    byte ck = CRC(Data, Length - 1, 0);
    bool Result = (ck == Data[Length - 1]);
    return Result;
}

byte CRC(byte Chk[], byte Length, byte Start)
{
    byte Result = 0;
    int CK = 0;
    for (int i = Start; i < Length; i++)
    {
        CK += Chk[i];
    }
    Result = (byte)CK;
    return Result;
}
