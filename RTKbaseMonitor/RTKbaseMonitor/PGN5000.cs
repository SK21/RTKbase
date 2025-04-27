using System;
using System.Net;

namespace RTKbaseMonitor
{
    public class PGN5000
    {
        //PGN5000, RTKbase info
        //0     136
        //1     19
        //2     bytes sent 0
        //3     sent 1
        //4     sent 2
        //5     sent 3
        //6     Status
        //      bit 0 - connected
        //      bit 1 - RTCM message found
        //7     hangup count
        //8     bad packet
        //9     InoID lo
        //10    InoID hi
        //11    IP0
        //12    IP1
        //13    IP2
        //14    IP3
        //15    CRC

        private const byte cByteCount = 16;
        private const byte HeaderHi = 19;
        private const byte HeaderLo = 136;
        private byte cBadPacket;
        private UInt32 cBytesSent;
        private bool cConnected;
        private byte cHangupCount;
        private UInt16 cInoID;
        private byte[] cIPaddress;
        private bool cMessageFound;
        private DateTime LastReceived;
        private frmMonitor mf;
        private DateTime lastBytesCheck = DateTime.MinValue;
        private UInt32 lastBytesSent = 0;
        private double cSpeed = 0.0;

        public PGN5000(frmMonitor CalledFrom)
        {
            mf = CalledFrom;
            cIPaddress = new byte[4];
        }
        public double Speed
        {
            get { return cSpeed; }
        }


        public byte[] Address
        { get { return cIPaddress; } }

        public byte BadPacket
        { get { return cBadPacket; } }

        public bool BaseConnected
        { get { return (DateTime.Now - LastReceived).TotalSeconds < 4; } }

        public UInt32 BytesSent
        { get { return cBytesSent; } }

        public bool Connected
        { get { return cConnected && BaseConnected; } }

        public byte HangupCount
        { get { return cHangupCount; } }

        public UInt16 InoID
        { get { return cInoID; } }

        public bool MessageFound
        { get { return cMessageFound; } }

        public bool ParseByteData(byte[] data)
        {
            bool Result = false;

            if (data.Length >= cByteCount &&
                data[0] == HeaderLo &&
                data[1] == HeaderHi &&
                GoodCRC(data))
            {
                cBytesSent = (uint)(data[5] << 24 | data[4] << 16 | data[3] << 8 | data[2]);

                cConnected = ((data[6] & 0b00000001) == 0b00000001);
                cMessageFound = ((data[6] & 0b00000010) == 0b00000010);

                cHangupCount = data[7];
                cBadPacket = data[8];
                cInoID = (ushort)(data[10] << 8 | data[9]);

                for (int i = 0; i < 4; i++)
                {
                    cIPaddress[i] = data[i + 11];
                }
                LastReceived = DateTime.Now;

                if (lastBytesCheck != DateTime.MinValue)
                {
                    double elapsedSeconds = (LastReceived - lastBytesCheck).TotalSeconds;
                    if (elapsedSeconds > 0)
                    {
                        cSpeed = ((cBytesSent - lastBytesSent) * 8) / (elapsedSeconds * 1000.0);
                    }
                }
                lastBytesCheck = LastReceived;
                lastBytesSent = cBytesSent;

                Result = true;
            }
            return Result;
        }

        private byte CRC(byte[] Data, int Length, byte Start = 0)
        {
            byte Result = 0;
            if (Length <= Data.Length)
            {
                int CK = 0;
                for (int i = Start; i < Length; i++)
                {
                    CK += Data[i];
                }
                Result = (byte)CK;
            }
            return Result;
        }

        private bool GoodCRC(byte[] Data, byte Start = 0)
        {
            bool Result = false;
            int Length = Data.Length;
            byte cr = CRC(Data, Length - 1, Start);
            Result = (cr == Data[Length - 1]);
            return Result;
        }
    }
}