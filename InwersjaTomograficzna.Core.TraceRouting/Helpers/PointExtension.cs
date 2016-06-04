using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace InwersjaTomograficzna.Core.RayDensity.Helpers
{
    public static class PointExtension
    {
        public static double Distance(this Point startPoint, Point endPoint)
        {
            double result = Math.Sqrt(Math.Pow(startPoint.X - endPoint.X, 2) + Math.Pow(startPoint.Y - endPoint.Y, 2));
            return result;
        }
    }
}
