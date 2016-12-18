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

        public void SubscribeStoper(Stoper stoprer)
        {
            start = stoprer.Start;
            stop = stoprer.Stop;
        }

        public AntColonyWorker(AlgorythmSettings settings)
        {
            _iterations = settings.Iterations;
            _antNumber = settings.AntNumber;
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
            start();
            result = new Colony(_iterations, _antNumber).Compute();
            stop();
        }
    }
}
