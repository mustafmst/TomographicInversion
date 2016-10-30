
using InwersjaTomograficzna.Core.Extensions;
using System.Drawing;
using System.Windows;

namespace InwersjaTomograficzna.Core.DataStructures
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
            return StartPoint.CrossPointWithXAxis(EndPoint, xAxis);
        }

        public Point GetCrossPointForYAxis(int yAxis)
        {
            return StartPoint.CrossPointWithYAxis(EndPoint, yAxis);
        }
    }
}
