
using InwersjaTomograficzna.Core.Extensions;
using System.Drawing;
using System.Windows;

namespace InwersjaTomograficzna.Core.DataStructures
{
    public class Signal
    {
        private PointF _startPoint;
        public PointF StartPoint
        {
            get
            {
                return _startPoint;
            }
        }

        private PointF _endPoint;
        public PointF EndPoint
        {
            get
            {
                return _endPoint;
            }
        }

        private decimal _time;
        public decimal Time
        {
            get
            {
                return _time;
            }
        }

        public Signal(PointF startPoint, PointF endPoint, decimal time)
        {
            _startPoint = startPoint;
            _endPoint = endPoint;
            _time = time;
        }

        public Signal(PointF startPoint, PointF endPoint)
        {
            _startPoint = startPoint;
            _endPoint = endPoint;
        }

        public PointF GetCrossPointFForXAxis(int xAxis)
        {
            return StartPoint.CrossPointFWithXAxis(EndPoint, xAxis);
        }

        public PointF GetCrossPointFForYAxis(int yAxis)
        {
            return StartPoint.CrossPointFWithYAxis(EndPoint, yAxis);
        }
    }
}
