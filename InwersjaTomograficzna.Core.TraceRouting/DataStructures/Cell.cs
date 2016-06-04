using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InwersjaTomograficzna.Core.TraceRouting.DataStructures
{
    public class Cell
    {
        public readonly int xIndex;
        public readonly int yIndex;

        public readonly int leftBoarder;
        public readonly int rightBoarder;
        public readonly int upperBoarder;
        public readonly int lowerBoarder;


    }
}
