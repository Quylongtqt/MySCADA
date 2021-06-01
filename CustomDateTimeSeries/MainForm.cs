using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections.ObjectModel;

using MindFusion.Charting;
using Brush = MindFusion.Drawing.Brush;
using SolidBrush = MindFusion.Drawing.SolidBrush;


namespace DateTimeSeries
{
	public partial class MainForm : Form
	{
		Timer myTimer = new Timer();
		Random random = new Random();
	
		MyDateTimeSeries series1, series2, series3;

		public MainForm()
		{
			InitializeComponent();

			// create sample data
			ObservableCollection<Series> data = new ObservableCollection<Series>();

			lineChart.LicenseKey = "license key stays here";
			
			series1 = new MyDateTimeSeries(DateTime.Now, DateTime.Now, DateTime.Now.AddMinutes(1));
			series1.DateTimeFormat = DateTimeFormat.LongTime;
		//	series1.DateTimeFormat = DateTimeFormat.CustomDateTime;
		//	series1.CustomDateTimeFormat = "mm:ss";
			//how many values will be added before a time stamp is rendered at the axis
			series1.LabelInterval = 10;
			series1.MinValue = 0;
			series1.MaxValue = 120;
			series1.Title = "Server 1";
			series1.SupportedLabels = LabelKinds.XAxisLabel;

			series2 = new MyDateTimeSeries(DateTime.Now, DateTime.Now, DateTime.Now.AddMinutes(1));
			series2.DateTimeFormat = DateTimeFormat.LongTime;			
			series2.LabelInterval = 10;
			series2.MinValue = 0;
			series2.MaxValue = 120;
			series2.Title = "Server 2";
			series2.SupportedLabels = LabelKinds.None;

			series3 = new MyDateTimeSeries(DateTime.Now, DateTime.Now, DateTime.Now.AddMinutes(1));
			series3.DateTimeFormat = DateTimeFormat.LongTime;
			series3.LabelInterval = 10;
			series3.MinValue = 0;
			series3.MaxValue = 120;
			series3.Title = "Server 3";
			series3.SupportedLabels = LabelKinds.None;


			// setup chart
			lineChart.Series.Add(series1);
			lineChart.Series.Add(series2);
			lineChart.Series.Add(series3);
			lineChart.Title = "Real-time Server Load";
			lineChart.ShowXCoordinates = false;
			lineChart.ShowLegendTitle = false;
			lineChart.LayoutPanel.Margin = new Margins(0,0,20,0);

			lineChart.XAxis.Title = "";
			lineChart.XAxis.MinValue = 0;
			lineChart.XAxis.MaxValue = 120;
			lineChart.XAxis.Interval = 10;

			lineChart.YAxis.MinValue = 0;
			lineChart.YAxis.MaxValue = 100;
			lineChart.YAxis.Interval = 10;
			lineChart.YAxis.Title = "Current Requests\n(in thousands)";			

			List<Brush> brushes = new List<Brush>()
			{
				new SolidBrush(Color.Red),
				new SolidBrush(Color.Orange),
				new SolidBrush(Color.SeaGreen)
			};

			List<double> thicknesses = new List<double>() { 2 };			

			PerSeriesStyle style = new PerSeriesStyle(brushes, brushes, thicknesses, null);
			lineChart.Plot.SeriesStyle = style;
			lineChart.Theme.PlotBackground = new SolidBrush(Color.White);
			lineChart.Theme.GridLineColor = Color.LightGray;
			lineChart.Theme.GridLineStyle = System.Drawing.Drawing2D.DashStyle.Dash;
			lineChart.TitleMargin = new Margins(10);
			lineChart.GridType = GridType.Horizontal;

			myTimer.Tick += new EventHandler(TimerEventProcessor);

			// Sets the timer interval to half a seconds.
			myTimer.Interval = 500;
			myTimer.Start();
		}

		// This is the method to run when the timer is raised.
		private void TimerEventProcessor(Object myObject,
												EventArgs myEventArgs)
		{
			double val = random.NextDouble() * 10 + 10;
			series1.addValue(val);

			val = random.NextDouble() * 10 + 40;
			series2.addValue(val);

			val = random.NextDouble() * 10 + 60;
			series3.addValue(val);
			Console.WriteLine(val);

			if (series1.Size > 1)
			{
				double currVal = series1.GetValue(series1.Size - 1, 0);

				if (currVal > lineChart.XAxis.MaxValue)
				{
					double span = currVal - series1.GetValue(series1.Size - 2, 0);
					lineChart.XAxis.MinValue += span;
					lineChart.XAxis.MaxValue += span;

				}
				lineChart.ChartPanel.InvalidateLayout();
			}
			
		}
	}
}