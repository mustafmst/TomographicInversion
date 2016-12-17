using InwersjaTomograficzna.Core.DataStructures.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures
{
    public class AlgorythmSettings
    {
        public string InputFileName { get; set; }
        public bool IsModel { get; set; }
        public bool Sirt { get; set; }
        public bool AntColony { get; set; }
        public int Iterations { get; set; }
        public MathMatrix<decimal> Times { get; set; }
        public MathMatrix<decimal> Signals { get; set; }
        public int AntNumber { get; set; }
        public string OutputFileName { get; set; }
        public bool RandomStartPoint { get; set; }
        public decimal MinimumVelocity { get; set; }
        public decimal MaximumVelocity { get; set; }
        public event IterationEventHandler _resetProgressBar;
        public event IterationEventHandler ResetProgressBar
        {
            add
            {
                lock (_resetProgressBar)
                {
                    _resetProgressBar += value;
                }
            }
            remove
            {
                lock (_resetProgressBar)
                {
                    _resetProgressBar -= value;
                }
            }
        }
        public event IterationEventHandler _updateProgressBar;
        public event IterationEventHandler UpdateProgressBar
        {
            add
            {
                lock (_updateProgressBar)
                {
                    _updateProgressBar += value;
                }
            }
            remove
            {
                lock (_updateProgressBar)
                {
                    _updateProgressBar -= value;
                }
            }
        }
       
    }
}
