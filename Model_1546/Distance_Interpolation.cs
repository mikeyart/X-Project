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
        string[] Distances = new string[] { Enumerable.Range(1, 20).ToString() + MyEnumerable.FiveRange(20, 100).ToString() + MyEnumerable.TenRange(100, 200).ToString() + MyEnumerable.TwentyFiveRange(200, 1001).ToString() };

        public double DistanceInterpolation(int distance, string path, int time, double height, int freq, string option43, double angle)
        {
            double E_inf, E_sup, E;
            int d_inf, d_sup;
            string d1, d2;
            if (Distances.Contains(distance.ToString()))
                return Height_Interpolation.HeightInterpolation(distance, path, time, height, freq, option43, angle);
            else
            {
                Array.Resize(ref Distances, Distances.Length + 1);
                Distances[Distances.GetUpperBound(0)] = distance.ToString();
                Array.Sort(Distances);
                int i = Array.IndexOf(Distances, distance);
                d1 = Distances[i - 1];
                d2 = Distances[i + 1];

                d_inf = Convert.ToInt32(d1);
                d_sup = Convert.ToInt32(d2);

                E_inf = Height_Interpolation.HeightInterpolation(d_inf, path, time, height, freq, option43, angle);
                E_sup = Height_Interpolation.HeightInterpolation(d_sup, path, time, height, freq, option43, angle);
                E = E_inf + (E_sup - E_inf) * Math.Log10(distance * 1.0 / d_inf * 1.0) / Math.Log10(d_sup * 1.0 / d_inf * 1.0);
                return E;
            }

        }

    }
}
