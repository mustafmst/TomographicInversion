using System.Windows;

namespace InwersjaTomograficzna.Core.RayDensity.DataStructures
{
    public class Signal
    {
        private Point _startPoint;
        public Point StartPoint
        {
            get
            {
                return _startPoint;
            }
        }

        private Point _endPoint;
        public Point EndPoint
        {
            get
            {
                return _endPoint;
            }
        }

        private decimal? _time;
        public decimal? Time
        {
            get
            {
                return _time;
            }
        }

        public Signal(Point startPoint, Point endPoint, decimal time)
        {
            _startPoint = startPoint;
            _endPoint = endPoint;
            _time = time;
        }

        public Point GetCrossPointForXAxis(int xAxis)
        {
            return new Point(xAxis,
                (((EndPoint.Y - StartPoint.Y) * (xAxis - StartPoint.X)) / (EndPoint.X - StartPoint.X)) + StartPoint.Y
                );
        }

        public Point GetCrossPointForYAxis(int yAxis)
        {
            return new Point(
                (((EndPoint.X - StartPoint.X) * (yAxis - StartPoint.Y)) / (EndPoint.Y - StartPoint.Y)) + StartPoint.X
                , yAxis);
        }
    }
}
