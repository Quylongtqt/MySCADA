using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Modbus.Device;

namespace MySCADA
{
    public class PLC_Modbus
    {
        public string IPAddress = "192.168.1.200";
        System.Timers.Timer ReadPLCTimer = new System.Timers.Timer();
        ModbusTcpDevice thePLC;
        public int Level;
        public bool Status;
        public bool isConnected;

        public SCADA Parent;

        public PLC_Modbus()
        {
            thePLC = new ModbusTcpDevice(IPAddress);
            thePLC.Connect();
            isConnected = true;

            ReadPLCTimer.Interval = 1000;
            ReadPLCTimer.Elapsed += new System.Timers.ElapsedEventHandler(ReadPLCTimer_Tick);
            ReadPLCTimer.Enabled = true;
            ReadPLCTimer.Start();
        }

        private void ReadPLCTimer_Tick(object sender, EventArgs e)
        {
            if (isConnected)
            {
                Level = thePLC.ReadWord(255, 99);
                Console.WriteLine($"Level = {Level}");
                Status = thePLC.ReadBoolean(255, 99);
                Console.WriteLine($"Status = {Status}");
            }
        }
        public void WriteInt(byte slaveID, ushort address, ushort value)
        {
            if (isConnected)
            {
                thePLC.WriteWord(slaveID, address, value);
            }
        }
        public void WriteBool(byte slaveID, ushort address, bool value)
        {
            if (isConnected)
            {
                thePLC.WriteBoolean(slaveID, address, value);
            }
        }
        
    }

    //public class Actuator
    //{
    //    public ushort Mode { get; set; } // "Mode"
    //    public bool Start { get; set; } // "Start"
    //    public bool Stop { get; set; }  // "Stop"
    //    public bool RunCond { get; set; }
    //    public bool StopCond { get; set; }
    //    public bool RunFeedback { get; set; }
    //    public bool Reset { get; set; }
    //    public byte aByte { get; set; }
    //    public bool Cmd { get; set; }
    //    public bool Fault { get; set; }
    //    public byte aByte2 { get; set; }
    //    public ushort Pos { get; set; }
    //}
    public class ModbusTcpDevice
    {
        ModbusIpMaster master = null;
        System.Net.Sockets.TcpClient client = null;
        string Address = "127.0.0.1";

        public ModbusTcpDevice(string address)
        {
            Address = address;
        }

        public bool Connect()
        {
            try
            {
                client = new System.Net.Sockets.TcpClient(this.Address, 502);
                master = ModbusIpMaster.CreateIp(client);

                return true;
            }

            catch (Exception ex)
            {
                //this.errorCode = ex.Message;
                return false;
            }
        }

        public void DisConnect()
        {
            if (client != null)
            {
                client.Close();
                client = null;
            }
            if (master != null)
            {
                master.Dispose();
                master = null;
            }
        }

        public bool ReadBoolean(byte slaveID, ushort address)
        {
            bool[] result = master.ReadCoils(slaveID, address, 1);
            return result[0];
        }

        public void WriteBoolean(byte slaveID, ushort address, bool value)
        {
            master.WriteSingleCoil(slaveID, address, value);
        }

        public ushort ReadWord(byte slaveID, ushort address)
        {
            ushort[] result = master.ReadHoldingRegisters(slaveID, address, 1);
            return result[0];
        }

        public void WriteWord(byte slaveID, ushort address, ushort value)
        {
            master.WriteSingleRegister(slaveID, address, value);
        }

        public UInt32 ReadDoubleWord(byte slaveID, ushort address)
        {
            ushort[] result = master.ReadHoldingRegisters(slaveID, address, 2);
            byte[] data0 = BitConverter.GetBytes(result[0]);  // MW100
            byte[] data1 = BitConverter.GetBytes(result[1]);  // MW101
            byte[] data = new byte[] { data0[0], data0[1], data1[0], data1[1] };
            return BitConverter.ToUInt32(data, 0);
        }

        public void WriteDoubleWord(byte slaveID, ushort address, uint value)
        {
            byte[] valueConvert = BitConverter.GetBytes(value);
            ushort[] data = new ushort[2];
            data[0] = BitConverter.ToUInt16(valueConvert, 0);
            data[1] = BitConverter.ToUInt16(valueConvert, 2);
            master.WriteMultipleRegisters(slaveID, address, data);
        }
    }
}