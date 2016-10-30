using InwersjaTomograficzna.Core.DataStructures;
using InwersjaTomograficzna.Core.Extensions;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;

namespace InwersjaTomograficzna.Core.ChartCreators
{
    public class RayDensityImageCreator
    {
        private readonly Matrix matrix;
        private readonly double maxValue;
        private readonly double minValue;
        private const int multiplaier = 50;
        private readonly List<Color> colors;


        public RayDensityImageCreator(Matrix matrix)
        {
            this.matrix = matrix;
            maxValue = matrix.MatrixOfEndValues.Cast<double>().Max();
            minValue = matrix.MatrixOfEndValues.Cast<double>().Min();
            colors = new ColorPicker().InterpolateColors((int)(((maxValue-minValue)/10)+1));

        }


        public Image CreateChart()
        {
            Bitmap image = new Bitmap(matrix.MaxX*multiplaier, matrix.MaxY*multiplaier);

            foreach(var cell in matrix.MatrixCells)
            {
                DrawCell(cell, colors[(int)(matrix.MatrixOfEndValues[cell.xIndex, cell.yIndex]-minValue)/10], image);
            }

            foreach (var cell in matrix.MatrixCells)
            {
                var g = Graphics.FromImage(image);
                g.DrawString(((int)matrix.MatrixOfEndValues[cell.xIndex, cell.yIndex]).ToString(), new Font(FontFamily.GenericSansSerif, 12), Brushes.Black, new PointF((cell.leftBoarder * multiplaier)+multiplaier, ((matrix.MaxY * multiplaier) - (cell.lowerBoarder * multiplaier)) - multiplaier));
            }
            image.Save(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location).ToString()+"\\debugImage.bmp");
            return image;
        }

        private void DrawCell(Cell cell, Color color, Bitmap image)
        {
            for(int x=cell.leftBoarder*multiplaier+1; x<cell.rightBoarder*multiplaier; x++)
            {
                for(int y=cell.lowerBoarder*multiplaier +1; y < cell.upperBoarder*multiplaier; y++)
                {
                    image.SetPixel(x, (matrix.MaxY * multiplaier) - y, color);

                }
            }

            
        }
        
    }
}
