using AntColony;
using DataStructures;
using DataStructures.Extensions;
using Extensions;
using InwersjaTomograficzna.Core.ChartCreators;
using InwersjaTomograficzna.Core.DataStructures;
using InwersjaTomograficzna.Core.DataStructures.Events;
using InwersjaTomograficzna.Core.RayDensity.DataReaders;
using InwersjaTomograficzna.Core.RayDensity.DataReaders.Mocks;
using InwersjaTomograficzna.Core.TraceRouting.DataReaders.ModelReader;
using SIRT;
using System;
using System.Drawing;
using System.Windows.Forms.DataVisualization.Charting;
using InwersjaTomograficzna.Core.TraceRouting.DataReaders.RealDataReader;

namespace InwersjaTomograficzna.Core
{
    public class CoreWorker
    {
        
        private ProjectionsData matrix;
        private SirtAgorythmWorker sirtWorker;
        private Stoper stoper;
        public event IterationEventHandler resetProgressBar;
        public event IterationEventHandler updateProgressBar;
        private AlgorythmSettings settings;
        private MathMatrix<decimal> result;

        public long GetTime
        {
            get { return stoper.GetMiliseconds; }
        }

        private bool isCalculated = false;

        public bool IsCalculated
        {
            get
            {
                return isCalculated;
            }
        }

        public CoreWorker()
        {
            stoper = new Stoper();
        }

        private void ReadFile(AlgorythmSettings settings)
        {
            if (settings.IsModel)
            {
                ModelReader reader = new ModelReader(settings.InputFileName);
                SignalRoutes signals = new SignalRoutes(reader.ReadData());
                matrix = new ProjectionsData(reader.CellSize, signals, 0, reader.MaxX1, 0, reader.MaxY1);
            }
            else
            {
                RealDataReader reader = new RealDataReader(settings.InputFileName);
                SignalRoutes signals = new SignalRoutes(reader.ReadData());
                matrix = new ProjectionsData(10, signals, 0, 150, 0, 100);
            }
        }

        public void CalculateIversion(AlgorythmSettings settingsFromWindow)
        {
            settings = settingsFromWindow;
            //try
            //{
                ReadFile(settings);
            //}
            //catch (Exception e)
            //{
            //    throw e;
            //}
            matrix.MakeRayDensity();
            if (settings.Sirt) Sirt();
            if (settings.AntColony) AntColony();
        }

        public void Sirt()
        {
            settings.Signals = matrix.SignalsMatrix;
            settings.Times = matrix.TimesMatrix;
            var sirtWorker = new SirtAgorythmWorker(settings);
            sirtWorker.SubscribeStoper(stoper);
            sirtWorker.resetProgressBar += resetProgressBar;
            sirtWorker.updateProgressBar += updateProgressBar;
            result = sirtWorker.Result;
            isCalculated = true;
        }

        public void AntColony()
        {
            settings.Signals = matrix.SignalsMatrix;
            settings.Times = matrix.TimesMatrix;
            var antColonytWorker = new AntColonyWorker(settings);
            antColonytWorker.SubscribeStoper(stoper);
            antColonytWorker.resetProgressBar += resetProgressBar;
            antColonytWorker.updateProgressBar += updateProgressBar;
            result = antColonytWorker.Result;
            isCalculated = true;
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

        public Chart CreateVelocityChart(Size size)
        {
            return new VelocityChartCreator(result, matrix).CreateVelocityChart(size);
        }

        public decimal GetStatisticError()
        {
            return matrix.SignalsMatrix.Multiply(result).AverageStatisticError(matrix.TimesMatrix);
        }

        public MathMatrix<decimal> GetResultMatrix()
        {
            return result.DeepClone();
        }
    }
}
