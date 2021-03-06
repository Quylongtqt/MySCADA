
namespace MySCADA
{
    partial class ChartView
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
            this.LevelChart = new MindFusion.Charting.WinForms.LineChart();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // LevelChart
            // 
            this.LevelChart.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LevelChart.LegendTitle = "Legend";
            this.LevelChart.Location = new System.Drawing.Point(12, 31);
            this.LevelChart.Name = "LevelChart";
            this.LevelChart.Padding = new System.Windows.Forms.Padding(5);
            this.LevelChart.ShowLegend = true;
            this.LevelChart.Size = new System.Drawing.Size(776, 407);
            this.LevelChart.SubtitleFontName = null;
            this.LevelChart.SubtitleFontSize = null;
            this.LevelChart.SubtitleFontStyle = null;
            this.LevelChart.TabIndex = 3;
            this.LevelChart.Text = "lineChart";
            this.LevelChart.Theme.UniformSeriesFill = new MindFusion.Drawing.SolidBrush("#FF90EE90");
            this.LevelChart.Theme.UniformSeriesStroke = new MindFusion.Drawing.SolidBrush("#FF000000");
            this.LevelChart.Theme.UniformSeriesStrokeThickness = 2D;
            this.LevelChart.TitleFontName = null;
            this.LevelChart.TitleFontSize = null;
            this.LevelChart.TitleFontStyle = null;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(528, 31);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(260, 30);
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            // 
            // ChartView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.ControlBox = false;
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.LevelChart);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ChartView";
            this.ShowIcon = false;
            this.Text = "Tank Level";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private MindFusion.Charting.WinForms.LineChart LevelChart;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}