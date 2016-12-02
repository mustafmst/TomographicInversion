using System;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace InwersjaTomograficzna.Core.Gui
{
    public partial class MainWindow : Form
    {
        private CoreWorker worker;
        private Chart signalChart;
        private Chart rayDensityChart;
        private Chart velocityChart;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void startToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetWorkingStatus(true);
            if (worker == null) return;
            worker.CalculateRayDensity();
            SetWorkingStatus(false);

            signalChart = worker.CreateSignalsChart();
            signalChart.Width = SignalChartPanel.Width;
            signalChart.Height = SignalChartPanel.Height;
            signalChart.Invalidate();
            SignalChartPanel.Controls.Clear();
            SignalChartPanel.Controls.Add(signalChart);

            rayDensityChart = worker.CreateRayDensityChart(new Size(
                RayDencityAndInwersionPanel.Panel1.Width,
                RayDencityAndInwersionPanel.Panel1.Height
                ));
            RayDencityAndInwersionPanel.Panel1.Controls.Clear();
            RayDencityAndInwersionPanel.Panel1.Controls.Add(rayDensityChart);

            velocityChart = worker.CreateVelocityChart(new Size(
                RayDencityAndInwersionPanel.Panel2.Width,
                RayDencityAndInwersionPanel.Panel2.Height
                ));
            RayDencityAndInwersionPanel.Panel2.Controls.Clear();
            RayDencityAndInwersionPanel.Panel2.Controls.Add(velocityChart);
        }

        private void SignalChartPanel_Resize(object sender, EventArgs e)
        {
            if (signalChart != null)
            {
                signalChart = worker.CreateSignalsChart();
                signalChart.Width = SignalChartPanel.Width;
                signalChart.Height = SignalChartPanel.Height;
                signalChart.Invalidate();
                SignalChartPanel.Controls.Clear();
                SignalChartPanel.Controls.Add(signalChart);
            }
        }

        private void RayDencityAndInwersionPanel_Panel1_Resize(object sender, EventArgs e)
        {
            if (worker != null && worker.IsCalculated)
            {
                rayDensityChart = worker.CreateRayDensityChart(new Size(
                RayDencityAndInwersionPanel.Panel1.Width,
                RayDencityAndInwersionPanel.Panel1.Height
                ));
                RayDencityAndInwersionPanel.Panel1.Controls.Clear();
                RayDencityAndInwersionPanel.Panel1.Controls.Add(rayDensityChart);
            }
        }

        private void mockDataMenu_Click(object sender, EventArgs e)
        {
            worker = new CoreWorker();
        }

        private void wczytajModelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.Filter = "Text Files (.txt)|*.txt|All Files (*.*)|*.*";
            openFileDialog1.FilterIndex = 1;

            openFileDialog1.ShowDialog();

            worker = new CoreWorker(openFileDialog1.FileName, true);
            
        }

        private void RayDencityAndInwersionPanel_Panel2_Resize(object sender, EventArgs e)
        {
            if (worker != null && worker.IsCalculated)
            {
                velocityChart = worker.CreateVelocityChart(new Size(
                RayDencityAndInwersionPanel.Panel2.Width,
                RayDencityAndInwersionPanel.Panel2.Height
                ));
                RayDencityAndInwersionPanel.Panel2.Controls.Clear();
                RayDencityAndInwersionPanel.Panel2.Controls.Add(velocityChart);
            }
        }

        private void SetWorkingStatus(bool status)
        {
            this.workStatus.Text = status ? "Working..." : "Done";
        }
    }
}
