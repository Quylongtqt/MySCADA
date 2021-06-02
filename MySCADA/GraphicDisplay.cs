using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;
using System.Drawing;
using MindFusion.Charting;

namespace MySCADA
{
    class GraphicDisplay : Form
    {
        public string Name;
        int Period;
        Timer UpdateTimer = null; //Windows form Timer
        public ArrayList Tags = new ArrayList();
        public SCADA Parent;

        PictureBox pbTank;
        PictureBox pbMotor_1;
        PictureBox pbMotor_2;
        PictureBox pbValve_1;
        PictureBox pbMotor_1_RunFeedback;
        PictureBox pbMotor_2_RunFeedback;
        Button btSTART;
        Button btSTOP;
        Button btALARM;
        Button btVIEW;

        Label lbMotor_1_Mode = new Label();
        Label lbMotor_1_Runfeedback = new Label();
        Label lbMotor_2_Mode = new Label();
        Label lbMotor_2_Runfeedback = new Label();
        Label lbValve_1_Mode = new Label();
        Label lbValve_1_Runfeedback = new Label();
        MotorPanel Motor_1_Control_Page = new MotorPanel(1, 100);
        MotorPanel Motor_2_Control_Page = new MotorPanel(2, 100);
        ValvePanel Valve_1_Control_Page = new ValvePanel(1, 3, 100);
        ChartView Tank_Level_Page = new ChartView(1000);
        AlarmView Tank_Alarm_Page = new AlarmView(5000);
        // Image from file
        Image imgTank = Image.FromFile("Tank.png");
        Image imgMotor_1 = Image.FromFile("Motor.png");
        Image imgMotor_2 = Image.FromFile("Motor.png");
        Image imgValve_1 = Image.FromFile("Valve.png");
        Image imgPipe_1 = Image.FromFile("Pipe_1.png");
        Image imgPipe_2 = Image.FromFile("Pipe_2.png");
        Image imgPipe_3 = Image.FromFile("Pipe_3.png");
        Image imgValve_OPEN = Image.FromFile("Valve_OPEN.png");
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.ComponentModel.BackgroundWorker backgroundWorker2;
        private System.ComponentModel.BackgroundWorker backgroundWorker3;
        public ProgressBars.Basic.BasicProgressBar barLevel;
        Image imgValve_CLOSE = Image.FromFile("Valve_CLOSE.png");
        public GraphicDisplay(string name, int period)
        {
            InitializeComponent();
            Name = name;
            Period = period;
            base.Name = Name;
            base.WindowState = FormWindowState.Normal;
            base.BackColor = Color.FromArgb(0, 95, 170);
            base.BackgroundImage = Image.FromFile("background.jpeg");
            base.Size = new Size(1200, 800);
            base.Text = ("My SCADA");
            base.ControlBox = false;
            
            pbTank = new PictureBox();
            pbTank.BackColor = Color.Transparent;
            pbTank.BackgroundImage = imgTank;
            pbTank.Size = imgTank.Size;
            pbTank.Location = new Point(300, 100);

            pbMotor_1 = new PictureBox();
            pbMotor_1.BackColor = Color.Transparent;
            pbMotor_1.BackgroundImage = imgMotor_1;
            pbMotor_1.Size = imgMotor_1.Size;
            pbMotor_1.Location = new Point(155, 552);
            pbMotor_1.Click += pbMotor_1_Click;

            pbMotor_2 = new PictureBox();
            pbMotor_2.BackColor = Color.Transparent;
            pbMotor_2.BackgroundImage = imgMotor_2;
            pbMotor_2.Size = imgMotor_2.Size;
            pbMotor_2.Location = new Point(155, 203);
            pbMotor_2.Click += pbMotor_2_Click;

            pbValve_1 = new PictureBox();
            pbValve_1.BackColor = Color.Transparent;
            pbValve_1.BackgroundImage = imgValve_1;
            pbValve_1.Size = imgValve_1.Size;
            pbValve_1.Location = new Point(690, 520);
            pbValve_1.Click += pbValve_1_Click;

            pbMotor_1_RunFeedback = new PictureBox();
            pbMotor_1_RunFeedback.BackColor = Color.Transparent;
            pbMotor_1_RunFeedback.BackgroundImage = imgPipe_1;
            pbMotor_1_RunFeedback.Size = imgPipe_1.Size;
            pbMotor_1_RunFeedback.Location = new Point(270, 590);

            pbMotor_2_RunFeedback = new PictureBox();
            pbMotor_2_RunFeedback.BackColor = Color.Transparent;
            pbMotor_2_RunFeedback.BackgroundImage = imgPipe_1;
            pbMotor_2_RunFeedback.Size = imgPipe_1.Size;
            pbMotor_2_RunFeedback.Location = new Point(270, 240);

            // Buton START
            btSTART = new Button();
            btSTART.Text = "START";
            btSTART.Size = new Size(80, 30);
            btSTART.Location = new Point(50, 100);
            btSTART.BackColor = Color.LightGray;
            btSTART.MouseDown += btSTART_MouseDown;
            btSTART.MouseUp += btSTART_MouseUp;
            // Buton STOP
            btSTOP = new Button();
            btSTOP.Text = "STOP";
            btSTOP.Size = new Size(80, 30);
            btSTOP.Location = new Point(50, 150);
            btSTOP.BackColor = Color.LightGray;
            btSTOP.MouseDown += btSTOP_MouseDown;
            btSTOP.MouseUp += btSTOP_MouseUp;
            // Buton VIEW
            btVIEW = new Button();
            btVIEW.Text = "VIEW";
            btVIEW.Size = new Size(80, 30);
            btVIEW.Location = new Point(50, 200);
            btVIEW.BackColor = Color.LightGray;
            btVIEW.MouseDown += btVIEW_Click;
            // Buton ALARM
            btALARM = new Button();
            btALARM.Text = "ALARM";
            btALARM.Size = new Size(80, 30);
            btALARM.Location = new Point(50, 250);
            btALARM.BackColor = Color.LightGray;
            btALARM.MouseDown += btALARM_Click;

            UpdateTimer = new Timer();
            UpdateTimer.Interval = Period;
            UpdateTimer.Tick += UpdateTimer_Tick;

            base.Controls.Add(btSTART);
            base.Controls.Add(btSTOP);
            base.Controls.Add(pbTank);
            base.Controls.Add(btVIEW);
            base.Controls.Add(btALARM);

            base.Controls.Add(pbMotor_1);
            base.Controls.Add(pbMotor_2);
            base.Controls.Add(pbValve_1);
            base.Controls.Add(pbMotor_1_RunFeedback);
            base.Controls.Add(pbMotor_2_RunFeedback);
            UpdateTimer.Start();
        }
        //START
        private void btSTART_MouseDown(object sender, EventArgs e)
        {
            Parent.S71500.WriteBool($"M0.0", true);
        }
        private void btSTART_MouseUp(object sender, EventArgs e)
        {
            Parent.S71500.WriteBool($"M0.0", false);
        }
        //STOP
        private void btSTOP_MouseDown(object sender, EventArgs e)
        {
            Parent.S71500.WriteBool($"M0.1", true);
        }
        private void btSTOP_MouseUp(object sender, EventArgs e)
        {
            Parent.S71500.WriteBool($"M0.1", false);
        }
        //VIEW
        private void btVIEW_Click(object sender, EventArgs e)
        {
            if (Tank_Level_Page.IsDisposed)
            {

                Tank_Level_Page = new ChartView(1000);
                Tank_Level_Page.Parent = this.Parent;
            }
                Tank_Level_Page.Show();
                Tank_Level_Page.Parent = this.Parent;
        }
        //ALARM
        private void btALARM_Click(object sender, EventArgs e)
        {
            if (Tank_Alarm_Page.IsDisposed)
            {

                Tank_Alarm_Page = new AlarmView(5000);
                Tank_Alarm_Page.Parent = this.Parent;
            }
            Tank_Alarm_Page.Show();
            Tank_Alarm_Page.Parent = this.Parent;
        }
        private void UpdateTimer_Tick(object sender, EventArgs e)
        {
            Task task = Parent.FindTask("Task_1");
            Tag tag;
            if (task != null)
            {
                // Motor_1
                tag = task.FindTag("Motor_1_Mode");
                if (tag != null)
                {
                    lbMotor_1_Mode.Text = tag.Value.ToString();
                    Motor_1_Control_Page.lbMode.Text = lbMotor_1_Mode.Text;
                }
                tag = task.FindTag("Motor_1_RunFeedback");
                if (tag != null)
                {
                    lbMotor_1_Runfeedback.Text = tag.Value.ToString();
                    Motor_1_Control_Page.lbRunfeedback.Text = lbMotor_1_Runfeedback.Text;
                }
                tag = task.FindTag("Motor_1_Pos");
                if (tag != null)
                {
                    switch (Convert.ToInt16(tag.Value))
                    {
                        case 0:
                             pbMotor_1_RunFeedback.BackgroundImage = imgPipe_2;
                            break;
                        case 1:
                            pbMotor_1_RunFeedback.BackgroundImage = imgPipe_3;
                            break;
                        case 2:
                            pbMotor_1_RunFeedback.BackgroundImage = imgPipe_2;
                            break;
                        case 3:
                            pbMotor_1_RunFeedback.BackgroundImage = imgPipe_3;
                            break;
                        case 4:
                            pbMotor_1_RunFeedback.BackgroundImage = imgPipe_2;
                            break;
                    }
                }
                // Motor_2
                tag = task.FindTag("Motor_2_Mode");
                if (tag != null)
                {
                    lbMotor_2_Mode.Text = tag.Value.ToString();
                    Motor_2_Control_Page.lbMode.Text = lbMotor_2_Mode.Text;
                }
                tag = task.FindTag("Motor_2_RunFeedback");
                if (tag != null)
                {
                    lbMotor_2_Runfeedback.Text = tag.Value.ToString();
                    Motor_2_Control_Page.lbRunfeedback.Text = lbMotor_2_Runfeedback.Text;
                }
                tag = task.FindTag("Motor_2_Pos");
                if (tag != null)
                {
                    switch (Convert.ToInt16(tag.Value))
                    {
                        case 0:
                            pbMotor_2_RunFeedback.BackgroundImage = imgPipe_2;
                            break;
                        case 1:
                            pbMotor_2_RunFeedback.BackgroundImage = imgPipe_3;
                            break;
                        case 2:
                            pbMotor_2_RunFeedback.BackgroundImage = imgPipe_2;
                            break;
                        case 3:
                            pbMotor_2_RunFeedback.BackgroundImage = imgPipe_3;
                            break;
                        case 4:
                            pbMotor_2_RunFeedback.BackgroundImage = imgPipe_2;
                            break;
                    }
                }
                // Valve_1
                tag = task.FindTag("Valve_1_Mode");
                if (tag != null)
                {
                    lbValve_1_Mode.Text = tag.Value.ToString();
                    Valve_1_Control_Page.lbMode.Text = lbValve_1_Mode.Text;
                }
                tag = task.FindTag("Valve_1_RunFeedback");
                if (tag != null)
                {

                    lbValve_1_Runfeedback.Text = tag.Value.ToString();
                    if(lbValve_1_Runfeedback.Text == "True")
                    {
                        pbValve_1.BackgroundImage = imgValve_OPEN;
                    }
                    if (lbValve_1_Runfeedback.Text == "False")
                    {
                        pbValve_1.BackgroundImage = imgValve_CLOSE;
                    }
                    Valve_1_Control_Page.lbRunfeedback.Text = lbValve_1_Runfeedback.Text;
                }
                // Tank_level
                tag = task.FindTag("Tank_Level");
                if (tag != null)
                {
                    int val = Convert.ToInt16(tag.Value);
                    barLevel.Value = val;
                    Tank_Level_Page.TankLevel = val;
                    Tank_Alarm_Page.TankLevel = val;
                    Tank_Alarm_Page.LevelQuality = tag.Quality;
                    Tank_Alarm_Page.Time = (tag.TimeStamp).ToString(@"yyyy\/MM\/dd\ hh\:mm\:ss");
                }
            }
        }

