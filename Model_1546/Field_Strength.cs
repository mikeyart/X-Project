using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Model_1546
{
    public class Field_Strength
    {
        double[] Heights = new double[] { 10, 20, 37.5, 75, 150, 300, 600, 1200 };
        public static double getMaxFieldStrength(int distance, string path, int time)
        {
            double e_fs = 106.9 - 20 * Math.Log10(distance);
            return e_fs;
        }

        public static double GetTabFieldStrength(int distance, string path, int time, double height, int freq)
        {

            string filename = "Tabulated.json";
            string path_file = Path.Combine(Environment.CurrentDirectory, filename);
            double strength = 0;
            string jsonFromFile;
            using (var reader = new StreamReader(path_file))
            {
                jsonFromFile = reader.ReadToEnd();
            }
            var objects = JsonConvert.DeserializeObject<List<RootObject>>(jsonFromFile);

            for (int i = 0; i < objects.Count; i++)
                if (objects[i].Frequency == freq && objects[i].Path == path && objects[i].Values == distance && Heights.Contains(height))
                    strength = objects[i].Strength;
            return strength;
        }
    }
}
