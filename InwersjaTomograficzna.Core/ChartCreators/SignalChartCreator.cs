using InwersjaTomograficzna.Core.DataStructures;
using System;
using System.Drawing;
using System.Windows.Forms.DataVisualization.Charting;

namespace InwersjaTomograficzna.Core.ChartCreators
{
    public class SignalChartCreator
    {
        private Matrix matrix;

        public SignalChartCreator(Matrix matrix)
        {
            this.matrix = matrix;
        }

        public Chart CreateSignalChart()
        {
            var signalChart = new Chart();
            signalChart.Titles.Add("signals");

            var chartArea = new ChartArea("ChartArea");
            chartArea.AxisX.Minimum = 0;
            chartArea.AxisX.Maximum = matrix.MaxX;
            chartArea.AxisY.Minimum = 0;
            chartArea.AxisY.Maximum = matrix.MaxY;

            signalChart.ChartAreas.Add(chartArea);
            signalChart.Series.Clear();

            foreach (var signal in matrix.AllSignals.AllRoutes)
            {
                var series = new Series(String.Format("({0} ; {1}) - ({2} ; {3})", signal.StartPointF.X, signal.StartPointF.Y, signal.EndPointF.X, signal.EndPointF.Y));
                series.Points.AddXY(signal.StartPointF.X, signal.StartPointF.Y);
                series.Points.AddXY(signal.EndPointF.X, signal.EndPointF.Y);
                series.ChartType = SeriesChartType.FastLine;
                series.Color = Color.Blue;
                signalChart.Series.Add(series);
            }

            return signalChart;
        }
    }
}
