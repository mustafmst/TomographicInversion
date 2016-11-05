using InwersjaTomograficzna.Core.DataStructures;
using InwersjaTomograficzna.Core.Extensions;
using InwersjaTomograficzna.Core.RayDensity.DataReaders;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows;

namespace InwersjaTomograficzna.Core.TraceRouting.DataReaders.ModelReader
{
    public class ModelReader : IDataReader
    {
        private List<PointF> startPointFs;
        private List<PointF> endPointFs;
        private int cellSize;
        private int maxCellXIndex;
        private int maxCellYIndex;
        private int[] xAxis;
        private int[] yAxis;
        private List<Cell> matrixCells;
        private StreamReader reader;
        private StreamWriter writer;
        private string line;
        private int MaxX;
        private int MaxY;

        public int MaxX1
        {
            get
            {
                return MaxX;
            }
        }

        public int MaxY1
        {
            get
            {
                return MaxY;
            }
        }

        public int CellSize
        {
            get
            {
                return cellSize;
            }
        }

        public ModelReader(string fileName)
        {
            ReadFromFile(fileName);
            writer = new StreamWriter("G:\\SIRTData.txt");
        }

        private void ReadFromFile(string fileName)
        {
            reader = new StreamReader(fileName);
            while((line = reader.ReadLine()) != null)
            {
                if (line.Contains("CELLSIZE"))
                {
                    cellSize = int.Parse(reader.ReadLine());
                }
                if (line.Contains("WORKSIZE"))
                {
                    var line = reader.ReadLine().Split(new char[]{ '\t', ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    var workSpaceSize = line.Select(val => int.Parse(val)).ToArray();
                    MaxX = workSpaceSize[0];
                    MaxY = workSpaceSize[1];
                    xAxis = GetBoardersFromMinAndMaxValueAndCellSize(0, MaxX, cellSize);
                    yAxis = GetBoardersFromMinAndMaxValueAndCellSize(0, MaxY, cellSize);
                }
                if (line.Contains("SP"))
                {
                    ReadStartPointFs();
                }
                if (line.Contains("RP"))
                {
                    ReadEndPointFs();
                }
                if (line.Contains("MODEL"))
                {
                    ReadModel();
                }
            }
        }

        private void ReadStartPointFs()
        {
            startPointFs = new List<PointF>();
            while(!(line = reader.ReadLine()).Contains("END"))
            {
                var PointFlocation = line.Split('\t').Select(val => int.Parse(val)).ToArray();
                startPointFs.Add(new PointF(PointFlocation[0],PointFlocation[1]));
            }
        }

        private void ReadEndPointFs()
        {
            endPointFs = new List<PointF>();
            while (!(line = reader.ReadLine()).Contains("END"))
            {
                var PointFlocation = line.Split('\t').Select(val => int.Parse(val)).ToArray();
                endPointFs.Add(new PointF(PointFlocation[0], PointFlocation[1]));
            }
        }

        private void ReadModel()
        {
            matrixCells = new List<Cell>();
            int rowCount = 0;
            while (!(line = reader.ReadLine()).Contains("END"))
            {
                var row = line.Split(new[] { '\t',' ' }, StringSplitOptions.RemoveEmptyEntries).Select(val => double.Parse(val)).ToArray();

                int columnCount = 0;
                foreach (var velocity in row)
                {
                    matrixCells.Add(new Cell(columnCount, rowCount, xAxis[columnCount], xAxis[columnCount + 1], xAxis[rowCount], yAxis[rowCount + 1], row[columnCount]));
                    columnCount++;
                }
                rowCount++;
            }
            maxCellYIndex = matrixCells.Select(c => c.yIndex).Max();
            maxCellXIndex = matrixCells.Select(c => c.xIndex).Max();
        }

        public Tuple<string, string, string, string, string>[] ReadData()
        {
            var rawDataList = new List<Tuple<string, string, string, string, string>>();

            foreach(var sp in startPointFs)
            {
                foreach(var rp in endPointFs)
                {
                    rawDataList.Add(new Tuple<string, string, string, string, string>(sp.X.ToString(), sp.Y.ToString(), rp.X.ToString(), rp.Y.ToString(), GetSignalTime(sp, rp).ToString()));
                }
            }

            return rawDataList.ToArray();
        }

        private void CreateSignalEquasionToFile(double[] matrixRow, double res)
        {
            writer.WriteLine(string.Join("\t", matrixRow)+string.Format("\t|\t{0}", res));
        }

        private double GetSignalTime(PointF startPointF, PointF endPointF)
        {
            var matrixARow = new double[matrixCells.Count()];
            var signal = new Signal(startPointF, endPointF);
            var temporaryPoints = new List<PointF>();
            GelAllCrossingsWithXBoarders(temporaryPoints, signal);
            GetAllCrossingsWithYBoarders(temporaryPoints, signal);
            if (temporaryPoints.Count() == 0) return 0;

            var tmpList1 = temporaryPoints.Distinct().ToList();
            var tmpList2 = tmpList1.Where(PointF => PointF.IsBetweenTwoPointFs(signal.StartPoint, signal.EndPoint)).ToList();
            List<PointF> sortedPointFs = PointFsSort.SortByDistanceFromPointF(signal.StartPoint, tmpList2);

            double res = 0;

            for (int i = 0; i < sortedPointFs.Count() - 1; i++)
            {
                var cell = GetCellFoLine(sortedPointFs[i], sortedPointFs[i + 1]);
                var lengthForPixel = sortedPointFs[i].Distance(sortedPointFs[i + 1]) / cell.velocity;
                matrixARow[(cell.yIndex * (maxCellXIndex+1))+cell.xIndex] = lengthForPixel; 
                res += lengthForPixel;
            }

            CreateSignalEquasionToFile(matrixARow , res);



            return res;
        }

        #region MethodsFromMatrixclass

        private int[] GetBoardersFromMinAndMaxValueAndCellSize(int min, int max, int cellSize)
        {
            List<int> boarders = new List<int>();

            for (int i = min; i <= max; i += cellSize)
            {
                boarders.Add(i);
            }

            return boarders.ToArray();
        }

        private void GetAllCrossingsWithYBoarders(List<PointF> temporaryPointFs, Signal signal)
        {
            if (signal.StartPoint.Y == signal.EndPoint.Y) return;
            foreach (var axis in yAxis)
            {
                var tmp = signal.GetCrossPointFForYAxis(axis);
                if (CheckIfPointFIsOnTheMatrix(tmp))
                {
                    temporaryPointFs.Add(signal.GetCrossPointFForYAxis(axis));
                }
            }
        }

        private void GelAllCrossingsWithXBoarders(List<PointF> temporaryPointFs, Signal signal)
        {
            if (signal.StartPoint.X == signal.EndPoint.X) return;
            foreach (var axis in xAxis)
            {
                var tmp = signal.GetCrossPointFForXAxis(axis);
                if (CheckIfPointFIsOnTheMatrix(tmp))
                {
                    temporaryPointFs.Add(signal.GetCrossPointFForXAxis(axis));
                }
            }
        }

        private bool CheckIfPointFIsOnTheMatrix(PointF PointF)
        {
            if (PointF.X >= 0 && PointF.X <= MaxX)
            {
                if (PointF.Y >= 0 && PointF.Y <= MaxY)
                {
                    return true;
                }
            }
            return false;
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

        #endregion
    }
}
