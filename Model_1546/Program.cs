using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using H = Model_1546.Height_Interpolation;
using F = Model_1546.Field_Strength;

namespace Model_1546
{
    class Program
    {
        
        static void Main(string[] args)
        {
            H h = new H();
            double strength = h.HeightInterpolation(1, "land", 10,-40,100,"a",0);
            Console.WriteLine(strength);
        }
    }
}
