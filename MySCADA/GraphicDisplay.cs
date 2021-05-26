using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;
using System.Drawing;

namespace MySCADA
{
    class GraphicDisplay : Form
    {
        public string Name;
        int Period;
        Timer UpdateTimer = null; //Windows form Timer
        public ArrayList Tags = new ArrayList();
        public SCADA Parent;

        PictureBox pbMotor_1;
        PictureBox pbMotor_2;
        PictureBox pbValve_1;
        PictureBox pbMotor_1_RunFeedback;
        PictureBox pbMotor_2_RunFeedback;

        Label lbMotor_1_Mode = new Label();
        Label lbMotor_1_Runfeedback = new Label();
        Label lbMotor_2_Mode = new Label();
        Label lbMotor_2_Runfeedback = new Label();
        Label lbValve_1_Mode = new Label();
        Label lbValve_1_Runfeedback = new Label();
        MotorPanel Motor_1_Control_Page = new MotorPanel(1, 100);
        MotorPanel Motor_2_Control_Page = new MotorPanel(2, 100);
        ValvePanel Valve_1_Control_Page = new ValvePanel(1, 3, 100);
        // Image from file
        Image imgMotor_1 = Image.FromFile("Motor.bmp");
        Image imgMotor_2 = Image.FromFile("Motor.bmp");
        Image imgValve_1 = Image.FromFile("Valve.png");
        Image imgFan_1 = Image.FromFile("fan_1.gif");
        Image imgFan_2 = Image.FromFile("fan_2.gif");
        Image imgFan_3 = Image.FromFile("fan_3.gif");
        Image imgFan_4 = Image.FromFile("fan_4.gif");
        Image imgValve_OPEN = Image.FromFile("Valve_OPEN.png");
        Image imgValve_CLOSE = Image.FromFile("Valve_CLOSE.png");
        public GraphicDisplay(string name, int period)
        {
            Name = name;
            Period = period;
            base.Name = Name;
            base.WindowState = FormWindowState.Normal;
            base.BackColor = Color.FromArgb(0, 95, 170);
            base.BackgroundImage = Image.FromFile("background.jpeg");
            base.Size = new Size(1000, 500);
            base.Text = ("My SCADA");
            base.ControlBox = false;

            pbMotor_1 = new PictureBox();
            pbMotor_1.BackColor = Color.Transparent;
            pbMotor_1.BackgroundImage = imgMotor_1;
            pbMotor_1.Size = imgMotor_1.Size;
            pbMotor_1.Location = new Point(100, 200);
            pbMotor_1.Click += pbMotor_1_Click;

            pbMotor_2 = new PictureBox();
            pbMotor_2.BackColor = Color.Transparent;
            pbMotor_2.BackgroundImage = imgMotor_2;
            pbMotor_2.Size = imgMotor_2.Size;
            pbMotor_2.Location = new Point(350, 200);
            pbMotor_2.Click += pbMotor_2_Click;

            pbValve_1 = new PictureBox();
            pbValve_1.BackColor = Color.Transparent;
            pbValve_1.BackgroundImage = imgValve_1;
            pbValve_1.Size = imgValve_1.Size;
            pbValve_1.Location = new Point(650, 200);
            pbValve_1.Click += pbValve_1_Click;

            imgFan_1.RotateFlip(RotateFlipType.Rotate180FlipY);
            imgFan_2.RotateFlip(RotateFlipType.Rotate180FlipY);
            imgFan_3.RotateFlip(RotateFlipType.Rotate180FlipY);
            imgFan_4.RotateFlip(RotateFlipType.Rotate180FlipY);

            pbMotor_1_RunFeedback = new PictureBox();
            pbMotor_1_RunFeedback.BackColor = Color.Transparent;
            pbMotor_1_RunFeedback.BackgroundImage = imgFan_1;
            pbMotor_1_RunFeedback.Size = imgFan_1.Size;
            pbMotor_1_RunFeedback.Location = new Point(260, 155);

            pbMotor_2_RunFeedback = new PictureBox();
            pbMotor_2_RunFeedback.BackColor = Color.Transparent;
            pbMotor_2_RunFeedback.BackgroundImage = imgFan_1;
            pbMotor_2_RunFeedback.Size = imgFan_1.Size;
            pbMotor_2_RunFeedback.Location = new Point(510, 155);


            UpdateTimer = new Timer();
            UpdateTimer.Interval = Period;
            UpdateTimer.Tick += UpdateTimer_Tick;

            base.Controls.Add(pbMotor_1);
            base.Controls.Add(pbMotor_2);
            base.Controls.Add(pbValve_1);
            base.Controls.Add(pbMotor_1_RunFeedback);
            base.Controls.Add(pbMotor_2_RunFeedback);

            UpdateTimer.Start();
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
                             pbMotor_1_RunFeedback.BackgroundImage = imgFan_1;
                            break;
                        case 1:
                            pbMotor_1_RunFeedback.BackgroundImage = imgFan_1;
                            break;
                        case 2:
                            pbMotor_1_RunFeedback.BackgroundImage = imgFan_2;
                            break;
                        case 3:
                            pbMotor_1_RunFeedback.BackgroundImage = imgFan_3;
                            break;
                        case 4:
                            pbMotor_1_RunFeedback.BackgroundImage = imgFan_4;
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
                            pbMotor_2_RunFeedback.BackgroundImage = imgFan_1;
                            break;
                        case 1:
                            pbMotor_2_RunFeedback.BackgroundImage = imgFan_1;
                            break;
                        case 2:
                            pbMotor_2_RunFeedback.BackgroundImage = imgFan_2;
                            break;
                        case 3:
                            pbMotor_2_RunFeedback.BackgroundImage = imgFan_3;
                            break;
                        case 4:
                            pbMotor_2_RunFeedback.BackgroundImage = imgFan_4;
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
            this.SuspendLayout();
            // GraphicDisplay
            this.ClientSize = new System.Drawing.Size(816, 552);
            this.Name = "GraphicDisplay";
            this.ResumeLayout(false);
        }

    }
}
