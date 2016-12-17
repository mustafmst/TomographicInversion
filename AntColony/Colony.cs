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
        public readonly Random rand;

        public Node FirstNode
        {
            get { return firstNode; }
        }
        
        public Colony(int iter, int antsAmount)
        {
            iterations = iter;
            AddAnts(antsAmount);
            rand = new Random();
        }

        private void AddAnts(int amount)
        {
            for(int i = 0; i < amount; i++)
            {
                Ants.Add(new Ant(firstNode));
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

        public void AddNewNode(Node node)
        {
            AllNodes.Add(node.HashCode, node);
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

        public bool DoesNodeExist(string matrixHash)
        {
            return AllNodes.ContainsKey(matrixHash);
        }

        public Node GetNode(string matrixHash)
        {
            return AllNodes[matrixHash];
        }
    }
}
