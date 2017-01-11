using DataStructures;
using Extensions;
using InwersjaTomograficzna.Core.DataStructures.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AntColony
{
    public class AntColonyWorker
    {
        private delegate void StoperEvent();
        private MathMatrix<decimal> result;
        private int _iterations;
        private int _antNumber;
        private StoperEvent start;
        private StoperEvent stop;
        public event IterationEventHandler resetProgressBar;
        public event IterationEventHandler updateProgressBar;
        private MathMatrix<decimal> signals;
        private MathMatrix<decimal> times;
        private Colony colony;

        public void SubscribeStoper(Stoper stoprer)
        {
            start = stoprer.Start;
            stop = stoprer.Stop;
        }

        public AntColonyWorker(AlgorythmSettings settings)
        {
            colony = new Colony(settings);
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

        private void Compute()
        {
            colony.resetProgressBar += resetProgressBar;
            colony.updateProgressBar += updateProgressBar;
            start();
            result = colony.Compute();
            stop();
            var tmp = DateTime.Now;
            var filename = ("\\" + "Ant_Result" + tmp.ToShortDateString() + tmp.ToShortTimeString()).Replace('.', '_').Replace(':', '_') + ".txt";
            result.PrinttoFile(filename);
        }
    }
}
