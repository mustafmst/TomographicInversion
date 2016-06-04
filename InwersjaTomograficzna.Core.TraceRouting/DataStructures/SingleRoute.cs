using System.Windows;

namespace InwersjaTomograficzna.Core.TraceRouting.DataStructures
{
    public class SingleRoute
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

        public SingleRoute(Point startPoint, Point endPoint, decimal time)
        {
            _startPoint = startPoint;
            _endPoint = endPoint;
            _time = time;
        }
    }
}
