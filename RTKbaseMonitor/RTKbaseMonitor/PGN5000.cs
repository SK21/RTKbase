using System;

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
        //7     hangup count lo
        //8     hangup count hi
        //9     InoID lo
        //10    InoID hi
        //11    CRC

        private const byte cByteCount = 12;
        private const byte HeaderHi = 19;
        private const byte HeaderLo = 136;
        private UInt32 cBytesSent;
        private bool cConnected;
        private UInt16 cHangupCount;
        private UInt16 cInoID;
        private frmMonitor mf;

        public PGN5000(frmMonitor CalledFrom)
        {
            mf = CalledFrom;
        }

        public UInt32 BytesSent
        { get { return cBytesSent; } }
        public bool Connected
        { get { return cConnected; } }
        public UInt16 HangupCount
        { get { return cHangupCount; } }
        public UInt16 InoID
        { get { return cInoID; } } 

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
                cHangupCount = (ushort)(data[8] << 8 | data[7]);
                cInoID = (ushort)(data[10] << 8 | data[9]);
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