namespace DateTimeSeries
{
	partial class MainForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			//this.label1 = new System.Windows.Forms.Label();
			this.lineChart = new MindFusion.Charting.WinForms.LineChart();
			this.SuspendLayout();
			// 
			// label1
			// 
			//this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
   //         | System.Windows.Forms.AnchorStyles.Right)));
			//this.label1.BackColor = System.Drawing.Color.White;
			//this.label1.Location = new System.Drawing.Point(0, 512);
			//this.label1.Name = "label1";
			//this.label1.Size = new System.Drawing.Size(1125, 45);
			//this.label1.TabIndex = 1;
			//this.label1.Text = "Here we use a custom DateTimeSeries that supports adding of values dynamically. T" +
   // "he series also exposes properties for specifing the intervals of time stamps and" +
   // " the update rate.";
			// 
			// lineChart
			// 
			this.lineChart.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lineChart.LegendTitle = "Legend";
			this.lineChart.Location = new System.Drawing.Point(3, -2);
			this.lineChart.Name = "lineChart";
			this.lineChart.Padding = new System.Windows.Forms.Padding(5);
			this.lineChart.ShowLegend = true;
			this.lineChart.Size = new System.Drawing.Size(1122, 511);
			this.lineChart.SubtitleFontName = null;
			this.lineChart.SubtitleFontSize = null;
			this.lineChart.SubtitleFontStyle = null;
			this.lineChart.TabIndex = 2;
			this.lineChart.Text = "lineChart";
			this.lineChart.Theme.UniformSeriesFill = new MindFusion.Drawing.SolidBrush("#FF90EE90");
			this.lineChart.Theme.UniformSeriesStroke = new MindFusion.Drawing.SolidBrush("#FF000000");
			this.lineChart.Theme.UniformSeriesStrokeThickness = 2D;
			this.lineChart.TitleFontName = null;
			this.lineChart.TitleFontSize = null;
			this.lineChart.TitleFontStyle = null;
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1124, 553);
			this.Controls.Add(this.lineChart);
			//this.Controls.Add(this.label1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "MainForm";
			this.Text = "MindFusion.Charting Sample: Real-time DateTime Series";
			this.ResumeLayout(false);

		}

		#endregion
		//private System.Windows.Forms.Label label1;
		private MindFusion.Charting.WinForms.LineChart lineChart;
	}
}

