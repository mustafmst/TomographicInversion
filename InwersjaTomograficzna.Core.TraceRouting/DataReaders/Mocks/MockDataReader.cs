using System;
using System.Collections.Generic;

namespace InwersjaTomograficzna.Core.RayDensity.DataReaders.Mocks
{
    public class MockDataReader : IDataReader
    {
        private const int maxY = 20;
        private const int maxX = 30;
        private const int startPointFsStep = 2;
        private const int recivingPointFStep = 2;
        private const int time = 1;


        public Tuple<string, string, string, string, string>[] ReadData()
        {
            List<Tuple<string, string, string, string, string>> signalsList = new List<Tuple<string, string, string, string, string>>();

            for(int spX = 0; spX <= maxX; spX += startPointFsStep)
            {
                for(int rpY=recivingPointFStep; rpY < maxY; rpY += recivingPointFStep)
                {
                    signalsList.Add(NewRawData(spX, 0, 0, rpY));
                }
                for(int rpX = recivingPointFStep; rpX < maxX; rpX += recivingPointFStep)
                {
                    signalsList.Add(NewRawData(spX, 0, rpX, maxY));
                }
                for(int rpY = maxY-recivingPointFStep; rpY > 0; rpY -= recivingPointFStep)
                {
                    signalsList.Add(NewRawData(spX, 0, maxX, rpY));
                }
            }

            return signalsList.ToArray();
        }

        private Tuple<string, string, string, string, string> NewRawData(int spX,int spY, int rpX, int rpY)
        {
            return new Tuple<string, string, string, string, string>(spX.ToString(), spY.ToString(), rpX.ToString(), rpY.ToString(), time.ToString());
        }
    }
}
