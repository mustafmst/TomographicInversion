using System;

namespace InwersjaTomograficzna.Core.RayDensity.DataReaders
{
    public interface IDataReader
    {
        Tuple<string, string, string, string, string>[] ReadData();
    }
}
