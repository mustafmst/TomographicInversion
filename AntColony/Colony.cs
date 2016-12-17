using DataStructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AntColony
{
    internal class Colony
    {
        private List<Ant> Ants;
        private Dictionary<string,Node> AllNodes;
        private Node firstNode;
        private readonly int iterations;

        public Node FirstNode
        {
            get { return firstNode; }
        }

        public Colony(int iter, int antsAmount)
        {
            iterations = iter;
            AddAnts(antsAmount);
        }

        private void AddAnts(int amount)
        {
            for(int i = 0; i < amount; i++)
            {
                Ants.Add(new Ant());
            }
        }

        public MathMatrix<decimal> Compute()
        {
            for(int i = 0; i < iterations; i++)
            {
                Ants.ForEach(ant => ant.Move(this));
                Ants.ForEach(ant => ant.LeaveSense());
                DecreaseSenseOnNodes();
                ReportIterationStatusToFile();
            }

            return FindBestSolution();
        }

        private MathMatrix<decimal> FindBestSolution()
        {
            throw new NotImplementedException();
        }

        private void DecreaseSenseOnNodes()
        {

        }

        public void AddNewNode()
        {

        }

        private void ReportIterationStatusToFile()
        {

        }

        public void MoveAntFromNodeToNode(Ant ant, Node from, Node to)
        {
            from.antsOnNode.Remove(ant);
            to.antsOnNode.Add(ant);
            ant.node = to;
        }
    }
}
