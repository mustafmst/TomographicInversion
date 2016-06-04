using InwersjaTomograficzna.Core.TraceRouting.DataReaders.Mocks;
using InwersjaTomograficzna.Core.TraceRouting.DataStructures;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InwerstaTomograficzna.Core.TraceRouting.Tests
{
    [TestFixture]
    public class BasicTests
    {
        [Test]
        public void TestTrasowaniaZMockowanymiDanymi()
        {
            SignalRoutes signals = new SignalRoutes(new MockDataReader());
            RoutedMatrix testMatrix = new RoutedMatrix(10, signals, 0, 30, 0, 20);
            var valueMatrix = testMatrix.MakeTraceRouting();
        }
    }
}
