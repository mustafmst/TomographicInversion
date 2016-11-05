using DataStructures;
using Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIRT
{
    public class SirtAgorythmWorker
    {
        private int iterations;
        private MathMatrix<decimal> signals;
        private MathMatrix<decimal> times;
        private MathMatrix<decimal> result;

        public MathMatrix<decimal> Result
        {
            get
            {
                if (result == null)
                {
                    Compute();
                }
                return result;
            }
        }

        public SirtAgorythmWorker(MathMatrix<decimal> signals, MathMatrix<decimal> times, int iterations)
        {
            this.signals = signals;
            this.times = times;
            this.iterations = iterations;
        }

        private void Compute()
        {
            result = new MathMatrix<decimal>(times.Width, signals.Width);

            var AT = signals.Transpose();

            var C = new MathMatrix<decimal>(signals.Width, signals.Width);
            for(int i=0; i< C.Width; i++)
            {
                C[i, i] = signals.ColumnSum(i) == 0 ? 0 : 1 / signals.ColumnSum(i);
            }

            var R = new MathMatrix<decimal>(signals.Height, signals.Height);
            for (int i = 0; i < C.Width; i++)
            {
                R[i, i] = signals.RowSum(i)==0 ? 0 : 1 / signals.RowSum(i);
            }

            var CATR = C.Multiply(AT).Multiply(R); 

            for(int iter = 0; iter< iterations; iter++)
            {
                var m1 = signals.Multiply(result);
                var m2 = times.Subtract(m1);
                var m3 = CATR.Multiply(m2);
                result = result.Add(m3);
            }

        }
    }
}
