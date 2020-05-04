using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model_1546
{
    public class Station_Add
    {
        public static DataTable ConvertCSVtoDataTable(string strFilePath)
        {
            DataTable dt = new DataTable();
            using (StreamReader sr = new StreamReader(strFilePath))
            {
                string[] headers = sr.ReadLine().Split(';');
                foreach (string header in headers)
                {
                    dt.Columns.Add(header);
                }
                while (!sr.EndOfStream)
                {
                    string[] rows = sr.ReadLine().Split(';');
                    DataRow dr = dt.NewRow();
                    for (int i = 0; i < rows.Count(); i++)
                    {
                        dr[i] = rows[i];
                    }
                    dt.Rows.Add(dr);
                }

            }
            return dt;
        }

        public static void StationAdd()
        {
            string filename = "Stations.csv";
            string path_file = Path.Combine(Environment.CurrentDirectory, filename);
            DataTable dt = ConvertCSVtoDataTable(path_file);

            DataRow[] Rows = dt.Select();

            foreach (DataRow row in Rows)
            {
                string[] columnNames = dt.Columns.Cast<DataColumn>()
                                         .Select(x => x.ColumnName).Where(n => n.Contains(height.ToString())).ToArray();
                var latitudeTx = row[columnNames].ToString();
                return name;
            }
        }

    }
}
