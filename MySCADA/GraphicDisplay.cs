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

        Label lbMotor_1_Mode = new Label();
        Label lbMotor_1_Runfeedback = new Label();
        FacePlate Motor_1_Control_Page = new FacePlate("Motor_1_Control_Panel", 100);
        FacePlate Motor_2_Control_Page = new FacePlate("Motor_2_Control_Panel", 100);
        
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
            Image img1 = Image.FromFile("Motor.bmp");
            pbMotor_1.BackgroundImage = img1;
            pbMotor_1.Size = img1.Size;
            pbMotor_1.Location = new Point(100, 200);
            pbMotor_1.BackColor = Color.LightGray;
            pbMotor_1.Click += pbMotor_1_Click;

            pbMotor_2 = new PictureBox();
            pbMotor_2.BackColor = Color.Transparent;
            Image img2 = Image.FromFile("Motor.bmp");
            pbMotor_2.BackgroundImage = img2;
            pbMotor_2.Size = img2.Size;
            pbMotor_2.Location = new Point(350, 200);
            pbMotor_2.BackColor = Color.LightGray;
            pbMotor_2.Click += pbMotor_2_Click;

            UpdateTimer = new Timer();
            UpdateTimer.Interval = Period;
            UpdateTimer.Tick += UpdateTimer_Tick;

            base.Controls.Add(pbMotor_1);
            base.Controls.Add(pbMotor_2);

            UpdateTimer.Start();
        }

        private void UpdateTimer_Tick(object sender, EventArgs e)
        {
            Task task = Parent.FindTask("Task_1");
            Tag tag;
            if (task != null)
            {
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
            }

        }

       
        private void pbMotor_1_Click(object sender, EventArgs e)
        {
            if (Motor_1_Control_Page.IsDisposed)
            {
                Motor_1_Control_Page = new FacePlate("Motor_1_Control_Panel", 100);
            }
            Motor_1_Control_Page.Show();
        }
        private void pbMotor_2_Click(object sender, EventArgs e)
        {
            if (Motor_2_Control_Page.IsDisposed)
            {
                Motor_2_Control_Page = new FacePlate("Motor_2_Control_Panel", 100);
            }
            Motor_2_Control_Page.Show();
        }
       
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // GraphicDisplay
            // 
            this.ClientSize = new System.Drawing.Size(816, 552);
            this.Name = "GraphicDisplay";
            this.ResumeLayout(false);
        }

    }
}
