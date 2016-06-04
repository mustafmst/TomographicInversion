using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InwersjaTomograficzna.Core.TraceRouting.DataReaders
{
    public class BasicFromFilesDataReader : IDataReader
    {
        private readonly string FileName;

        BasicFromFilesDataReader(string fileName)
        {
            FileName = fileName;
        }

        public Tuple<string, string, string, string, string>[] ReadData()
        {
            throw new NotImplementedException();
        }
    }
}
