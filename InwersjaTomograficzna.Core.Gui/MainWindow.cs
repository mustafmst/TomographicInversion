using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;
using InwersjaTomograficzna.Core.RayDensity.DataReaders.Mocks;
using InwersjaTomograficzna.Core.RayDensity.DataStructures;
using System.Windows.Forms.DataVisualization.Charting;
using InwersjaTomograficzna.Core.Helpers;

namespace InwersjaTomograficzna.Core.Gui
{
    public partial class MainWindow : Form
    {
        private CoreWorker worker;
        private Chart signalChart;
        private Image rayDensity;

        public MainWindow()
        {
            InitializeComponent();
            worker = new CoreWorker();
        }

        private void startToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var matrix = worker.CalculateRayDensity();

            signalChart = worker.CreateSignalsChart(matrix.AllSignals);

            signalChart.Width = SignalChartPanel.Width;
            signalChart.Height = SignalChartPanel.Height;

            signalChart.Invalidate();

            SignalChartPanel.Controls.Add(signalChart);

            rayDensity = worker.CreateRayDensityChart(matrix);

            PictureBox picBox = new PictureBox();
            picBox.Width = RayDencityAndInwersionPanel.Panel1.Width;
            picBox.Height = RayDencityAndInwersionPanel.Panel1.Height;
            picBox.Image = rayDensity.ResizeImage(picBox.Width, picBox.Height);

            RayDencityAndInwersionPanel.Panel1.Controls.Add(picBox);
        }

        private void SignalChartPanel_Resize(object sender, EventArgs e)
        {
            signalChart.Width = SignalChartPanel.Width;
            signalChart.Height = SignalChartPanel.Height;

            signalChart.Invalidate();
        }

        private void RayDencityAndInwersionPanel_Panel1_Resize(object sender, EventArgs e)
        {
            if (rayDensity != null)
            {
                PictureBox picBox = new PictureBox();
                picBox.Width = RayDencityAndInwersionPanel.Panel1.Width;
                picBox.Height = RayDencityAndInwersionPanel.Panel1.Height;
                picBox.Image = rayDensity.ResizeImage(picBox.Width, picBox.Height);

                RayDencityAndInwersionPanel.Panel1.Controls.Add(picBox);
            }
        }
    }
}