        private void pbMotor_1_Click(object sender, EventArgs e)
        {
            if (Motor_1_Control_Page.IsDisposed)
            {
                Motor_1_Control_Page = new MotorPanel(1, 100);
                Motor_1_Control_Page.Parent = this.Parent;
            }
            Motor_1_Control_Page.Show();
            Motor_1_Control_Page.Parent = this.Parent;
        }
        private void pbMotor_2_Click(object sender, EventArgs e)
        {
            if (Motor_2_Control_Page.IsDisposed)
            {
                Motor_2_Control_Page = new MotorPanel(2, 100);
                Motor_2_Control_Page.Parent = this.Parent;
            }
            Motor_2_Control_Page.Show();
            Motor_2_Control_Page.Parent = this.Parent;
        }
        private void pbValve_1_Click(object sender, EventArgs e)
        {
            if (Valve_1_Control_Page.IsDisposed)
            {
                Valve_1_Control_Page = new ValvePanel(1, 3, 100);
                Valve_1_Control_Page.Parent = this.Parent;
            }
            Valve_1_Control_Page.Show();
            Valve_1_Control_Page.Parent = this.Parent;
        }
        
        private void InitializeComponent()
        {
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.backgroundWorker2 = new System.ComponentModel.BackgroundWorker();
            this.backgroundWorker3 = new System.ComponentModel.BackgroundWorker();
            this.barLevel = new ProgressBars.Basic.BasicProgressBar();
            this.SuspendLayout();
            // 
            // barLevel
            // 
            this.barLevel.BackColor = System.Drawing.Color.DarkGray;
            this.barLevel.Font = new System.Drawing.Font("Consolas", 10.25F);
            this.barLevel.ForeColor = System.Drawing.Color.DodgerBlue;
            this.barLevel.Location = new System.Drawing.Point(590, 140);
            this.barLevel.Name = "barLevel";
            this.barLevel.Size = new System.Drawing.Size(30, 520);
            this.barLevel.TabIndex = 0;
            this.barLevel.Text = "barLevel";
            // 
            // GraphicDisplay
            // 
            this.ClientSize = new System.Drawing.Size(816, 552);
            this.Controls.Add(this.barLevel);
            this.Name = "GraphicDisplay";
            this.ResumeLayout(false);

        }
    }
}
