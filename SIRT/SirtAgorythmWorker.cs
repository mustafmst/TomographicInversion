using DataStructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIRT
{
    public class SirtAgorythmWorker
    {
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

        public SirtAgorythmWorker(MathMatrix<decimal> signals, MathMatrix<decimal> times)
        {
            this.signals = signals;
            this.times = times;
        }

        private void Compute()
        {
            result = new MathMatrix<decimal>(times.Width, times.Height);
        }
    }
}
