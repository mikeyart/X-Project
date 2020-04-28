using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model_1546
{
    public static class MyEnumerable
    {
        public static IEnumerable<int> FiveRange(int start, int count)
        {
            for (int i = start; i < start + count; i += 5)
            {
                yield return i;
            }
        }

        public static IEnumerable<int> TenRange(int start, int count)
        {
            for (int i = start; i < start + count; i += 10)
            {
                yield return i;
            }
        }

        public static IEnumerable<int> TwentyFiveRange(int start, int count)
        {
            for (int i = start; i < start + count; i += 25)
            {
                yield return i;
            }
        }
    }

    public class Distance_Interpolation
    {
        public static double DistanceInterpolation(int distance, string path, int time, double height, int freq, string option43, double angle)
        {
            //List<int> Distances = Enumerable.Range(1, 20).ToList() + MyEnumerable.FiveRange(20,100).ToList();
            List<int> D1 = Enumerable.Range(1, 19).ToList();
            List<int> D2 = MyEnumerable.FiveRange(20, 80).ToList();
            List<int> D3 = MyEnumerable.TenRange(100, 100).ToList();
            List<int> D4 = MyEnumerable.TwentyFiveRange(200, 1001).ToList();

            var Distances = D1.Concat(D2)
                              .Concat(D3)
                              .Concat(D4)
                              .ToList();
            double E_inf, E_sup, E;
            int d_inf, d_sup,i;
             if (Distances.Contains(distance))
                 return Height_Interpolation.HeightInterpolation(distance, path, time, height, freq, option43, angle);
             else
             {
                Distances.Add(distance);
                Distances.Sort();
                i = Distances.IndexOf(distance);
                 d_inf = Distances[i - 1];
                 d_sup = Distances[i + 1];

                 E_inf = Height_Interpolation.HeightInterpolation(d_inf, path, time, height, freq, option43, angle);
                 E_sup = Height_Interpolation.HeightInterpolation(d_sup, path, time, height, freq, option43, angle);
                 E = E_inf + (E_sup - E_inf) * Math.Log10(distance * 1.0 / d_inf * 1.0) / Math.Log10(d_sup * 1.0 / d_inf * 1.0);
                 return E;
             }
             
            return 0;
        }

    }
}
