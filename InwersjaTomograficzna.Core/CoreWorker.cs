﻿using InwersjaTomograficzna.Core.ChartCreators;
using InwersjaTomograficzna.Core.RayDensity.DataReaders;
using InwersjaTomograficzna.Core.RayDensity.DataReaders.Mocks;
using InwersjaTomograficzna.Core.RayDensity.DataStructures;
using InwersjaTomograficzna.Core.TraceRouting.DataReaders.ModelReader;
using System;
using System.Drawing;
using System.Windows.Forms.DataVisualization.Charting;

namespace InwersjaTomograficzna.Core
{
    public class CoreWorker
    {
        private RoutedMatrix matrix;
        public bool IsCalculated
        {
            get
            {
                return matrix != null;
            }
        }

        public CoreWorker()
        {
            SignalRoutes signals = new SignalRoutes(new MockDataReader());
            matrix = new RoutedMatrix(2, signals, 0, 30, 0, 20);
        }

        public CoreWorker(string fileName, bool isModel)
        {
            if (isModel)
            {
                ModelReader reader = new ModelReader(fileName);
                SignalRoutes signals = new SignalRoutes(reader);
                matrix = new RoutedMatrix(reader.CellSize, signals, 0, reader.MaxX1, 0, reader.MaxY1);
            }
        }

        public void CalculateRayDensity()
        {
            matrix.MakeRayDensity();
        }

        public Chart CreateSignalsChart()
        {
            return new SignalChartCreator(matrix).CreateSignalChart();
        }

        public Image CreateRayDensityImage()
        {
            return new RayDensityImageCreator(matrix).CreateChart();
        }

        public Chart CreateRayDensityChart(Size size)
        {
            return new RayDensityChartCreator(matrix).CreateRayDensityChart(size);
        }
    }
}
