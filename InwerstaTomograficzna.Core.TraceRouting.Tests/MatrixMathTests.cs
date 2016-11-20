using DataStructures;
using Extensions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InwerstaTomograficzna.Core.TraceRouting.Tests
{
    [TestFixture]
    public class MatrixMathTests
    {
        [Test]
        public void MatrixColumnSumTest()
        {
            MathMatrix<decimal> m = CreateMatrix();

            var colSum1 = m.ColumnSum(0);
            var colSum2 = m.ColumnSum(1);

            Assert.AreEqual(3, colSum1);
            Assert.AreEqual(7, colSum2);

        }

        [Test]
        public void MatrixRowSumTest()
        {
            MathMatrix<decimal> m = CreateMatrix();

            var rowSum1 = m.RowSum(0);
            var rowSum2 = m.RowSum(1);

            Assert.AreEqual(4, rowSum1);
            Assert.AreEqual(6, rowSum2);
        }

        [Test]
        public void MatrixMultiplyTest() { }

        [Test]
        public void MatrixAddTest()
        {
            MathMatrix<decimal> m1 = CreateMatrix();
            MathMatrix<decimal> m2 = CreateMatrix();

            var res = m1.Add(m2);
            Assert.AreEqual(2, res[0,0]);
            Assert.AreEqual(4, res[1,0]);
            Assert.AreEqual(6, res[0,1]);
            Assert.AreEqual(8, res[1,1]);

        }

        [Test]
        public void MatrixSubtrackTest() { }

        private MathMatrix<decimal> CreateMatrix()
        {
            MathMatrix<decimal> m = new MathMatrix<decimal>(2, 2);
            m[0, 0] = 1;
            m[1, 0] = 2;
            m[0, 1] = 3;
            m[1, 1] = 4;

            return m;
        }

    }
}
