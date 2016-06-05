using InwersjaTomograficzna.Core.RayDensity.DataStructures;
using System;
using System.Drawing;
using System.Windows.Forms.DataVisualization.Charting;

namespace InwersjaTomograficzna.Core.ChartCreators
{
    public class SignalChartCreator
    {
        private RoutedMatrix matrix;

        public SignalChartCreator(RoutedMatrix matrix)
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
                var series = new Series(String.Format("({0} ; {1}) - ({2} ; {3})", signal.StartPoint.X, signal.StartPoint.Y, signal.EndPoint.X, signal.EndPoint.Y));
                series.Points.AddXY(signal.StartPoint.X, signal.StartPoint.Y);
                series.Points.AddXY(signal.EndPoint.X, signal.EndPoint.Y);
                series.ChartType = SeriesChartType.FastLine;
                series.Color = Color.Blue;
                signalChart.Series.Add(series);
            }

            return signalChart;
        }
    }
}
