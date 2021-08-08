using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace MySCADA
{
    public class SCADA
    {
        ArrayList Tasks = new ArrayList();
        public PLC_S7 S71500;
        public PLC_Modbus M340;
        public void AddPLC(PLC_S7 plc)
        {
            plc.Parent = this;
            S71500 = plc;
        }
        public void AddPLC(PLC_Modbus plc)
        { 
            plc.Parent = this;
            M340 = plc;
        }
        public void AddTask(Task task)
        {
            task.Parent = this;
            Tasks.Add(task);
        }
        public Task FindTask(string name)
        {
            Task task = null;
            for (int i = 0; i < Tasks.Count; i++)
            {
                task = (Task)Tasks[i];
                if (task.Name == name)
                    return task;
            }
            return null;
        }
        public void RunTask(string name)
        {
            Task task = null;
            for (int i = 0; i < Tasks.Count; i++)
            {
                task = (Task)Tasks[i];
                if (task.Name == name)
                    task.Engine();
            }
        }
        public void SleepTask(string name)
        {
            Task task = null;
            for (int i = 0; i < Tasks.Count; i++)
            {
                task = (Task)Tasks[i];
                if (task.Name == name)
                    task.Sleep();
            }
        }
        public void ResumeTask(string name)
        {
            Task task = null;
            for (int i = 0; i < Tasks.Count; i++)
            {
                task = (Task)Tasks[i];
                if (task.Name == name)
                    task.Resume();
            }
        }
        public void KillTask(string name)
        {
            Task task = null;
            for (int i = 0; i < Tasks.Count; i++)
            {
                task = (Task)Tasks[i];
                if (task.Name == name)
                    task.Kill();
            }
        }
    }
}
