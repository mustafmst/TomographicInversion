﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InwersjaTomograficzna.Core.RayDensity.DataReaders.Mocks
{
    public class MockDataReader : IDataReader
    {
        private const int maxY = 20;
        private const int maxX = 30;
        private const int startPointsStep = 2;
        private const int recivingPointStep = 2;
        private const int time = 1;


        public Tuple<string, string, string, string, string>[] ReadData()
        {
            List<Tuple<string, string, string, string, string>> signalsList = new List<Tuple<string, string, string, string, string>>();

            for(int spX = 0; spX <= maxX; spX += startPointsStep)
            {
                for(int rpY=0; rpY < maxY; rpY += recivingPointStep)
                {
                    signalsList.Add(NewRawData(spX, 0, 0, rpY));
                }
                for(int rpX = 0; rpX < maxX; rpX += recivingPointStep)
                {
                    signalsList.Add(NewRawData(spX, 0, rpX, maxY));
                }
                for(int rpY = maxY; rpY > 0; rpY -= recivingPointStep)
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
