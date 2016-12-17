using DataStructures;
using DataStructures.Extensions;
using Extensions;

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
            var newNode = GenerateNewNode(colony);

            if (!newNode.connectedNodes.Contains(node))
            {
                node.connectedNodes.Add(newNode);
                newNode.connectedNodes.Add(node);
            }

            colony.MoveAntFromNodeToNode(this, node, newNode);
        }

        public void LeaveSense()
        {

        }

        private Node GenerateNewNode(Colony colony)
        {
            var newMatrix = node.Matrix.DeepClone();
            var changeIndex = colony.rand.Next(newMatrix.Height);
            ChangeValueInMatrix(changeIndex, (colony.rand.Next(100) < 50), newMatrix);
            string hash = newMatrix.GetMatrixHash();
            bool nodeExist = colony.DoesNodeExist(hash);
            if (nodeExist)
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
    }
}
