using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using S7.Net;

namespace MySCADA
{
    public class PLC
    {
        public string IPAddress = "192.168.1.201";
        System.Timers.Timer ReadPLCTimer = new System.Timers.Timer();
        Plc thePLC;
        public Device Motor_1 = new Device();
        public Device Motor_2 = new Device();
        public Device Valve = new Device();
        public SCADA Parent;

        public PLC()
        {
            thePLC = new Plc(CpuType.S71500, IPAddress, 0, 1);
            try
            {
                thePLC.Open();
            }
            catch
            {
                ;
            }
            ReadPLCTimer.Interval = 1000;
            ReadPLCTimer.Elapsed += new System.Timers.ElapsedEventHandler(ReadPLCTimer_Tick);
            ReadPLCTimer.Enabled = true;
            ReadPLCTimer.Start();
        }

        private void ReadPLCTimer_Tick(object sender, EventArgs e)
        {
            if (thePLC.IsConnected)
            {
                thePLC.ReadClass(Motor_1, 1);
                thePLC.ReadClass(Motor_2, 2);
                thePLC.ReadClass(Valve, 3);
            }
        }
        public void WriteBool(string address, bool value)
        {
            thePLC.Write(address, value);
        }
        public void WriteInt(string address, short value)
        {
            thePLC.Write(address, value);
        }
    }

    public class Device
    {
        public ushort Mode { get; set; } // "Mode"
        public bool Start { get; set; } // "Start"
        public bool Stop { get; set; }  // "Stop"
        public bool RunCond { get; set; }
        public bool StopCond { get; set; }
        public bool RunFeedback { get; set; }
        public bool Reset { get; set; }
        public byte aByte { get; set; }
        public bool Cmd { get; set; }
        public bool Fault { get; set; }
    }

}