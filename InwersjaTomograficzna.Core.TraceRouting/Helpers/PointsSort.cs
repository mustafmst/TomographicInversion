using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace InwersjaTomograficzna.Core.RayDensity.Helpers
{
    public static class PointsSort
    {
        public static List<Point> SortByDistanceFromPoint(Point startPoint, List<Point> list)
        {
            List<Point> result = new List<Point>();
            result.Add(startPoint);
            list.Remove(startPoint);
            int loopVar = list.Count();

            for(int i=0; i< loopVar; i++)
            {
                Point nearestPoint = NearestPointFromList(startPoint, list);
                result.Add(nearestPoint);
                list.Remove(nearestPoint);
            }

            return result;
        }

        public static Point NearestPointFromList(Point srcPoint, List<Point> listOfPoints)
        {
            KeyValuePair<double, int> smallestDistance = new KeyValuePair<double, int>();
            for(int i=0;i<listOfPoints.Count; i++)
            {
                double distance = srcPoint.Distance(listOfPoints[i]);
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
            return listOfPoints[smallestDistance.Value];
        }
    }
}
