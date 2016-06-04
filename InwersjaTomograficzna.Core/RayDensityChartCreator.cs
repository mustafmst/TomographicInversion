using InwersjaTomograficzna.Core.RayDensity.DataStructures;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace InwersjaTomograficzna.Core
{
    public class RayDensityChartCreator
    {
        private readonly RoutedMatrix matrix;
        private readonly double maxValue;
        private readonly double minValue;


        public RayDensityChartCreator(RoutedMatrix matrix)
        {
            this.matrix = matrix;
            maxValue = matrix.MatrixOfEndValues.Cast<double>().Max();
            minValue = matrix.MatrixOfEndValues.Cast<double>().Min();
        }


        public Image CreateChart()
        {
            Bitmap image = new Bitmap(matrix.MaxX*50, matrix.MaxY*50);

            foreach(var cell in matrix.MatrixCells)
            {
                DrawCell(cell, GetColorForValue(matrix.MatrixOfEndValues[cell.xIndex, cell.yIndex]), image);
            }

            foreach (var cell in matrix.MatrixCells)
            {
                var g = Graphics.FromImage(image);
                g.DrawString(((int)matrix.MatrixOfEndValues[cell.xIndex, cell.yIndex]).ToString(), new Font(FontFamily.GenericSansSerif, 12), Brushes.Black, new PointF(cell.leftBoarder * 50, cell.lowerBoarder * 50));
            }
            image.Save(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location).ToString()+"\\debugImage.bmp");
            return image;
        }

        private void DrawCell(Cell cell, Color color, Bitmap image)
        {
            for(int x=cell.leftBoarder*50+1; x<cell.rightBoarder*50; x++)
            {
                for(int y=cell.lowerBoarder*50 +1; y < cell.upperBoarder*50; y++)
                {
                    image.SetPixel(x, y, color);

                }
            }

            
        }

        private Color GetColorForValue(double value)
        {
            var percent = ((value-minValue) / (maxValue-minValue)) * 100;

            if (percent > 90) return Color.Red;
            if (percent > 80) return Color.DarkRed;
            if (percent > 70) return Color.Purple;
            if (percent > 60) return Color.MediumPurple;
            if (percent > 50) return Color.Yellow;
            if (percent > 40) return Color.YellowGreen;
            if (percent > 30) return Color.LightGreen;
            if (percent > 20) return Color.DarkKhaki;
            if (percent > 10) return Color.DarkGreen;
            return Color.DarkOliveGreen;
        }
        
    }
}
