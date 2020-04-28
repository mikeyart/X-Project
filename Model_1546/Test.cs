using ChoETL;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Model_1546
{
    public class Test
    {
        public static DataTable ConvertCSVtoDataTable(string strFilePath)
        {
            StreamReader sr = new StreamReader(strFilePath);
            string[] headers = sr.ReadLine().Split(',');
            DataTable dt = new DataTable();
            foreach (string header in headers)
            {
                dt.Columns.Add(header);
            }
            while (!sr.EndOfStream)
            {
                string[] rows = Regex.Split(sr.ReadLine(), ",(?=(?:[^\"]*\"[^\"]*\")*[^\"]*$)");
                DataRow dr = dt.NewRow();
                for (int i = 0; i < headers.Length; i++)
                {
                    dr[i] = rows[i];
                }
                dt.Rows.Add(dr);
            }
            return dt;
        }

        public static bool readJSON(float height, int dist)
        {
            string filename = "Tabulated.csv";
            string path_file = Path.Combine(Environment.CurrentDirectory, filename);
            DataTable dt = ConvertCSVtoDataTable(path_file);

           /* DataRow[] dr = dt.Select("Values);
            DataColumn[] dc= dt.Select("")

            foreach (DataRow row in dr)
            {
                var name = row["NAME"].ToString();
                var contact = row["CONTACT"].ToString();
            }
            */
            foreach (DataRow row in dt.Rows)
            {
                foreach(DataColumn column in dt.Columns)
                {
                    Console.WriteLine(row[column]);
                }
            }
            /*using (var reader = new StreamReader(path_file))
            {
                jsonFromFile = reader.ReadToEnd();
            }
            var objects = JsonConvert.DeserializeObject<List<RootObject>>(jsonFromFile);
            Console.WriteLine(objects[0].Path[0]);*/
            return true;
        }
       
    }
}
