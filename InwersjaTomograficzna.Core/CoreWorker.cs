using DataStructures;
using Extensions;
using InwersjaTomograficzna.Core.ChartCreators;
using InwersjaTomograficzna.Core.DataStructures;
using InwersjaTomograficzna.Core.RayDensity.DataReaders;
using InwersjaTomograficzna.Core.RayDensity.DataReaders.Mocks;
using InwersjaTomograficzna.Core.TraceRouting.DataReaders.ModelReader;
using SIRT;
using System.Drawing;
using System.Windows.Forms.DataVisualization.Charting;

namespace InwersjaTomograficzna.Core
{
    public class CoreWorker
    {
        private ProjectionsData matrix;
        private SirtAgorythmWorker sirtWorker;
        private Stoper stoper;
        ModelReader reader;

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
            SignalRoutes signals = new SignalRoutes(new MockDataReader().ReadData());
            matrix = new ProjectionsData(2, signals, 0, 30, 0, 20);
            stoper = new Stoper();
        }

        public CoreWorker(string fileName, bool isModel)
        {
            if (isModel)
            {
                reader = new ModelReader(fileName);
                SignalRoutes signals = new SignalRoutes(reader.ReadData());
                matrix = new ProjectionsData(reader.CellSize, signals, 0, reader.MaxX1, 0, reader.MaxY1);
            }

            stoper = new Stoper();
        }

        public void CalculateIversion()
        {
            matrix.MakeRayDensity();
            sirtWorker = new SirtAgorythmWorker(matrix.SignalsMatrix, matrix.TimesMatrix, 100);
            sirtWorker.SubscribeStoper(stoper);
            var res = sirtWorker.Result;
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
            return new VelocityChartCreator(sirtWorker.Result, matrix).CreateVelocityChart(size);
        }

        public decimal GetStatisticError()
        {
            return sirtWorker.Result.AverageStatisticError(reader.GetRealVelocities());
        }
    }
}
