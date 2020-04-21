using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Excel;
using _Excel = Microsoft.Office.Interop.Excel;
using System.IO;

namespace Model_1546
{
    class Interpolation
    {
        public static void getExcelFile()
        {
            for (int q = 1; q <= 2; q++)
            {
                //Create COM Objects. Create a COM object for everything that is referenced
                _Excel.Application xlApp = new _Excel.Application();
                string filename = "Curves.xls";
                string path = Path.Combine(Environment.CurrentDirectory, filename);
                _Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(path);
                _Excel._Worksheet xlWorksheet = xlWorkbook.Sheets[q];
                _Excel.Range xlRange = xlWorksheet.UsedRange;

                int rowCount = xlRange.Rows.Count;
                int colCount = xlRange.Columns.Count;            

                //iterate over the field strength column
                for (int i = 7; i <= rowCount; i++)
                {
                    for (int j = 11; j <= colCount; j++)
                    {
                        if (j == 11)
                            Console.Write("\r\n");
                        //write the value to the console
                        if (xlRange.Cells[i, j] != null && xlRange.Cells[i, j].Value2 != null)
                            Console.Write(xlRange.Cells[i, j].Value2.ToString() + "\t");
                    }
                }

                //iterate over the distance column
                for (int i = 7; i <= rowCount; i++)
                {
                    for (int j = 2; j <= 2; j++)
                    {
                        if (j == 2)
                            Console.Write("\r\n");
                        //write the value to the console
                        if (xlRange.Cells[i, j] != null && xlRange.Cells[i, j].Value2 != null)
                            Console.Write(xlRange.Cells[i, j].Value2.ToString() + "\t");
                    }
                }

                Console.Write("\r\n");

                int d = 40;
                int f = 500;


                //cleanup
                GC.Collect();
                GC.WaitForPendingFinalizers();
                                
                //release com objects to fully kill excel process from running in the background
                Marshal.ReleaseComObject(xlRange);
                Marshal.ReleaseComObject(xlWorksheet);

                //close and release
                xlWorkbook.Close();
                Marshal.ReleaseComObject(xlWorkbook);

                //quit and release
                xlApp.Quit();
                Marshal.ReleaseComObject(xlApp);
            }
        }


    }
}
