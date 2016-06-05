using System;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using InwersjaTomograficzna.Core.Helpers;

namespace InwersjaTomograficzna.Core.Gui
{
    public partial class MainWindow : Form
    {
        private CoreWorker worker;
        private Chart signalChart;
        private Image rayDensity;
        private Chart rayDensityChart;

        public MainWindow()
        {
            InitializeComponent();
            worker = new CoreWorker();
        }

        private void startToolStripMenuItem_Click(object sender, EventArgs e)
        {
            worker.CalculateRayDensity();

            signalChart = worker.CreateSignalsChart();

            signalChart.Width = SignalChartPanel.Width;
            signalChart.Height = SignalChartPanel.Height;

            signalChart.Invalidate();

            SignalChartPanel.Controls.Add(signalChart);

            /*rayDensity = worker.CreateRayDensityImage();

            PictureBox picBox = new PictureBox();
            picBox.Width = RayDencityAndInwersionPanel.Panel1.Width;
            picBox.Height = RayDencityAndInwersionPanel.Panel1.Height;
            picBox.Image = rayDensity.ResizeImage(picBox.Width, picBox.Height);

            RayDencityAndInwersionPanel.Panel1.Controls.Add(picBox);*/
            rayDensityChart = worker.CreateRayDensityChart(new Size(
                RayDencityAndInwersionPanel.Panel1.Width,
                RayDencityAndInwersionPanel.Panel1.Height
                ));
            RayDencityAndInwersionPanel.Panel1.Controls.Add(rayDensityChart);

        }

        private void SignalChartPanel_Resize(object sender, EventArgs e)
        {
            if (signalChart != null)
            {
                signalChart.Width = SignalChartPanel.Width;
                signalChart.Height = SignalChartPanel.Height;

                signalChart.Invalidate();
            }
        }

        private void RayDencityAndInwersionPanel_Panel1_Resize(object sender, EventArgs e)
        {
            if (/*rayDensity*/ worker != null)
            {
                //PictureBox picBox = new PictureBox();
                //picBox.Width = RayDencityAndInwersionPanel.Panel1.Width;
                //picBox.Height = RayDencityAndInwersionPanel.Panel1.Height;
                //picBox.Image = rayDensity.ResizeImage(picBox.Width, picBox.Height);
                //
                //RayDencityAndInwersionPanel.Panel1.Controls.Add(picBox);
                rayDensityChart = worker.CreateRayDensityChart(new Size(
                RayDencityAndInwersionPanel.Panel1.Width,
                RayDencityAndInwersionPanel.Panel1.Height
                ));
                RayDencityAndInwersionPanel.Panel1.Controls.Clear();
                RayDencityAndInwersionPanel.Panel1.Controls.Add(rayDensityChart);
            }
        }
    }
}
