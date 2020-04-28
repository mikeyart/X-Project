using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Model_1546
{
    class Program
    {
        
        static void Main(string[] args)
        {
            double E = Calculate_Field.CalculateField(100, 0.0, 10, 300, 850, "a", 5, true, 10, 40, 15);
            Console.WriteLine(E);
        }
    }
}
