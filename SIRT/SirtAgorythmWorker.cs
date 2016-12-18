using DataStructures;
using Extensions;
using InwersjaTomograficzna.Core.DataStructures.Events;
using System;

namespace SIRT
{
    public class SirtAgorythmWorker
    {
        private delegate void StoperEvent();
        private int iterations;
        private MathMatrix<decimal> signals;
        private MathMatrix<decimal> times;
        private MathMatrix<decimal> result;
        private StoperEvent start;
        private StoperEvent stop;
        public event IterationEventHandler resetProgressBar;
        public event IterationEventHandler updateProgressBar;
        private int iter;

        public int Iterations
        {
            get
            {
                return iterations;
            }
        }

        public int CurrentIteration
        {
            get
            {
                return iter;
            }
        }

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

        public SirtAgorythmWorker(AlgorythmSettings settings)
        {
            this.signals = settings.Signals;
            this.times = settings.Times;
            this.iterations = settings.Iterations;
        }

        public void SubscribeStoper(Stoper stoprer)
        {
            start = stoprer.Start;
            stop = stoprer.Stop;
        }

        private void Compute()
        {
            var progressVal = new IterationArgument();
            start();
            progressVal.Value = iterations;
            resetProgressBar?.Invoke(progressVal);
            result = new MathMatrix<decimal>(times.Width, signals.Width);

            var AT = signals.Transpose();

            var C = new MathMatrix<decimal>(signals.Width, signals.Width);
            for(int i=0; i< C.Width; i++)
            {
                C[i, i] = signals.ColumnSum(i) == 0 ? 0 : 1 / signals.ColumnSum(i);
            }

            var R = new MathMatrix<decimal>(signals.Height, signals.Height);
            for (int i = 0; i < R.Width; i++)
            {
                R[i, i] = signals.RowSum(i)==0 ? 0 : 1 / signals.RowSum(i);
            }

            var CATR = C.Multiply(AT).Multiply(R); 

            for(iter = 0; iter< iterations; iter++)
            {
                var m1 = signals.Multiply(result);
                var m2 = times.Subtract(m1);
                var m3 = CATR.Multiply(m2);
                result = result.Add(m3);

                progressVal.Value = iter;
                updateProgressBar?.Invoke(progressVal);
            }

            ConvertResultToVelociti();
            stop();
        }

        private void ConvertResultToVelociti()
        {
            for(int i=0; i< result.Height; i++)
            {
                result[i, 0] = result[i, 0]==0 ? 0 : 1 / result[i, 0];
            }
        }
    }
}
