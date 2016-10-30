
using InwersjaTomograficzna.Core.Extensions;
using System.Drawing;
using System.Windows;

namespace InwersjaTomograficzna.Core.DataStructures
{
    public class Signal
    {
        private PointF _startPointF;
        public PointF StartPointF
        {
            get
            {
                return _startPointF;
            }
        }

        private PointF _endPointF;
        public PointF EndPointF
        {
            get
            {
                return _endPointF;
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

        public Signal(PointF startPointF, PointF endPointF, decimal time)
        {
            _startPointF = startPointF;
            _endPointF = endPointF;
            _time = time;
        }

        public PointF GetCrossPointFForXAxis(int xAxis)
        {
            return StartPointF.CrossPointFWithXAxis(EndPointF, xAxis);
        }

        public PointF GetCrossPointFForYAxis(int yAxis)
        {
            return StartPointF.CrossPointFWithYAxis(EndPointF, yAxis);
        }
    }
}
