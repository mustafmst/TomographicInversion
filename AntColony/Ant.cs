using DataStructures;
using DataStructures.Extensions;
using Extensions;
using System;
using System.Linq;

namespace AntColony
{
    internal class Ant
    {
        public Node node;

        public Ant(Node startNode)
        {
            node = startNode;
        }

        public void Move(Colony colony)
        {
            GenerateNewNodes(colony, 2);
            var nextNode = ChooseNextNode(colony.rand);
            colony.MoveAntFromNodeToNode(this, node, nextNode);
        }

        private void GenerateNewNodes(Colony colony, int nodesNumber)
        {
            for (int i = 0; i < nodesNumber; i++)
            {
                var newNode = GenerateNewNode(colony);

                if (!newNode.connectedNodes.Contains(node))
                {
                    node.connectedNodes.Add(newNode);
                    newNode.connectedNodes.Add(node);
                }
            }
        }

        public void LeaveSense(Colony colony)
        {
            if (node.Error == null)
            {
                node.Error = colony.GetStatisticErrorForNode(node);
            }

            int senseForNode = 10-(int)(node.Error/10);
            node.Sense += senseForNode;
        }

        private Node GenerateNewNode(Colony colony)
        {
            var newMatrix = node.Matrix.DeepClone();
            var changeIndex = colony.rand.Next(newMatrix.Height);
            ChangeValueInMatrix(changeIndex, (colony.rand.Next(100) < 50), newMatrix);
            string hash = newMatrix.GetMatrixHash();
            if (colony.DoesNodeExist(hash))
            {
                return colony.GetNode(hash);
            }
            var newNode = new Node(newMatrix);
            colony.AddNewNode(newNode);
            return newNode;
        }

        private void ChangeValueInMatrix(int index, bool increment, MathMatrix<decimal> matrix)
        {
            if (increment)
            {
                matrix[index, 0] += 10;
            }
            else
            {
                matrix[index, 0] -= 10;
            }
        }

        private Node ChooseNextNode(Random rand)
        {
            Node nextNode;
            var senseSum = node.connectedNodes.Select(n => n.Sense).Sum();
            int randomNumber = rand.Next(senseSum);
            foreach(var connectedNode in node.connectedNodes.OrderBy(n => n.Sense))
            {
                nextNode = connectedNode;
                randomNumber -= connectedNode.Sense;
                if (randomNumber < 0) return nextNode;
            }
            throw new Exception("Ups! coś poszło nie tak!!! Nie znaleziono węzła!");
        }
    }
}
