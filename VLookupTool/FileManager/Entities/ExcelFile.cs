using System.Collections.Generic;
using Microsoft.VisualBasic.FileIO;
using OfficeOpenXml;

namespace FileManager.Entities
{
    public class ExcelFile
    {

        public static List<Dictionary<string, string>> Load(string path)
        {

            TestExcel(path);
            return FileToDict(path);
        }

        public static List<Dictionary<string, string>> FileToDict(string path)
        {
            List<Dictionary<string, string>> placeholder =
                new List<Dictionary<string, string>> { new Dictionary<string, string>() };

            return placeholder;
        }

        static void TestExcel(string path)
        {
            FileInfo fileInfo = new FileInfo(path);
            using (ExcelPackage package = new ExcelPackage(fileInfo))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets[1];

                for (int row = 1; row <= worksheet.Dimension.Rows; row++)
                {
                    for (int col = 1; col <= worksheet.Dimension.Columns; col++)
                    {
                        Console.Write(worksheet.Cells[row, col].Value.ToString() + "\t");
                    }
                    Console.WriteLine();
                }
            }
        }
    }
}
