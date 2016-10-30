using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows;

namespace InwersjaTomograficzna.Core.Extensions
{
    public static class PointFsSort
    {
        public static List<PointF> SortByDistanceFromPointF(PointF startPointF, List<PointF> list)
        {
            List<PointF> result = new List<PointF>();
            result.Add(startPointF);
            list.Remove(startPointF);
            int loopVar = list.Count();

            for(int i=0; i< loopVar; i++)
            {
                PointF nearestPointF = NearestPointFFromList(startPointF, list);
                result.Add(nearestPointF);
                list.Remove(nearestPointF);
            }

            return result;
        }

        public static PointF NearestPointFFromList(PointF srcPointF, List<PointF> listOfPointFs)
        {
            KeyValuePair<double, int> smallestDistance = new KeyValuePair<double, int>();
            for(int i=0;i<listOfPointFs.Count; i++)
            {
                double distance = srcPointF.Distance(listOfPointFs[i]);
                if (i == 0)
                {
                    smallestDistance = new KeyValuePair<double, int>(distance, i);
                }
                else
                {
                    if(distance < smallestDistance.Key)
                    {
                        smallestDistance = new KeyValuePair<double, int>(distance, i);
                    }
                }
            }
            return listOfPointFs[smallestDistance.Value];
        }
    }
}
