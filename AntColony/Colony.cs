using DataStructures;
using Extensions;
using InwersjaTomograficzna.Core.DataStructures.Events;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
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
        public event IterationEventHandler resetProgressBar;
        public event IterationEventHandler updateProgressBar;
        private StreamWriter writer;

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
            GenerateFirstNode(settings.RandomStartPoint);
            AddAnts(settings.AntNumber);
        }

        private void GenerateFirstNode(bool randomStart)
        {
            var matrix = new MathMatrix<decimal>(1, signals.Width);
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
                for (int i = 0; i < matrix.Height; i++)
                {
                    matrix[i, 0] = averageVelocity;
                }
            }
            else
            {
                matrix.PutRandomValuesIntoMatrix(300, 2000,2);
            }

            var node = new Node(matrix, this);
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
            var progressVal = new IterationArgument();
            progressVal.Value = iterations;
            resetProgressBar?.Invoke(progressVal);
            StartWriter();
            for (int i = 0; i < iterations; i++)
            {
                //Ants.ForEach(ant => 
                //{
                //    var t = new Thread(new ThreadStart(() => ant.Move(this)));
                //    t.Start();
                //    t.Join();
                //});
                Ants.ForEach(ant => ant.Move(this));
                Ants.ForEach(ant => ant.LeaveSense(this));
                DecreaseSenseOnNodes();
                ReportIterationStatusToFile();

                progressVal.Value = i;
                WriteData(i, signals.Multiply(FindBestSolution().Matrix.ConvertResultToVelociti()).AverageStatisticError(times));
                updateProgressBar?.Invoke(progressVal);
            }

            var bestNode = FindBestSolution();
            writer.Close();
            return bestNode.Matrix;
        }

        private Node FindBestSolution()
        {
            var lowestErrorNode = AllNodes.Select(d => d.Value).OrderBy(e => e.Error).First();
            return lowestErrorNode;
        }

        private void DecreaseSenseOnNodes()
        {
            AllNodes.Select(d => d.Value).ToList().ForEach(n => n.Sense -= Ants.Count());
        }

        public void AddNewNode(Node node)
        {
            AllNodes.Add(node.HashCode, node);
        }

        private void ReportIterationStatusToFile()
        {
            Console.WriteLine("Ants on first node: " + firstNode.antsOnNode.Count());
            var lowestErrorNode = AllNodes.Select(d => d.Value).OrderBy(e => e.Error).First();
            Console.WriteLine(string.Format("Lowest error: {0} : {1} : {2}", lowestErrorNode.HashCode, lowestErrorNode.Sense, lowestErrorNode.Error));
            var maxSense = AllNodes.Select(d => d.Value).OrderBy(e => e.Sense).Last();
            Console.WriteLine(string.Format("max sense: {0} : {1} : {2}", maxSense.HashCode, maxSense.Sense, maxSense.Error));
            var orderedByAntsASC = AllNodes.Select(d => d.Value).ToList().OrderBy(n => n.visited).Last();
            Console.WriteLine(string.Format("max sense: {0} : {1} : {2} : {3}", orderedByAntsASC.HashCode, orderedByAntsASC.Sense, orderedByAntsASC.Error, orderedByAntsASC.visited));
        }

        public void MoveAntFromNodeToNode(Ant ant, Node from, Node to)
        {
            from.antsOnNode.Remove(ant);
            to.antsOnNode.Add(ant);
            ant.node = to;
            ant.lastNode = from;
            to.visited++;
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
            return signals.Multiply(node.Matrix.ConvertResultToVelociti()).AverageStatisticError(times);
        }

        private void StartWriter()
        {
            var tmp = DateTime.Now;
            var filename = ("\\" + "Ants_" + tmp.ToShortDateString() + tmp.ToShortTimeString()).Replace('.', '_').Replace(':', '_') + ".txt";
            var path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + filename;

            writer = new StreamWriter(path);
        }

        private void WriteData(int i, decimal error)
        {
            if (writer != null)
            {
                writer.WriteLine(String.Format("{0}\t{1}", i, error));
            }
        }
    }
}
