using InwersjaTomograficzna.Core.RayDensity.DataReaders;
using InwersjaTomograficzna.Core.RayDensity.DataStructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace InwersjaTomograficzna.Core.TraceRouting.DataReaders.ModelReader
{
    class ModelReader : IDataReader
    {
        private List<Point> startPoints;
        private List<Point> endPoints;
        private readonly int cellSize;
        private double[,] velocityMatrix;
        private int[] xAxis;
        private int[] yAxis;
        private List<Cell> matrixCells;

        public ModelReader(string fileName)
        {
            ReadFromFile(fileName);
        }

        private void ReadFromFile(string fileName)
        {

        }

        public Tuple<string, string, string, string, string>[] ReadData()
        {
            throw new NotImplementedException();
        }


    }
}
