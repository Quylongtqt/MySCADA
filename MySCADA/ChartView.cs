using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using MindFusion.Charting;
using Brush = MindFusion.Drawing.Brush;
using SolidBrush = MindFusion.Drawing.SolidBrush;

namespace MySCADA
{
    public partial class ChartView : Form
    {
        int Period;
        Timer UpdateTimer = null; //Windows form Timer
        public SCADA Parent;
        public int TankLevel;
        MyDateTimeSeries LineLevel;
        Button btCloseX;
        Image img = Image.FromFile("image.png");
        public ChartView(int period)
        {
            InitializeComponent();
            Period = period;

            // Buton Close X:
            btCloseX = new Button();
            btCloseX.Text = "X";
            btCloseX.Size = new Size(20, 20);
            btCloseX.Location = new Point(775, 5);
            btCloseX.BackColor = Color.Red;
            btCloseX.Click += btCloseX_Click;

            this.SuspendLayout();

            LineLevel = new MyDateTimeSeries(DateTime.Now, DateTime.Now, DateTime.Now.AddMinutes(1));
            LineLevel.DateTimeFormat = DateTimeFormat.LongTime;

            LineLevel.LabelInterval = 10;
            LineLevel.MinValue = 0;
            LineLevel.MaxValue = 120;
            LineLevel.Title = "Tank 1";
            LineLevel.SupportedLabels = LabelKinds.XAxisLabel;

            // setup chart
            LevelChart.Series.Add(LineLevel);
            LevelChart.Title = "Real-time Tank Level";
            LevelChart.ShowXCoordinates = false;
            LevelChart.ShowLegendTitle = false;
            LevelChart.LayoutPanel.Margin = new Margins(0, 0, 20, 0);

            LevelChart.XAxis.Title = "Time";
            LevelChart.XAxis.MinValue = 0;
            LevelChart.XAxis.MaxValue = 120;
            LevelChart.XAxis.Interval = 10;

            LevelChart.YAxis.MinValue = 0;
            LevelChart.YAxis.MaxValue = 100;
            LevelChart.YAxis.Interval = 10;
            LevelChart.YAxis.Title = "Tank Level(%)";

            List<Brush> brushes = new List<Brush>()
            {
                new SolidBrush(Color.Red)
            };

            List<double> thicknesses = new List<double>() { 2 };

            PerSeriesStyle style = new PerSeriesStyle(brushes, brushes, thicknesses, null);
            LevelChart.Plot.SeriesStyle = style;
            LevelChart.Theme.PlotBackground = new SolidBrush(Color.White);
            LevelChart.Theme.GridLineColor = Color.LightGray;
            LevelChart.Theme.GridLineStyle = System.Drawing.Drawing2D.DashStyle.Dash;
            LevelChart.TitleMargin = new Margins(10);
            LevelChart.GridType = GridType.Horizontal;

            // Timer
            UpdateTimer = new Timer();
            UpdateTimer.Interval = Period;
            UpdateTimer.Tick += UpdateTimer_Tick;
            UpdateTimer.Start();
            base.Controls.Add(btCloseX);
            pictureBox1.BackgroundImage = img;
        }
        private void btCloseX_Click(object sender, EventArgs e)
        {
            base.Close();
        }
        private void UpdateTimer_Tick(object sender, EventArgs e)
        {
            LineLevel.addValue(TankLevel);
            if (LineLevel.Size > 1)
            {
                double currVal = LineLevel.GetValue(LineLevel.Size - 1, 0);

                if (currVal > LevelChart.XAxis.MaxValue)
                {
                    double span = currVal - LineLevel.GetValue(LineLevel.Size - 2, 0);
                    LevelChart.XAxis.MinValue += span;
                    LevelChart.XAxis.MaxValue += span;

                }
                LevelChart.ChartPanel.InvalidateLayout();
            }
        }
    }
}
