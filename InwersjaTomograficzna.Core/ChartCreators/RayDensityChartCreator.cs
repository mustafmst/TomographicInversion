using InwersjaTomograficzna.Core.DataStructures;
using InwersjaTomograficzna.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms.DataVisualization.Charting;

namespace InwersjaTomograficzna.Core.ChartCreators
{
    public class RayDensityChartCreator
    {
        private ProjectionsData matrix;
        private readonly double maxValue;
        private readonly double minValue;
        private List<Color> colors;
        private readonly int numberOfColors;

        public RayDensityChartCreator(ProjectionsData matrix)
        {
            this.matrix = matrix;
            maxValue = matrix.MatrixOfEndValues.Cast<double>().Max();
            minValue = matrix.MatrixOfEndValues.Cast<double>().Min();
            numberOfColors = (int)(((maxValue - minValue) / 10) + 1);
            colors = new ColorPicker().InterpolateColors(numberOfColors);
        }

        public Chart CreateRayDensityChart(Size size)
        {
            Chart rayChart = new Chart();
            rayChart.ChartAreas.Clear();
            var CA = rayChart.ChartAreas.Add("CA");
            rayChart.Series.Clear();
            var S1 = rayChart.Series.Add("S1");
            rayChart.Legends.Clear();
            S1.ChartType = SeriesChartType.Point;
            rayChart.Legends.Add(new Legend("Legend"));
            rayChart.Size = size;
            SetMarkerSize(rayChart);
            CreateMarkers(rayChart);

            foreach (var cell in matrix.MatrixCells)
            {
                int pt = S1.Points.AddXY(cell.xIndex+1, cell.yIndex+1);
                S1.Points[pt].MarkerImage = "NI" + (int)((matrix.MatrixOfEndValues[cell.xIndex, cell.yIndex]-minValue)/10);
                
                S1.Points[pt].IsVisibleInLegend = true;
            }

            
            return rayChart;
        }

        public void SetMarkerSize(Chart chart)
        {
            int sx = chart.Size.Width / (matrix.MaxX/matrix.CellSize);
            int sy = chart.Size.Height / (matrix.MaxY / matrix.CellSize);
            chart.Series["S1"].MarkerSize = Math.Max(sx, sy);
            chart.Series["S1"].MarkerBorderWidth = 1;
            chart.Series["S1"].MarkerBorderColor = Color.Black;
        }

        public void CreateMarkers(Chart chart)
        {
            int sw = chart.Size.Width / (matrix.MaxX / matrix.CellSize);
            int sh = chart.Size.Height / (matrix.MaxY / matrix.CellSize);

            foreach(var ni in chart.Images)
            {
                ni.Dispose();
            }
            chart.Images.Clear();

            for(int i=0; i< numberOfColors; i++)
            {
                Bitmap bmp = new Bitmap(sw, sh);
                using(Graphics g = Graphics.FromImage(bmp))
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
            le.CustomItems.Add(colors[i], ((i * 10) + minValue).ToString());
        }
    }
}
