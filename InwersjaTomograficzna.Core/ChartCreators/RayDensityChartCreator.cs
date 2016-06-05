using InwersjaTomograficzna.Core.Helpers;
using InwersjaTomograficzna.Core.RayDensity.DataStructures;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InwersjaTomograficzna.Core.ChartCreators
{
    public class RayDensityChartCreator
    {
        private RoutedMatrix matrix;
        private readonly double maxValue;
        private readonly double minValue;
        private List<Color> colors;

        public RayDensityChartCreator(RoutedMatrix matrix)
        {
            this.matrix = matrix;
            maxValue = matrix.MatrixOfEndValues.Cast<double>().Max();
            minValue = matrix.MatrixOfEndValues.Cast<double>().Min();
            colors = new ColorPicker().InterpolateColors((int)(((maxValue - minValue) / 10) + 1));
        }


    }
}
