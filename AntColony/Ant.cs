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
        public Node lastNode;

        public Ant(Node startNode)
        {
            node = startNode;
            lastNode = null;
        }

        public void Move(Colony colony)
        {
            GenerateNewNodes(colony, 2);
            var nextNode = ChooseNextNode(colony);
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
                else if(node.connectedNodes.Count == 1)
                {
                    i --;
                }
            }
        }

        public void LeaveSense(Colony colony)
        {
            int senseForNode = (int)((node.Error*1000)- (lastNode.Error * 1000));
            if (senseForNode <= 0) return;
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
            var newNode = new Node(newMatrix, colony);
            colony.AddNewNode(newNode);
            return newNode;
        }

        private void ChangeValueInMatrix(int index, bool increment, MathMatrix<decimal> matrix)
        {
            var value = 100;

            if ((matrix[index, 0] - value) < 0) increment = true;
            if (increment)
            {
                matrix[index, 0] += value;
            }
            else
            {
                matrix[index, 0] -= value;
            }
        }

        private Node ChooseNextNode(Colony colony)
        {
            Node nextNode;
            var senseSum = node.connectedNodes.Where(n=>n != lastNode).Select(n => n.Sense).Sum();
            int randomNumber = colony.rand.Next(senseSum);
            if (senseSum == 0) GenerateNewNodes(colony, 1);
            foreach(var connectedNode in node.connectedNodes.Where(n => n != lastNode).OrderBy(n => n.Sense))
            {
                nextNode = connectedNode;
                randomNumber -= connectedNode.Sense;
                if (randomNumber < 0) return nextNode;
            }
            throw new Exception();
            //return ChooseNextNode(colony);
        }
    }
}
