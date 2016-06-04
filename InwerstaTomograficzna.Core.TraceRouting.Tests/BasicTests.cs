using InwersjaTomograficzna.Core.RayDensity.DataReaders.Mocks;
using InwersjaTomograficzna.Core.RayDensity.DataStructures;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InwerstaTomograficzna.Core.RayDensity.Tests
{
    [TestFixture]
    public class BasicTests
    {
        [Test]
        public void TestTrasowaniaZMockowanymiDanymi()
        {
            SignalRoutes signals = new SignalRoutes(new MockDataReader());
            RoutedMatrix testMatrix = new RoutedMatrix(2, signals, 0, 30, 0, 20);
            var valueMatrix = testMatrix.MakeRayDensity();
        }
    }
}
