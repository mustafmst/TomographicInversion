using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InwersjaTomograficzna.Core.TraceRouting.DataReaders.Mocks
{
    public class MockDataReader : IDataReader
    {
        public Tuple<string, string, string, string, string>[] ReadData()
        {
            List<Tuple<string, string, string, string, string>> signalsList = new List<Tuple<string, string, string, string, string>>();

            return signalsList.ToArray();
        }
    }
}
