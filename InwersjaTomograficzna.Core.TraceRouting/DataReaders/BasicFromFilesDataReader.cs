using System;
using DataStructures;

namespace InwersjaTomograficzna.Core.RayDensity.DataReaders
{
    public class BasicFromFilesDataReader : IDataReader
    {
        private readonly string FileName;

        BasicFromFilesDataReader(string fileName)
        {
            FileName = fileName;
        }

        public MathMatrix<decimal> GetRealVelocities()
        {
            throw new NotImplementedException();
        }

        public Tuple<string, string, string, string, string>[] ReadData()
        {
            throw new NotImplementedException();
        }
    }
}
