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
    public partial class ValvePanel : Form
    {
        int ID;
        int DBnumber;
        int Period;
        Timer UpdateTimer = null; //Windows form Timer
        public SCADA Parent;
        public Label lbMode;
        public Label lbRunfeedback;
        public PictureBox pbRunFeedback;
        Button btCloseX;
        ComboBox cbMode;
        Button btOpen;
        Button btClose;
        Button btReset;
        Image imgRUN = Image.FromFile("CMD_RUN.png");
        Image imgDEFAULT = Image.FromFile("CMD_DEFAULT.png");

        public ValvePanel(int id, int dbnumber, int period)
        {
            ID = id;
            DBnumber = dbnumber;
            string Name = "Valve_" + ID + " Control Page";
            Period = period;
            base.Name = Name;
            base.WindowState = FormWindowState.Normal;
            base.BackColor = Color.FromArgb(0, 95, 170);
            base.BackgroundImage = Image.FromFile("background.jpeg");
            base.Size = new Size(250, 250);
            base.Text = Name;
            base.ControlBox = false;

            // Buton Close X:
            btCloseX = new Button();
            btCloseX.Text = "X";
            btCloseX.Size = new Size(20, 20);
            btCloseX.Location = new Point(210, 5);
            btCloseX.BackColor = Color.Red;
            btCloseX.Click += btCloseX_Click;
            this.SuspendLayout();
            // Combobox Device Mode
            cbMode = new ComboBox();
            cbMode.Items.AddRange(new object[] { "MANUAL", "AUTO" });
            cbMode.Text = "MODE";
            cbMode.Size = new Size(80, 30);
            cbMode.Location = new Point(5, 5);
            cbMode.TextChanged += cbMode_SelectedIndexChanged;
            // Buton Open
            btOpen = new Button();
            btOpen.Text = "OPEN";
            btOpen.Size = new Size(80, 30);
            btOpen.Location = new Point(5, 90);
            btOpen.BackColor = Color.LightGray;
            btOpen.MouseDown += btOpen_MouseDown;
            btOpen.MouseUp += btOpen_MouseUp;
            // Buton Close
            btClose = new Button();
            btClose.Text = "CLOSE";
            btClose.Size = new Size(80, 30);
            btClose.Location = new Point(5, 130);
            btClose.BackColor = Color.LightGray;
            btClose.MouseDown += btClose_MouseDown;
            btClose.MouseUp += btClose_MouseUp;
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

            // Add to ValvePanel
            base.Controls.Add(lbMode);
            base.Controls.Add(btCloseX);
            base.Controls.Add(cbMode);
            base.Controls.Add(btOpen);
            base.Controls.Add(btClose);
            base.Controls.Add(btReset);
            base.Controls.Add(pbRunFeedback);

            this.ResumeLayout(false);
        }

        private void btCloseX_Click(object sender, EventArgs e)
        {
            base.Close();
        }
        private void btOpen_MouseDown(object sender, EventArgs e)
        {
            Parent.S71500.WriteBool($"DB{DBnumber}.DBX2.0", true);
        }
        private void btOpen_MouseUp(object sender, EventArgs e)
        {
            Parent.S71500.WriteBool($"DB{DBnumber}.DBX2.0", false);
        }
        private void btClose_MouseDown(object sender, EventArgs e)
        {
            Parent.S71500.WriteBool($"DB{DBnumber}.DBX2.1", true);
        }
        private void btClose_MouseUp(object sender, EventArgs e)
        {
            Parent.S71500.WriteBool($"DB{DBnumber}.DBX2.1", false);
        }
        private void btReset_MouseDown(object sender, EventArgs e)
        {
            Parent.S71500.WriteBool($"DB{DBnumber}.DBX2.5", true);
        }
        private void btReset_MouseUp(object sender, EventArgs e)
        {
            Parent.S71500.WriteBool($"DB{DBnumber}.DBX2.5", false);
        }
        private void UpdateTimer_Tick(object sender, EventArgs e)
        {

            if (lbRunfeedback.Text == "True")
            {
                pbRunFeedback.BackgroundImage = imgRUN;
            }
            else
            {
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
                Parent.S71500.WriteInt($"DB{DBnumber}.DBW0", value);
            }
            else if (mode == "MANUAL")
            {
                short value = 1; //Khai báo kiểu short ( tương đương int 16 bits của S7)
                Parent.S71500.WriteInt($"DB{DBnumber}.DBW0", value);
            }
        }
    }
}
