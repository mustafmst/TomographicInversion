using DataStructures;
using InwersjaTomograficzna.Core.DataStructures;
using InwersjaTomograficzna.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;

namespace InwersjaTomograficzna.Core.ChartCreators
{
    public class VelocityChartCreator
    {
        private MathMatrix<decimal> matrix;
        private readonly decimal maxValue;
        private readonly decimal minValue;
        private List<Color> colors;
        private readonly int numberOfColors;

        public VelocityChartCreator(MathMatrix<decimal> velocityMatrix, ProjectionsData data) 
        {
            var rows = data.MatrixCells.Select(c => c.yIndex).Max()+1;
            var cols = data.MatrixCells.Select(c => c.xIndex).Max()+1;

            matrix = new MathMatrix<decimal>(cols, rows);
            for(int i=0; i < velocityMatrix.Height; i++)
            {
                var row = i / matrix.Width;
                var col = i % matrix.Width;
                matrix[row, col] = velocityMatrix[i, 0];
            }

            maxValue = 2500;
            minValue = 300;
            numberOfColors = (int)(((maxValue - minValue) / 100) + 1);
            colors = new ColorPicker().InterpolateColors(numberOfColors);
        }

        public Chart CreateVelocityChart(Size size)
        {
            Chart rayChart = new Chart();
            rayChart.ChartAreas.Clear();
            var CA = rayChart.ChartAreas.Add("CA");
            rayChart.Series.Clear();
            var S1 = rayChart.Series.Add("S1");
            rayChart.Legends.Clear();
            S1.ChartType = SeriesChartType.Point;
            rayChart.Size = size;
            rayChart.Legends.Add(new Legend("Legend"));
            SetMarkerSize(rayChart);
            CreateMarkers(rayChart);

            for (int i = 0; i < matrix.Width; i++)
            {
                for (int j = 0; j < matrix.Height; j++)
                {
                    int pt = S1.Points.AddXY(i + 1, j + 1);
                    S1.Points[pt].MarkerImage = "NI" + (int)((matrix[j,i] - minValue) / 100);
                }
            }
            return rayChart;
        }

        public void SetMarkerSize(Chart chart)
        {
            int sx = chart.Size.Width / (matrix.Width);
            int sy = chart.Size.Height / (matrix.Height);
            chart.Series["S1"].MarkerSize = Math.Max(sx, sy);
            chart.Series["S1"].MarkerBorderWidth = 1;
            chart.Series["S1"].MarkerBorderColor = Color.Black;
        }

        public void CreateMarkers(Chart chart)
        {
            int sw = chart.Size.Width / (matrix.Width);
            int sh = chart.Size.Height / (matrix.Height);

            foreach (var ni in chart.Images)
            {
                ni.Dispose();
            }
            chart.Images.Clear();

            for (int i = 0; i < numberOfColors; i++)
            {
                Bitmap bmp = new Bitmap(sw, sh);
                using (Graphics g = Graphics.FromImage(bmp))
                {
                    g.Clear(colors[i]);
                }
                chart.Images.Add(new NamedImage("NI" + i, bmp));
                AddLegendItem(chart, i);
            }
        }

        private void AddLegendItem(Chart chart, int i)
        {
            var le = chart.Legends["Legend"];
            le.CustomItems.Add(colors[i], ((i * 100) + minValue).ToString());
        }
    }
}
