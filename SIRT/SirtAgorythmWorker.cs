using DataStructures;
using Extensions;
using InwersjaTomograficzna.Core.DataStructures.Events;
using System;
using System.IO;
using System.Reflection;

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
        private StreamWriter writer;
        private bool randomStart;

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
            this.randomStart = settings.RandomStartPoint;
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
            result = new MathMatrix<decimal>(1, signals.Width);
            if (!randomStart)
            {
                decimal averageVelocity = 0;

                for (int i = 0; i < times.Height; i++)
                {
                    averageVelocity += signals.RowSum(i) / times[i, 0];
                }

                averageVelocity = averageVelocity / times.Height;

                averageVelocity -= averageVelocity - (int)averageVelocity;
                averageVelocity -= averageVelocity % 100;
                for (int i = 0; i < result.Height; i++)
                {
                    result[i, 0] = averageVelocity;
                }
            }
            else
            {
                result.PutRandomValuesIntoMatrix(300, 2000,2);
            }

            result = result.ConvertResultToVelociti();

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
            StartWriter();
            for(iter = 0; iter< iterations; iter++)
            {
                var m1 = signals.Multiply(result);
                var m2 = times.Subtract(m1);
                var m3 = CATR.Multiply(m2);
                result = result.Add(m3);

                progressVal.Value = iter;
                WriteData(iter, signals.Multiply(result).AverageStatisticError(times));
                updateProgressBar?.Invoke(progressVal);
            }

            ConvertResultToVelociti();
            writer.Close();
            stop();
            result.PrinttoFile("Sirt_Result.txt");
        }

        private void ConvertResultToVelociti()
        {
            result = result.ConvertResultToVelociti();
        }

        private void StartWriter()
        {
            var tmp = DateTime.Now;
            var filename = ("\\" + "Sirt_" + tmp.ToShortDateString() + tmp.ToShortTimeString()).Replace('.', '_').Replace(':', '_') + ".txt";
            var path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location)+filename;

            writer = new StreamWriter(path);
        }

        private void WriteData(int i, decimal error)
        {
            if(writer != null)
            {
                writer.WriteLine(String.Format("{0}\t{1}",i,error));
            }
        }
    }
}
