using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Model_1546
{
    public class TerrainClearanceAngle
    {
        double v_prim, v, Jv, Jv_prim, corr;

        public static double TerrainClearanceAngleCorrectionTx(double angle, int freq)
        {
            double v_prim, v, Jv, Jv_prim, corr;
            if (angle < 0)
                return 0;
            else
            {
                v_prim = 0.036 * Math.Sqrt(freq);
                v = 0.065 * angle * Math.Sqrt(freq);
                Jv = 6.9 + 20.0 * Math.Log10(v - 0.1 + Math.Sqrt(Math.Pow(v - 0.1, 2) + 1));
                Jv_prim = 6.9 + 20.0 * Math.Log10(v_prim - 0.1 + Math.Sqrt(Math.Pow(v_prim - 0.1, 2) + 1));
                corr = Jv_prim - Jv;
                return corr;
            }

        }
        public static double TerrainClearanceAngleCorrectionRx(double angle, int freq)
        {
            double v_prim, v, Jv, Jv_prim,corr;
            if (angle < 0.55)
                return 0;
            else
            {
                if (angle > 40)
                    angle = 40;
                v_prim = 0.036 * Math.Sqrt(freq);
                v = 0.065 * angle * Math.Sqrt(freq);
                Jv = 6.9 + 20.0 * Math.Log10(v - 0.1 + Math.Sqrt(Math.Pow(v - 0.1, 2) + 1));
                Jv_prim = 6.9 + 20.0 * Math.Log10(v_prim - 0.1 + Math.Sqrt(Math.Pow(v_prim - 0.1, 2) + 1));
                corr = Jv_prim - Jv;
                return corr;
            }
        }

        public static double Ang( double value)
        {
            double angle = value + 90;
            return (angle + value) % 360;
        }
        public static double GetC_h1(int freq, double h)
        {
            double Teta_eff,v,k_v,Jv,correction;
            Teta_eff = Ang(Math.Atan(-1 * h / 9000.0));
            if (freq == 2000)
                k_v = 6.0;
            else if (freq == 600)
                k_v = 3.31;
            else if (freq == 100)
            {
                k_v = 1.35;

                v = k_v * Teta_eff;

                if (v <= -0.7806)
                    Jv = 0;
                else
                {
                    Jv = 6.9 + 20 * Math.Log10(v - 0.1 + Math.Sqrt(Math.Pow(v - 0.1, 2) + 1));
                    correction = 6.03 - Jv;
                    return correction;
                }
            }

        }
    }
}
