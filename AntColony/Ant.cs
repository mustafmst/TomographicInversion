using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AntColony
{
    internal class Ant
    {
        public Node node;
        public void Move(Colony colony)
        {
            node = colony.FirstNode;
        }

        public void LeaveSense()
        {

        }
    }
}
