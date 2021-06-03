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
    public partial class AlarmView : Form
    {
        int Period;
        Timer UpdateTimer = null; //Windows form Timer
        public SCADA Parent;
        public int TankLevel;
        public string LevelQuality;
        public string Time;
        Button btCloseX;
        TextBox textBox_0;
        TextBox textBox_1;
        TextBox textBox_2;
        TextBox textBox_3;
        TextBox textBox_4;
        TextBox textBox_5;
        TextBox textBox_6;
        TextBox textBox_7;
        TextBox textBox_8;
        TextBox textBox_9;
        TextBox textBox_10;
        public AlarmView(int period)
        {
            InitializeComponent();
            Period = period;

            // Buton Close X:
            btCloseX = new Button();
            btCloseX.Text = "X";
            btCloseX.Size = new Size(20, 20);
            btCloseX.Location = new Point(420, 5);
            btCloseX.BackColor = Color.Red;
            btCloseX.Click += btCloseX_Click;
            base.ControlBox = false;
            base.SuspendLayout();

            textBox_0 = new TextBox();
            textBox_0.Location = new System.Drawing.Point(20, 20);
            textBox_0.Size = new System.Drawing.Size(400, 20);
            textBox_0.Text = "---------TimeStamp-------------Tank Level---------Status-----";

            textBox_1 = new TextBox();
            textBox_1.Location = new System.Drawing.Point(20, 40);
            textBox_1.Size = new System.Drawing.Size(400, 20);

            textBox_2 = new TextBox();
            textBox_2.Location = new System.Drawing.Point(20, 60);
            textBox_2.Size = new System.Drawing.Size(400, 20);

            textBox_3 = new TextBox();
            textBox_3.Location = new System.Drawing.Point(20, 80);
            textBox_3.Size = new System.Drawing.Size(400, 20);

            textBox_4 = new TextBox();
            textBox_4.Location = new System.Drawing.Point(20, 100);
            textBox_4.Size = new System.Drawing.Size(400, 20);

            textBox_5 = new TextBox();
            textBox_5.Location = new System.Drawing.Point(20, 120);
            textBox_5.Size = new System.Drawing.Size(400, 20);

            textBox_6 = new TextBox();
            textBox_6.Location = new System.Drawing.Point(20, 140);
            textBox_6.Size = new System.Drawing.Size(400, 20);

            textBox_7 = new TextBox();
            textBox_7.Location = new System.Drawing.Point(20, 160);
            textBox_7.Size = new System.Drawing.Size(400, 20);

            textBox_8 = new TextBox();
            textBox_8.Location = new System.Drawing.Point(20, 180);
            textBox_8.Size = new System.Drawing.Size(400, 20);

            textBox_9 = new TextBox();
            textBox_9.Location = new System.Drawing.Point(20, 200);
            textBox_9.Size = new System.Drawing.Size(400, 20);

            textBox_10 = new TextBox();
            textBox_10.Location = new System.Drawing.Point(20, 220);
            textBox_10.Size = new System.Drawing.Size(400, 20);

            // Timer
            UpdateTimer = new Timer();
            UpdateTimer.Interval = Period;
            UpdateTimer.Tick += UpdateTimer_Tick;
            UpdateTimer.Start();
            base.Controls.Add(btCloseX);
            base.Controls.Add(textBox_0);
            base.Controls.Add(textBox_1);
            base.Controls.Add(textBox_2);
            base.Controls.Add(textBox_3);
            base.Controls.Add(textBox_4);
            base.Controls.Add(textBox_5);
            base.Controls.Add(textBox_6);
            base.Controls.Add(textBox_7);
            base.Controls.Add(textBox_8);
            base.Controls.Add(textBox_9);
            base.Controls.Add(textBox_10);
        }
        private void btCloseX_Click(object sender, EventArgs e)
        {
            base.Close();
        }
        private void UpdateTimer_Tick(object sender, EventArgs e)
        {
            textBox_10.Text = textBox_9.Text;
            textBox_10.BackColor = textBox_9.BackColor;
            textBox_9.Text = textBox_8.Text;
            textBox_9.BackColor = textBox_8.BackColor;
            textBox_8.Text = textBox_7.Text;
            textBox_8.BackColor = textBox_7.BackColor;
            textBox_7.Text = textBox_6.Text;
            textBox_7.BackColor = textBox_6.BackColor;
            textBox_6.Text = textBox_5.Text;
            textBox_6.BackColor = textBox_5.BackColor;
            textBox_5.Text = textBox_4.Text;
            textBox_5.BackColor = textBox_4.BackColor;
            textBox_4.Text = textBox_3.Text;
            textBox_4.BackColor = textBox_3.BackColor;
            textBox_3.Text = textBox_2.Text;
            textBox_3.BackColor = textBox_2.BackColor;
            textBox_2.Text = textBox_1.Text;
            textBox_2.BackColor = textBox_1.BackColor;
            textBox_1.Text = Time + "   " + "   " + "   " + "   " + TankLevel.ToString() + "   " + "   " + "   " + "   " + "    " + LevelQuality;
            switch(LevelQuality)
            {
                case "LOW LOW":
                case "HIGH HIGH":
                    textBox_1.BackColor = Color.Red;
                    break;
                case "LOW":
                case "HIGH":
                    textBox_1.BackColor = Color.Orange;
                    break;
                case "GOOD":
                    textBox_1.BackColor = Color.LightGreen;
                    break;
            }
                
        }
    }
    
}
