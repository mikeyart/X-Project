using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model_1546
{
    public class Calculate_Field
    {
        public static double CalculateField(int distance, double sea_percent, int time, double height, int freq, string option43, double angle, bool use_rTCA, int rTCA, double power, int ag12)
        {
            double fsl_10m, Kh2, correction;

            fsl_10m = Corrections.fsl(distance, sea_percent, time, height, freq, option43, angle, use_rTCA, rTCA, power);

            if (ag12 == 10)
                return Math.Round(fsl_10m, 2);
            else
            {
                Kh2 = 3.2 + 6.2 * Math.Log10(freq);
                correction = Kh2 * Math.Log10(ag12 * 1.0 / 10);
                return Math.Round(fsl_10m, 2);
            }
        }
        
    }
}
