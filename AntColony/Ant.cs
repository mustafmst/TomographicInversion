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
            GenerateNewNodes(colony, 1);
            var nextNode = ChooseNextNode(colony);

            lock (colony)
            {
                colony.MoveAntFromNodeToNode(this, node, nextNode);
            }
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

        public int ValueOfRoute(Node from, Node to)
        {
            int value = (int)((from.Error * 1000) - (to.Error * 1000));
            if (value <= 0) return 0;
            return value;
        }

        public void LeaveSense(Colony colony)
        {
            int senseForNode = ValueOfRoute(lastNode, node);
            node.Sense += senseForNode/100;
        }

        private Node GenerateNewNode(Colony colony)
        {
            var newMatrix = node.Matrix.DeepClone();
            for(int i=0; i < 1; i++)
            {
                var changeIndex = colony.rand.Next(newMatrix.Height);
                ChangeValueInMatrix(changeIndex, (colony.rand.Next(100) < 50), newMatrix, colony.rand.Next(1,10));
            }
            var newNode = new Node(newMatrix, colony);
            lock (colony)
            {
                string hash = newNode.HashCode;
                if (colony.DoesNodeExist(hash))
                {
                    return colony.GetNode(hash);
                }
                colony.AddNewNode(newNode);
            }

            return newNode;
        }

        private void ChangeValueInMatrix(int index, bool increment, MathMatrix<decimal> matrix, int randomNumber)
        {
            var value = 100 * randomNumber;

            if (increment)
            {
                matrix[index, 0] += value;
            }
            else
            {
                matrix[index, 0] -= value;
            }
            if (matrix[index, 0] > 2000) matrix[index, 0] = 2000;
            if (matrix[index, 0] < 300) matrix[index, 0] = 300;
        }

        private Node ChooseNextNode(Colony colony)
        {
            Node nextNode;
            var senseSum = node.connectedNodes/*.Where(n=>n != lastNode)*/.Select(n => n.Sense).Sum();
            senseSum += node.connectedNodes/*.Where(n => n != lastNode)*/.Select(n => ValueOfRoute(node, n)).Sum();
            int randomNumber = colony.rand.Next(0,senseSum);
            if (senseSum == 0) GenerateNewNodes(colony, 1);
            foreach(var connectedNode in node.connectedNodes/*.Where(n => n != lastNode)*/.OrderBy(n => n.Sense))
            {
                nextNode = connectedNode;
                randomNumber -= connectedNode.Sense + ValueOfRoute(node, nextNode);                
                if (randomNumber < 0) return nextNode;
            }
            throw new Exception();
            //return ChooseNextNode(colony);
        }
    }
}
