using InwersjaTomograficzna.Core.DataStructures;
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
        private double[][] velocityMatrix;
        private int[] xAxis;
        private int[] yAxis;
        private List<Cell> matrixCells;
        private StreamReader reader;
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
            var rowList = new List<double[]>();
            while (!(line = reader.ReadLine()).Contains("END"))
            {
                var row = line.Split('\t').Select(val => double.Parse(val)).ToArray();
                int count = row.Count();
                rowList.Add(row);
            }
            velocityMatrix = rowList.ToArray();
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

        private double GetSignalTime(PointF startPointF, PointF endPointF)
        {
            return 1;
        }


    }
}
