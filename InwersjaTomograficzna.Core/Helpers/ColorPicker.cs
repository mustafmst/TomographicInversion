using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InwersjaTomograficzna.Core.Helpers
{
    public class ColorPicker
    {
        private List<Color> baseColors;

        public ColorPicker()
        {
            baseColors = new List<Color>();
            baseColors.Add(Color.RoyalBlue);
            baseColors.Add(Color.LightSkyBlue);
            baseColors.Add(Color.LightGreen);
            baseColors.Add(Color.Yellow);
            baseColors.Add(Color.Orange);
            baseColors.Add(Color.Red);
        }

        public ColorPicker(List<Color> gradient)
        {
            baseColors = gradient;
        }

        public List<Color> InterpolateColors(int count)
        {
            List<Color> colorList = new List<Color>();

            SortedDictionary<float, Color> gradient = new SortedDictionary<float, Color>();
            for(int i=0; i<baseColors.Count; i++)
            {
                gradient.Add(1f * i / (baseColors.Count - 1), baseColors[i]);
            }

            using(Bitmap bmp = new Bitmap(count, 1))
            {
                using(Graphics g = Graphics.FromImage(bmp))
                {
                    Rectangle bmpCRect = new Rectangle(Point.Empty, bmp.Size);
                    LinearGradientBrush brush = new LinearGradientBrush(bmpCRect, Color.Empty, Color.Empty, 0, false);
                    ColorBlend colorBlend = new ColorBlend();
                    colorBlend.Positions = new float[gradient.Count];
                    for(int i = 0; i < gradient.Count; i++)
                    {
                        colorBlend.Positions[i] = gradient.ElementAt(i).Key;
                    }
                    colorBlend.Colors = gradient.Values.ToArray();
                    brush.InterpolationColors = colorBlend;
                    g.FillRectangle(brush, bmpCRect);
                    for(int i=0; i < count; i++)
                    {
                        colorList.Add(bmp.GetPixel(i, 0));
                    }
                    brush.Dispose();
                }
            }
            return colorList;
        }
    }
}
