using DataStructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AntColony
{
    internal class Node
    {
        private MathMatrix<decimal> matrix;
        private int sense;
        public readonly List<Node> connectedNodes;
        public readonly List<Ant> antsOnNode;

        public Node(MathMatrix<decimal> newMatrix)
        {
            matrix = newMatrix;
            sense = 1;
            connectedNodes = new List<Node>();
            antsOnNode = new List<Ant>();
        }

        public MathMatrix<decimal> Matrix
        {
            get { return matrix; }
        }

        public int AntNumberOnNode
        {
            get
            {
                return antsOnNode.Count;
            }
        }

        public int Sense
        {
            get
            {
                return sense;
            }
            set
            {
                if (value >= sense)
                {
                    sense = 1;
                }
                else
                {
                    sense = value;
                }
            }
        }
    }
}
