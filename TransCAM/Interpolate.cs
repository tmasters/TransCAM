using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TransCAM
{
    public class Interpolate
    {
        public static double[] InterpolateForYears(double yearBeforeVal, int startYear, int numYears, Dictionary<double, double> points)
        {
            double[] newValues = new double[numYears];
            double[,] orderedPoints = new double[points.Count, 2];
            int i = 0; 
            foreach (var kvp in points.OrderBy(k => k.Key).ToDictionary(k => k.Key, k => k.Value))
            {
                orderedPoints[i, 0] = kvp.Key;
                orderedPoints[i, 1] = kvp.Value;
                i++;
            }

            for (int yr = 0; yr < newValues.Length - 1; yr++)
            {
                int j=0;
                while (j < orderedPoints.GetUpperBound(0) && yr + startYear > orderedPoints[j, 0])
                {
                    j++;
                }
                double y2 = orderedPoints[j, 1];
                double x2 = orderedPoints[j, 0];
                double y1 = 0, x1 = 0;
                if (j > 0)
                {
                    y1 = orderedPoints[j - 1, 1];
                    x1 = orderedPoints[j - 1, 0];
                }
                else
                {
                    y1 = yearBeforeVal;
                    x1 = startYear - 1;
                }

                //Now we have 2 points, find slope and point for actual year
                double slope = (y2 - y1) / (x2 - x1);
                double new_y = slope * (yr + startYear - x1) + y1;
                newValues[yr] = new_y;
            }
            return newValues;
        }

        public static Dictionary<int, double> InterpolateForPDF(int firstPoint, int lastPoint, Dictionary<double, double> points)
        {
            Dictionary<int, double> newValues = new Dictionary<int, double>();
            double[,] orderedPoints = new double[points.Count, 2];
            int i = 0;
            foreach (var kvp in points.OrderBy(k => k.Key).ToDictionary(k => k.Key, k => k.Value))
            {
                orderedPoints[i, 0] = kvp.Key;
                orderedPoints[i, 1] = kvp.Value;
                i++;
            }

            for (int val = 0; val < lastPoint - firstPoint + 1; val++)
            {
                int j = 0;
                while (val > orderedPoints[j, 0] * 10.0 && j <= orderedPoints.GetUpperBound(0))
                {
                    j++;
                }
                if (j == 0 || val > orderedPoints[j, 0]) //Below the lowest point or above the highest point
                {
                    newValues.Add(val, 0.0);
                }
                else
                {
                    double y2 = orderedPoints[j, 1];
                    double x2 = orderedPoints[j, 0];
                    double y1 = orderedPoints[j - 1, 1];
                    double x1 = orderedPoints[j - 1, 0];
                    //Now we have 2 points, find slope and point for actual year
                    double slope = (y2 - y1) / (x2 - x1);
                    double new_y = slope * (x2 - x1) + y1;
                    newValues.Add(val, new_y);
                }
            }

            return newValues;
        }
    }
}
