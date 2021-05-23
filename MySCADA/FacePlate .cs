using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MySCADA
{
    public partial class FacePlate : Form
    {
        string Name;
        int Period;
        Timer UpdateTimer = null; //Windows form Timer
        public SCADA Parent;
        public Label lbMode;

        public Label lbRunfeedback;
        public PictureBox pbRunFeedback;
        Button btClose;
        ComboBox cbMode;
        Button btStart;
        Button btStop;
        Button btReset;
        Image imgRUN = Image.FromFile("CMD_RUN.png");
        Image imgDEFAULT = Image.FromFile("CMD_DEFAULT.png");
        //Button pbStop; 

        public FacePlate(string name, int period)
        {
            Name = name;
            Period = period;
            base.Name = Name;
            base.WindowState = FormWindowState.Normal;
            base.BackColor = Color.FromArgb(0, 95, 170);
            base.BackgroundImage = Image.FromFile("background.jpeg");
            base.Size = new Size(250, 250);
            base.Text = Name;
            base.ControlBox = false;

            // Buton Close:
            btClose = new Button();
            btClose.Text = "X";
            btClose.Size = new Size(20, 20);
            btClose.Location = new Point(210, 5);
            btClose.BackColor = Color.Red;
            btClose.Click += btClose_Click;

            this.SuspendLayout();
            // Combobox Device Mode
            cbMode = new ComboBox();
            cbMode.Items.AddRange(new object[] {"MANUAL", "AUTO"});
            cbMode.Text = "MODE";
            cbMode.Size = new Size(80, 30);
            cbMode.Location = new Point(5, 5);
            cbMode.TextChanged += cbMode_SelectedIndexChanged;
            // Buton Start
            btStart = new Button();
            btStart.Text = "START";
            btStart.Size = new Size(80, 30);
            btStart.Location = new Point(5, 90);
            btStart.BackColor = Color.LightGray;
            btStart.MouseDown += btStart_MouseDown;
            btStart.MouseUp += btStart_MouseUp;

            // Buton Stop
            btStop = new Button();
            btStop.Text = "STOP";
            btStop.Size = new Size(80, 30);
            btStop.Location = new Point(5, 130);
            btStop.BackColor = Color.LightGray;
            btStop.MouseDown += btStop_MouseDown;
            btStop.MouseUp += btStop_MouseUp;
            // Buton Reset
            btReset = new Button();
            btReset.Text = "RESET";
            btReset.Size = new Size(80, 30);
            btReset.Location = new Point(150, 170);
            btReset.BackColor = Color.LightGray;
            btReset.MouseDown += btReset_MouseDown;
            btReset.MouseUp += btReset_MouseUp;
            // Label Mode
            lbMode = new Label();
            lbMode.BackColor = Color.White;
            lbMode.ForeColor = Color.Green;
            lbMode.Font = new Font("Cambria", 12);
            lbMode.Size = new Size(20, 20);
            lbMode.Location = new Point(90, 5);

            // Label RunFeedback
            lbRunfeedback = new Label();
            // Picture RunFeedback
            pbRunFeedback = new PictureBox();
            Image imgDEFAULT = Image.FromFile("CMD_DEFAULT.png");
            pbRunFeedback.Size = imgDEFAULT.Size;
            pbRunFeedback.BackColor = Color.Transparent;
            pbRunFeedback.Location = new Point(120, 80);
            pbRunFeedback.BackgroundImage = imgDEFAULT;

            UpdateTimer = new Timer();
            UpdateTimer.Interval = Period;
            UpdateTimer.Tick += UpdateTimer_Tick;
            UpdateTimer.Start();


            // Add to FacePlate
            base.Controls.Add(lbMode);
            base.Controls.Add(btClose);
            base.Controls.Add(cbMode);
            base.Controls.Add(btStart);
            base.Controls.Add(btStop);
            base.Controls.Add(btReset);
            base.Controls.Add(pbRunFeedback);

            this.ResumeLayout(false);
        }

        private void btClose_Click(object sender, EventArgs e)
        {
            base.Close();
        }
        private void btStart_MouseDown(object sender, EventArgs e)
        {
            switch(base.Name)
            {
                case "Motor_1_Control_Panel":
                    Parent.S71500.WriteBool("DB1.DBX2.0", true);
                    break;
                case "Motor_2_Control_Panel":
                    Parent.S71500.WriteBool("DB2.DBX2.0", true);
                    break;
                case "Valve_Control_Panel":
                    Parent.S71500.WriteBool("DB3.DBX2.0", true);
                    break;
                default:
                    break;
            }    
        }
        private void btStart_MouseUp(object sender, EventArgs e)
        {
            switch (base.Name)
            {
                case "Motor_1_Control_Panel":
                    Parent.S71500.WriteBool("DB1.DBX2.0", false);
                    break;
                case "Motor_2_Control_Panel":
                    Parent.S71500.WriteBool("DB2.DBX2.0", false);
                    break;
                case "Valve_Control_Panel":
                    Parent.S71500.WriteBool("DB3.DBX2.0", false);
                    break;
                default:
                    break;
            }
        }
        private void btStop_MouseDown(object sender, EventArgs e)
        {
            switch (base.Name)
            {
                case "Motor_1_Control_Panel":
                    Parent.S71500.WriteBool("DB1.DBX2.1", true);
                    break;
                case "Motor_2_Control_Panel":
                    Parent.S71500.WriteBool("DB2.DBX2.1", true);
                    break;
                case "Valve_Control_Panel":
                    Parent.S71500.WriteBool("DB3.DBX2.1", true);
                    break;
                default:
                    break;
            }
        }
        private void btStop_MouseUp(object sender, EventArgs e)
        {
            switch (base.Name)
            {
                case "Motor_1_Control_Panel":
                    Parent.S71500.WriteBool("DB1.DBX2.1", false);
                    break;
                case "Motor_2_Control_Panel":
                    Parent.S71500.WriteBool("DB2.DBX2.1", false);
                    break;
                case "Valve_Control_Panel":
                    Parent.S71500.WriteBool("DB3.DBX2.1", false);
                    break;
                default:
                    break;
            }
        }
        private void btReset_MouseDown(object sender, EventArgs e)
        {
            switch (base.Name)
            {
                case "Motor_1_Control_Panel":
                    Parent.S71500.WriteBool("DB1.DBX2.5", true);
                    break;
                case "Motor_2_Control_Panel":
                    Parent.S71500.WriteBool("DB2.DBX2.5", true);
                    break;
                case "Valve_Control_Panel":
                    Parent.S71500.WriteBool("DB3.DBX2.5", true);
                    break;
                default:
                    break;
            }
        }
        private void btReset_MouseUp(object sender, EventArgs e)
        {
            switch (base.Name)
            {
                case "Motor_1_Control_Panel":
                    Parent.S71500.WriteBool("DB1.DBX2.5", false);
                    break;
                case "Motor_2_Control_Panel":
                    Parent.S71500.WriteBool("DB2.DBX2.5", false);
                    break;
                case "Valve_Control_Panel":
                    Parent.S71500.WriteBool("DB3.DBX2.5", false);
                    break;
                default:
                    break;
            }
        }
        private void UpdateTimer_Tick(object sender, EventArgs e)
        {
                        
            if (lbRunfeedback.Text == "True")
            {
                pbRunFeedback.BackgroundImage = imgRUN;
            }
            else {
                pbRunFeedback.BackgroundImage = imgDEFAULT;
            }
        }

        private void cbMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            string mode;
            mode = cbMode.Text;
            if (mode == "AUTO")
            {
                short value = 2; //Khai báo kiểu short ( tương đương int 16 bits của S7)
                switch (base.Name)
                {
                    case "Motor_1_Control_Panel":
                        Parent.S71500.WriteInt("DB1.DBW0", value);
                        break;
                    case "Motor_2_Control_Panel":
                        Parent.S71500.WriteInt("DB2.DBW0", value);
                        break;
                    case "Valve_Control_Panel":
                        Parent.S71500.WriteInt("DB3.DBW0", value);
                        break;
                    default:
                        break;
                }
            }
            else if (mode == "MANUAL")
            {
                short value = 1; //Khai báo kiểu short ( tương đương int 16 bits của S7)
                switch (base.Name)
                {
                    case "Motor_1_Control_Panel":
                        Parent.S71500.WriteInt("DB1.DBW0", value);
                        break;
                    case "Motor_2_Control_Panel":
                        Parent.S71500.WriteInt("DB2.DBW0", value);
                        break;
                    case "Valve_Control_Panel":
                        Parent.S71500.WriteInt("DB3.DBW0", value);
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
