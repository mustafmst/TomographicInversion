using System;
using System.Drawing;
using System.Windows;

namespace InwersjaTomograficzna.Core.Extensions
{
    public static class PointFExtension
    {
        public static double Distance(this PointF startPointF, System.Drawing.PointF endPointF)
        {
            double result = Math.Sqrt(Math.Pow(startPointF.X - endPointF.X, 2) + Math.Pow(startPointF.Y - endPointF.Y, 2));
            return result;
        }

        public static bool IsBetweenTwoPointFs(this PointF srcPointF, PointF PointF1, PointF PointF2)
        {
            if(srcPointF.X >= PointF1.X && srcPointF.X <= PointF2.X)
            {
                if (srcPointF.Y >= PointF1.Y && srcPointF.Y <= PointF2.Y) return true;
                if (srcPointF.Y >= PointF2.Y && srcPointF.Y <= PointF1.Y) return true;
            }
            if (srcPointF.X >= PointF2.X && srcPointF.X <= PointF1.X)
            {
                if (srcPointF.Y >= PointF1.Y && srcPointF.Y <= PointF2.Y) return true;
                if (srcPointF.Y >= PointF2.Y && srcPointF.Y <= PointF1.Y) return true;
            }

            return false;
        }

        public static PointF CrossPointFWithXAxis(this PointF StartPointF, PointF EndPointF, int xAxis)
        {
            if (xAxis == StartPointF.X) return StartPointF;
            if (xAxis == EndPointF.X) return EndPointF;
            //if (StartPointF.X == EndPointF.X) return new PointF(xAxis, StartPointF.X);
            return new PointF(xAxis,
                (((EndPointF.Y - StartPointF.Y) * (xAxis - StartPointF.X)) / (EndPointF.X - StartPointF.X)) + StartPointF.Y
                );
        }

        public static PointF CrossPointFWithYAxis(this PointF StartPointF, PointF EndPointF, int yAxis)
        {
            if (yAxis == StartPointF.Y) return StartPointF;
            if (yAxis == EndPointF.Y) return EndPointF;
            //if (StartPointF.Y == EndPointF.Y) return new PointF(StartPointF.Y, yAxis);
            return new PointF(
                (((EndPointF.X - StartPointF.X) * (yAxis - StartPointF.Y)) / (EndPointF.Y - StartPointF.Y)) + StartPointF.X
                , yAxis);
        }

        public static PointF CenterBetweenThisAndAnotherPointF(this PointF PointF1, PointF PointF2)
        {
            return new PointF((PointF1.X + PointF2.X) / 2, (PointF1.Y + PointF2.Y) / 2);
        }
    }
}
