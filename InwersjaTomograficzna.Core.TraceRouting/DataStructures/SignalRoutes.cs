using System;
using System.Windows;
using System.Collections.Generic;
using InwersjaTomograficzna.Core.RayDensity.DataReaders;

namespace InwersjaTomograficzna.Core.RayDensity.DataStructures
{
    public class SignalRoutes
    {
        private Signal[] _allRoutes;
        public Signal[] AllRoutes
        {
            get
            {
                return _allRoutes;
            }
        }

        /// <summary>
        /// Initializes Object of SignalRoutes
        /// </summary>
        /// <param name="RawData">Array of signals raw data in format: startPoint_x, startPoint_y, endPoint_x, endPoint_y, time </param>
        public SignalRoutes(IDataReader dataReader)
        {
            Tuple<string, string, string, string, string>[] RawData = dataReader.ReadData();

            List<Signal> routesList = new List<Signal>();

            foreach(var signal in RawData)
            {
                routesList.Add(new Signal(
                    new Point(double.Parse(signal.Item1), double.Parse(signal.Item2)),
                    new Point(double.Parse(signal.Item3), double.Parse(signal.Item4)),
                    decimal.Parse(signal.Item5)
                    ));
            }

            _allRoutes = routesList.ToArray();
        }

    }
}
