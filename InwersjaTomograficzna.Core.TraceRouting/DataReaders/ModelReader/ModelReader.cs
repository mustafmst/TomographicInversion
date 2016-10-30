using InwersjaTomograficzna.Core.DataStructures;
using InwersjaTomograficzna.Core.RayDensity.DataReaders;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;

namespace InwersjaTomograficzna.Core.TraceRouting.DataReaders.ModelReader
{
    public class ModelReader : IDataReader
    {
        private List<Point> startPoints;
        private List<Point> endPoints;
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
                    var workSpaceSize = reader.ReadLine().Split('\t').Select(val => int.Parse(val)).ToArray();
                    MaxX = workSpaceSize[0];
                    MaxY = workSpaceSize[1];
                }
                if (line.Contains("SP"))
                {
                    ReadStartPoints();
                }
                if (line.Contains("RP"))
                {
                    ReadEndPoints();
                }
                if (line.Contains("MODEL"))
                {
                    ReadModel();
                }
            }
        }

        private void ReadStartPoints()
        {
            startPoints = new List<Point>();
            while(!(line = reader.ReadLine()).Contains("END"))
            {
                var pointlocation = line.Split('\t').Select(val => int.Parse(val)).ToArray();
                startPoints.Add(new Point(pointlocation[0],pointlocation[1]));
            }
        }

        private void ReadEndPoints()
        {
            endPoints = new List<Point>();
            while (!(line = reader.ReadLine()).Contains("END"))
            {
                var pointlocation = line.Split('\t').Select(val => int.Parse(val)).ToArray();
                endPoints.Add(new Point(pointlocation[0], pointlocation[1]));
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

            foreach(var sp in startPoints)
            {
                foreach(var rp in endPoints)
                {
                    rawDataList.Add(new Tuple<string, string, string, string, string>(sp.X.ToString(), sp.Y.ToString(), rp.X.ToString(), rp.Y.ToString(), GetSignalTime(sp, rp).ToString()));
                }
            }

            return rawDataList.ToArray();
        }

        private double GetSignalTime(Point startPoint, Point endPoint)
        {
            return 1;
        }


    }
}
