using InwersjaTomograficzna.Core;
using InwersjaTomograficzna.Core.DataStructures;
using InwersjaTomograficzna.Core.RayDensity.DataReaders.Mocks;
using NUnit.Framework;

namespace InwerstaTomograficzna.Core.RayDensity.Tests
{
    [TestFixture]
    public class BasicTests
    {
        private object worker;

        [Test]
        public void TestTrasowaniaZMockowanymiDanymi()
        {
            SignalRoutes signals = new SignalRoutes(new MockDataReader().ReadData());
            ProjectionsData testMatrix = new ProjectionsData(2, signals, 0, 30, 0, 20);
            var valueMatrix = testMatrix.MakeRayDensity();
        }

        [Test]
        public void TestRysowania()
        {
            SignalRoutes signals = new SignalRoutes(new MockDataReader().ReadData());
            ProjectionsData testMatrix = new ProjectionsData(2, signals, 0, 30, 0, 20);
            var valueMatrix = testMatrix.MakeRayDensity();

            var worker = new CoreWorker();

            var signalChart = worker.CreateSignalsChart();
        }
    }
}
