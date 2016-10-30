using InwersjaTomograficzna.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace InwersjaTomograficzna.Core.DataStructures
{
    public class Matrix
    {
        private double[,] matrixOfEndValues;
        private int[] xBoarders;
        private int[] yBoarders;
        private List<Cell> matrixCells;
        private SignalRoutes allSignals;
        private readonly int minX;
        private readonly int maxX;
        private readonly int minY;
        private readonly int maxY;
        private readonly int cellSize;

        #region Properties
        public SignalRoutes AllSignals
        {
            get
            {
                return allSignals;
            }
        }

        public int MaxX
        {
            get
            {
                return maxX;
            }
        }

        public int MaxY
        {
            get
            {
                return maxY;
            }
        }

        public int CellSize
        {
            get
            {
                return cellSize;
            }
        }

        public List<Cell> MatrixCells
        {
            get
            {
                return matrixCells;
            }
        }

        public double[,] MatrixOfEndValues
        {
            get
            {
                return matrixOfEndValues;
            }
        }

        public int[] XBoarders
        {
            get
            {
                return xBoarders;
            }
        }

        public int[] YBoarders
        {
            get
            {
                return yBoarders;
            }
        }

        #endregion

        public Matrix(int cellSize, SignalRoutes signals, int minX, int maxX, int minY, int maxY)
        {
            if (maxX % cellSize != 0 || maxY % cellSize != 0)
            {
                throw new FormatException("Wrong data! wrong cell size");
            }

            this.minX = minX;
            this.maxX = maxX;
            this.minY = minY;
            this.maxY = maxY;
            this.cellSize = cellSize;

            allSignals = signals;
            xBoarders = GetBoardersFromMinAndMaxValueAndCellSize(minX, maxX, cellSize);
            yBoarders = GetBoardersFromMinAndMaxValueAndCellSize(minY, maxY, cellSize);

            GenerateMatrix();
        }

        private int[] GetBoardersFromMinAndMaxValueAndCellSize(int min, int max, int cellSize)
        {
            List<int> boarders = new List<int>();
            
            for(int i = min; i <= max; i += cellSize)
            {
                boarders.Add(i);
            }

            return boarders.ToArray();
        }

        private void GenerateMatrix()
        {
            matrixCells = new List<Cell>();

            matrixOfEndValues = new double[xBoarders.Length - 1, yBoarders.Length - 1];

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

        public double[,] MakeRayDensity()
        {
            List<PointF> temporaryPointFs;

            foreach(var signal in allSignals.AllRoutes)
            {
                temporaryPointFs = new List<PointF>();
                GelAllCrossingsWithXBoarders(temporaryPointFs, signal);
                GetAllCrossingsWithYBoarders(temporaryPointFs, signal);
                if (temporaryPointFs.Count() == 0) continue;

                var tmpList1 = temporaryPointFs.Distinct().ToList();
                var tmpList2 = tmpList1.Where(PointF => PointF.IsBetweenTwoPointFs(signal.StartPointF, signal.EndPointF)).ToList();
                List<PointF> sortedPointFs = PointFsSort.SortByDistanceFromPointF(signal.StartPointF, tmpList2);

                AddSignalLengthsToCells(sortedPointFs);
            }

            return matrixOfEndValues;
        }

        private void GetAllCrossingsWithYBoarders(List<PointF> temporaryPointFs, Signal signal)
        {
            if (signal.StartPointF.Y == signal.EndPointF.Y) return;
            foreach (var yAxis in yBoarders)
            {
                var tmp = signal.GetCrossPointFForYAxis(yAxis);
                if (CheckIfPointFIsOnTheMatrix(tmp))
                {
                    temporaryPointFs.Add(signal.GetCrossPointFForYAxis(yAxis));
                }
            }
        }

        private void GelAllCrossingsWithXBoarders(List<PointF> temporaryPointFs, Signal signal)
        {
            if (signal.StartPointF.X == signal.EndPointF.X) return;
            foreach (var xAxis in xBoarders)
            {
                var tmp = signal.GetCrossPointFForXAxis(xAxis);
                if (CheckIfPointFIsOnTheMatrix(tmp))
                {
                    temporaryPointFs.Add(signal.GetCrossPointFForXAxis(xAxis));
                }
            }
        }

        private void AddSignalLengthsToCells(List<PointF> sortedListofCorssPointFs)
        {
            for(int i=0; i<sortedListofCorssPointFs.Count()-1; i++)
            {
                double distance = sortedListofCorssPointFs[i].Distance(sortedListofCorssPointFs[i + 1]);
                AddValueToSpecificCell(sortedListofCorssPointFs[i], sortedListofCorssPointFs[i + 1], distance);
            }
        }

        private void AddValueToSpecificCell(PointF firstPointF, PointF secondPointF, double value)
        {
            Cell res = GetCellFoLine(firstPointF, secondPointF);
            matrixOfEndValues[res.xIndex, res.yIndex] += value;
        }

        private Cell GetCellFoLine(PointF firstPointF, PointF secondPointF)
        {
            var centerPointF = firstPointF.CenterBetweenThisAndAnotherPointF(secondPointF);

            var cells = matrixCells.Where(cell => centerPointF.IsBetweenTwoPointFs(new PointF(cell.leftBoarder, cell.lowerBoarder), new PointF(cell.rightBoarder, cell.upperBoarder)));
            var tmpcount = cells.Count();
            var res = ((firstPointF.X == secondPointF.X || firstPointF.Y == secondPointF.Y) && cells.Count() > 1) ?
                cells.Where(cell => cell.lowerBoarder == centerPointF.Y ||
                                    cell.leftBoarder == centerPointF.X).Single() :
                cells.Single();
            return res;
        }

        private bool CheckIfPointFIsOnTheMatrix(PointF PointF)
        {
            if(PointF.X >= minX && PointF.X <= maxX)
            {
                if(PointF.Y >= minY && PointF.Y <= maxY)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
