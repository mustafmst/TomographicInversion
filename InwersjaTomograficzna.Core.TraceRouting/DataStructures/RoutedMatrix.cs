using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InwersjaTomograficzna.Core.TraceRouting.DataStructures
{
    public class RoutedMatrix
    {
        private int[][] matrixOfEndValues;
        private int[] xBoarders;
        private int[] yBoarders;
        private List<Cell> matrixCells;

        RoutedMatrix(int cellSize, SignalRoutes signals, int minX, int maxX, int minY, int maxY)
        {
            if (maxX % cellSize != 0 || maxY % cellSize != 0)
            {
                throw new FormatException("Wrong data! wrong cell size");
            }

            xBoarders = GetBoardersFromMinAndMaxValueAndCellSize(minX, maxX, cellSize);
            yBoarders = GetBoardersFromMinAndMaxValueAndCellSize(minY, maxY, cellSize);

            GenerateMatrix();
        }

        private int[] GetBoardersFromMinAndMaxValueAndCellSize(int min, int max, int cellSize)
        {
            List<int> boarders = new List<int>();
            
            for(int i = min; i <= max; i++)
            {
                boarders.Add(i);
            }

            return boarders.ToArray();
        }

        private void GenerateMatrix()
        {
            matrixCells = new List<Cell>();

            matrixOfEndValues = new int[xBoarders.Length-1][yBoarders.Length-1]();

            for(int xIndex = 0; xIndex < xBoarders.Length-1; xIndex++)
            {
                for(int yIndex = 0; yIndex < yBoarders.Length - 1; yIndex++)
                {
                    matrixCells.Add(
                        new Cell(xIndex, yIndex,
                            xBoarders[xIndex], xBoarders[xIndex + 1], 
                            yBoarders[yIndex], yBoarders[yIndex + 1]));
                }
            }
        }
    }
}
