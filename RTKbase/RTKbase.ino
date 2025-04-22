#include <NativeEthernet.h>
#include <NativeEthernetUdp.h>

const char* ntripServer = "rtkdata.online"; 
const int   ntripPort = 2101;                    
const char* ntripMountPoint = "mountpoint";      
const char* casterPassword = "password";

byte mac[] = { 0xDE, 0xAD, 0xBE, 0xEF, 0xFE, 0xED };
EthernetClient ntripClient;

unsigned long lastConnectionAttempt = 0;
const unsigned long connectionInterval = 5000;  // Reattempt connection every 5 seconds

uint32_t lastSentRTCM_ms = 0;           //Time of last data pushed to socket
int maxTimeBeforeHangup_ms = 10000; //If we fail to get a complete RTCM frame after 10s, then disconnect from caster

uint32_t serverBytesSent = 0; //Just a running total
uint32_t lastReport_ms = 0;       //Time of last report of bytes sent

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

void setup() 
{
    Serial.begin(38400);

    Serial.println("Starting Teensy 4.1 NTRIP Base Station with Ethernet");

    Serial.print("Initializing Ethernet...");
    if (Ethernet.begin(mac) == 0)
    {
        Serial.println("DHCP failed.");
    }
    delay(1000);

    Serial.print("IP Address: ");
    Serial.println(ipAddressToString(Ethernet.localIP()));

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
                serverBytesSent++;
                lastSentRTCM_ms = millis();
            }
        }

        if (millis() - lastReport_ms > 1000)
        {
            lastReport_ms = millis();
            Serial.printf("Total sent: %d\n", serverBytesSent);
        }

        if (millis() - lastSentRTCM_ms > maxTimeBeforeHangup_ms)
        {
            Serial.println("RTCM timeout. Disconnecting...");
            ntripClient.stop();
        }
    }
}