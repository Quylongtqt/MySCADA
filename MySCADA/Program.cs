using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace MySCADA
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Very Simple SCADA Kernel");
            Console.WriteLine("The program helps to learn");
            Console.WriteLine("SCADA Design and Programming");
            Console.WriteLine("Press any key to continue");
            //Console.ReadKey();

            SCADA Root = new SCADA();
            PLC plc = new PLC();
            Root.AddPLC(plc);
            // Task 1:
            Task Task1 = new Task("Task_1", 100); //update time 100ms

            Tag Motor_1_Mode = new Tag("Motor_1_Mode","Motor_1.Mode");
            Tag Motor_2_Mode = new Tag("Motor_2_Mode", "Motor_2.Mode");
            Tag Valve_1_Mode = new Tag("Valve_1_Mode", "Valve_1.Mode");
            Tag Tank_Level = new Tag("Tank_Level", "Tank.Level");

            Tag Motor_1_Start = new Tag("Motor_1_Start", "Motor_1.Start");
            Tag Motor_2_Start = new Tag("Motor_2_Start", "Motor_2.Start");
            Tag Valve_1_Start = new Tag("Valve_1_Start", "Valve_1.Start");

            Tag Motor_1_Stop = new Tag("Motor_1_Stop", "Motor_1.Stop");
            Tag Motor_2_Stop = new Tag("Motor_2_Stop", "Motor_2.Stop");
            Tag Valve_1_Stop = new Tag("Valve_1_Stop", "Valve_1.Stop");

            Tag Motor_1_RunFeedback = new Tag("Motor_1_RunFeedback", "Motor_1.RunFeedback");
            Tag Motor_2_RunFeedback = new Tag("Motor_2_RunFeedback", "Motor_2.RunFeedback");
            Tag Valve_1_RunFeedback = new Tag("Valve_1_RunFeedback", "Valve_1.RunFeedback");
            
            Tag Motor_1_Reset = new Tag("Motor_1_Reset", "Motor_1.Reset");
            Tag Motor_2_Reset = new Tag("Motor_2_Reset", "Motor_2.Reset");
            Tag Valve_1_Reset = new Tag("Valve_1_Reset", "Valve_1.Reset");
           
            Tag Motor_1_Fault = new Tag("Motor_1_Fault", "Motor_1.Fault");
            Tag Motor_2_Fault = new Tag("Motor_2_Fault", "Motor_2.Fault");
            Tag Valve_1_Fault = new Tag("Valve_1_Fault", "Valve_1.Fault");

            Tag Motor_1_Pos = new Tag("Motor_1_Pos", "Motor_1.Pos");
            Tag Motor_2_Pos = new Tag("Motor_2_Pos", "Motor_2.Pos");
            Tag Valve_1_Pos = new Tag("Valve_1_Pos", "Valve_1.Pos");

            Task1.AddTag(Motor_1_Mode);
            Task1.AddTag(Motor_2_Mode);
            Task1.AddTag(Valve_1_Mode);
            Task1.AddTag(Tank_Level);

            Task1.AddTag(Motor_1_Start);
            Task1.AddTag(Motor_2_Start);
            Task1.AddTag(Valve_1_Start);

            Task1.AddTag(Motor_1_Stop);
            Task1.AddTag(Motor_2_Stop);
            Task1.AddTag(Valve_1_Stop);

            Task1.AddTag(Motor_1_RunFeedback);
            Task1.AddTag(Motor_2_RunFeedback);
            Task1.AddTag(Valve_1_RunFeedback);

            Task1.AddTag(Motor_1_Reset);
            Task1.AddTag(Motor_2_Reset);
            Task1.AddTag(Valve_1_Reset);

            Task1.AddTag(Motor_1_Fault);
            Task1.AddTag(Motor_2_Fault);
            Task1.AddTag(Valve_1_Fault);

            Task1.AddTag(Motor_1_Pos);
            Task1.AddTag(Motor_2_Pos);
            Task1.AddTag(Valve_1_Pos);

            Root.AddTask(Task1);
            Root.RunTask("Task_1");
            
            GraphicDisplay Main_Page = new GraphicDisplay("Main_Page", 100);
            Main_Page.Parent = Root;
            Main_Page.ShowDialog();

            Console.ReadKey();
        }
    }
}

