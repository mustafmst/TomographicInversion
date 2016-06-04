using InwersjaTomograficzna.Core;
using InwersjaTomograficzna.Core.RayDensity.DataReaders.Mocks;
using InwersjaTomograficzna.Core.RayDensity.DataStructures;
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
            SignalRoutes signals = new SignalRoutes(new MockDataReader());
            RoutedMatrix testMatrix = new RoutedMatrix(2, signals, 0, 30, 0, 20);
            var valueMatrix = testMatrix.MakeRayDensity();
        }

        [Test]
        public void TestRysowania()
        {
            SignalRoutes signals = new SignalRoutes(new MockDataReader());
            RoutedMatrix testMatrix = new RoutedMatrix(2, signals, 0, 30, 0, 20);
            var valueMatrix = testMatrix.MakeRayDensity();

            var worker = new CoreWorker();

            var signalChart = worker.CreateSignalsChart(testMatrix);
        }
    }
}
