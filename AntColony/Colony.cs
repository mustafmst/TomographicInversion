using DataStructures;
using Extensions;
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
        private MathMatrix<decimal> signals;
        private MathMatrix<decimal> times;

        public Node FirstNode
        {
            get { return firstNode; }
        }
        
        public Colony(AlgorythmSettings settings)
        {
            iterations = settings.Iterations;
            signals = settings.Signals;
            times = settings.Times;
            rand = new Random();
            Ants = new List<Ant>();
            AllNodes = new Dictionary<string, Node>();
            GenerateFirstNode();
            AddAnts(settings.AntNumber);
        }

        private void GenerateFirstNode()
        {
            decimal averageVelocity = 0;

            for(int i = 0; i < times.Height; i++)
            {
                averageVelocity += signals.RowSum(i) / times[i, 0];
            }

            averageVelocity = averageVelocity / times.Height;

            var matrix = new MathMatrix<decimal>(1, signals.Width);
            for(int i = 0; i < matrix.Height; i++)
            {
                matrix[i, 0] = averageVelocity;
            }

            var node = new Node(matrix);
            firstNode = node;
            AddNewNode(firstNode);
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
                Ants.ForEach(ant => ant.LeaveSense(this));
                DecreaseSenseOnNodes();
                ReportIterationStatusToFile();
            }

            var bestNode = FindBestSolution();
            return bestNode.Matrix;
        }

        private Node FindBestSolution()
        {
            var orderedByAnts = AllNodes.Select(d => d.Value).ToList().Where(n => n.antsOnNode.Count() != 0).OrderBy(n => n.Error);
            var orderedBySense = AllNodes.Select(d => d.Value).ToList().Where(n => n.Sense != 1).OrderBy(n => n.Error);

            return orderedByAnts.First();
        }

        private void DecreaseSenseOnNodes()
        {
            AllNodes.Select(d => d.Value).ToList().ForEach(n => n.Sense -= 1);
        }

        public void AddNewNode(Node node)
        {
            AllNodes.Add(node.HashCode, node);
        }

        private void ReportIterationStatusToFile()
        {
            Console.WriteLine("Ants on first node: " + firstNode.antsOnNode.Count());
        }

        public void MoveAntFromNodeToNode(Ant ant, Node from, Node to)
        {
            from.antsOnNode.Remove(ant);
            to.antsOnNode.Add(ant);
            ant.node = to;
            ant.lastNode = from;
        }

        public bool DoesNodeExist(string matrixHash)
        {
            return AllNodes.ContainsKey(matrixHash);
        }

        public Node GetNode(string matrixHash)
        {
            return AllNodes[matrixHash];
        }

        public decimal GetStatisticErrorForNode(Node node)
        {
            return signals.Multiply(node.Matrix).AverageStatisticError(times);
        }
    }
}
