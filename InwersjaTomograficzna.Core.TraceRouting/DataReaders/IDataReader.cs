using System;

namespace InwersjaTomograficzna.Core.TraceRouting.DataReaders
{
    public interface IDataReader
    {
        Tuple<string, string, string, string, string>[] ReadData();
    }
}
