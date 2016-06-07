using System;
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

        public static bool IsBetweenTwoPoints(this Point srcPoint, Point point1, Point point2)
        {
            if(srcPoint.X >= point1.X && srcPoint.X <= point2.X)
            {
                if (srcPoint.Y >= point1.Y && srcPoint.Y <= point2.Y) return true;
                if (srcPoint.Y >= point2.Y && srcPoint.Y <= point1.Y) return true;
            }
            if (srcPoint.X >= point2.X && srcPoint.X <= point1.X)
            {
                if (srcPoint.Y >= point1.Y && srcPoint.Y <= point2.Y) return true;
                if (srcPoint.Y >= point2.Y && srcPoint.Y <= point1.Y) return true;
            }

            return false;
        }

        public static Point CrossPointWithXAxis(this Point StartPoint, Point EndPoint, int xAxis)
        {
            return new Point(xAxis,
                (((EndPoint.Y - StartPoint.Y) * (xAxis - StartPoint.X)) / (EndPoint.X - StartPoint.X)) + StartPoint.Y
                );
        }

        public static Point CrossPointWithYAxis(this Point StartPoint, Point EndPoint, int yAxis)
        {
            return new Point(
                (((EndPoint.X - StartPoint.X) * (yAxis - StartPoint.Y)) / (EndPoint.Y - StartPoint.Y)) + StartPoint.X
                , yAxis);
        }

        public static Point CenterBetweenThisAndAnotherPoint(this Point point1, Point point2)
        {
            return new Point((point1.X + point2.X) / 2, (point1.Y + point2.Y) / 2);
        }
    }
}
