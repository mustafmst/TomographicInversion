using InwersjaTomograficzna.Core.RayDensity.DataReaders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataStructures;
using System.IO;

namespace InwersjaTomograficzna.Core.TraceRouting.DataReaders.RealDataReader
{
    public class RealDataReader : IDataReader
    {
        private readonly string parFileDirectory;

        public RealDataReader(string pathToParFile)
        {
            parFileDirectory = new FileInfo(pathToParFile).Directory.FullName;

        }


        public MathMatrix<decimal> GetRealVelocities()
        {
            throw new NotImplementedException();
        }

        public Tuple<string, string, string, string, string>[] ReadData()
        {
            List<Tuple<string,string,string,string,string>> rays = new List<Tuple<string, string, string, string, string>>();
            for (int i = 1; i < 5; i++)
            {
                using (var startReader = new StreamReader(parFileDirectory + "\\SP_0" + i + "_P.txt"))
                using (var timeReader = new StreamReader(parFileDirectory + "\\t_0" + i + "_P.txt"))
                {
                    string spLine;
                    while ((spLine = startReader.ReadLine()) != null)
                    {
                        var points = spLine.Split(new[] {' ', '\t'}, StringSplitOptions.RemoveEmptyEntries);
                        var sX = points[0];//.Split('e')[0];
                        var sY = points[1];//.Split('e')[0];

                        using (var endReader = new StreamReader(parFileDirectory + "\\RP_0" + i + "_P.txt"))
                        {
                            string endLine;
                            while ((endLine = endReader.ReadLine()) != null)
                            {
                                var pointe = endLine.Split(new[] {' ', '\t'}, StringSplitOptions.RemoveEmptyEntries);
                                var eX = pointe[0];//.Split('e')[0];
                                var eY = pointe[1];//.Split('e')[0];

                                var time = timeReader.ReadLine();
                                rays.Add(new Tuple<string, string, string, string, string>(sX, sY, eX, eY,
                                    time ));
                            }
                        }
                    }
                }
            }

            return rays.ToArray();
        }
    }
}
