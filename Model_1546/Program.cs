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
            F f = new F();
            string strength = f.GetTabFieldStrength(3,"land",50,37.5,100);
            Console.WriteLine(strength);
        }
    }
}
