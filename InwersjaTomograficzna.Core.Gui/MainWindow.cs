using DataStructures;
using InwersjaTomograficzna.Core.DataStructures.Events;
using System;
using System.Drawing;
using System.Threading;
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
        private string InputFile;
        private bool IsModel;

        public MainWindow()
        {
            InitializeComponent();
        }

        #region MyEventHandling
        private void ResetProgressBar(IterationArgument e)
        {
            this.Invoke((MethodInvoker)delegate
            {
                progressBar.Maximum = e.Value;
                progressBar.Step = 1;
                progressBar.Value = 0;
            });
        }

        private void UpdateProgressBar(IterationArgument e)
        {
            this.Invoke((MethodInvoker)delegate
            {
                progressBar.Value = e.Value;
            });
        }
        #endregion

        #region ChartMethods
        private void HandleVelocityChart()
        {
            velocityChart = worker.CreateVelocityChart(new Size(
                            RayDencityAndInwersionPanel.Panel2.Width,
                            RayDencityAndInwersionPanel.Panel2.Height
                            ));
            RayDencityAndInwersionPanel.Panel2.Controls.Clear();
            RayDencityAndInwersionPanel.Panel2.Controls.Add(velocityChart);
        }

        private void HandleRayDensityChart()
        {
            rayDensityChart = worker.CreateRayDensityChart(new Size(
                            RayDencityAndInwersionPanel.Panel1.Width,
                            RayDencityAndInwersionPanel.Panel1.Height
                            ));
            RayDencityAndInwersionPanel.Panel1.Controls.Clear();
            RayDencityAndInwersionPanel.Panel1.Controls.Add(rayDensityChart);
        }

        private void HandleSignalChart()
        {
            signalChart = worker.CreateSignalsChart();
            signalChart.Width = SignalChartPanel.Width;
            signalChart.Height = SignalChartPanel.Height;
            signalChart.Invalidate();
            SignalChartPanel.Controls.Clear();
            SignalChartPanel.Controls.Add(signalChart);
        }
        #endregion

        #region WinFormEventsHandling
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
            if (openFileDialog1.FileName == null || openFileDialog1.FileName=="" )
            {
                startToolStripMenuItem.Enabled = false;
                return;
            }
            else
            {
                startToolStripMenuItem.Enabled = true;
                InputFile = openFileDialog1.FileName;
                worker = new CoreWorker();
                Text = "Inwersja Tomograficzna | " + openFileDialog1.FileName;
                SetWorkingStatus("Plik wczytany poprawnie. Można zaczynać.");
            }
            IsModel = true;
        }

        private void wczytajDaneToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.Filter = "Text Files (.txt)|*.txt|All Files (*.*)|*.*";
            openFileDialog1.FilterIndex = 1;

            openFileDialog1.ShowDialog();
            if (openFileDialog1.FileName == null || openFileDialog1.FileName == "")
            {
                startToolStripMenuItem.Enabled = false;
                return;
            }
            else
            {
                startToolStripMenuItem.Enabled = true;
                InputFile = openFileDialog1.FileName;
                worker = new CoreWorker();
                Text = "Inwersja Tomograficzna | " + openFileDialog1.FileName;
                SetWorkingStatus("Plik wczytany poprawnie. Można zaczynać.");
            }
            IsModel = false;
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

        private void startToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                AlgorythmSettings settings = new AlgorythmSettings
                {
                    IsModel = IsModel,
                    InputFileName = InputFile,
                    AntNumber = (int)antsNumeric.Value,
                    Iterations = (int)iterationsNumeric.Value,
                    Sirt = SirtRadioBtn.Checked,
                    AntColony = AntsRadioBtn.Checked
                };
                statisticsTree.Nodes.Clear();
                resultTree.Nodes.Clear();
                if (worker == null) return;
                worker.resetProgressBar += ResetProgressBar;
                worker.updateProgressBar += UpdateProgressBar;
                ThreadWorker threadWorker = new ThreadWorker(() => worker.CalculateIversion(settings));
                threadWorker.ThreadDone += CalculationThreadDone;
                SetWorkingStatus("Trwa obliczanie...");
                przetwarzanieToolStripMenuItem.Enabled = false;
                Thread thread = new Thread(threadWorker.Run);
                thread.Start();
            }
            catch(Exception fileException)
            {
                MessageBox.Show(fileException.Message,fileException.GetType().Name,MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }
        }
        #endregion

        #region OtherMethods
        private void SetWorkingStatus(string status)
        {
            this.workStatus.Text = status;
        }

        private void UpdateTimeStats(long time)
        {
            var timeNode = new TreeNode("Czas potrzebny do obliczenia wyniku");
            timeNode.Nodes.Add(new TreeNode(time.ToString()));
            statisticsTree.Nodes.Add(timeNode);
        }

        private void UpdateAverageStatisticError()
        {
            var timeNode = new TreeNode("Błąd statystyczny");
            var error = worker.GetStatisticError();
            timeNode.Nodes.Add(new TreeNode(error.ToString()));
            statisticsTree.Nodes.Add(timeNode);
        }

        private void CalculationThreadDone(object sender, EventArgs e)
        {
            this.Invoke((MethodInvoker)delegate
            {
                SetWorkingStatus("Zrobione!");
                PostCalculationWork();
                przetwarzanieToolStripMenuItem.Enabled = true;
            });
        }

        private void PostCalculationWork()
        {
            HandleSignalChart();
            HandleRayDensityChart();
            HandleVelocityChart();

            PrintResultMatrix();

            UpdateTimeStats(worker.GetTime);
            UpdateAverageStatisticError();
            progressBar.Value = 0;
        }

        private void PrintResultMatrix()
        {
            var resNode = new TreeNode("Resurl Matrix");
            var result = worker.GetResultMatrix();

            for(int i = 0; i < result.Height; i++)
            {
                resNode.Nodes.Add(((int)result[i, 0]).ToString());
            }

            resultTree.Nodes.Add(resNode);
        }
        #endregion

        
    }
}
