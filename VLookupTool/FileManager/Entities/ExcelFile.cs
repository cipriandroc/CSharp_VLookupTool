using System.Collections.Generic;
using Microsoft.VisualBasic.FileIO;
using OfficeOpenXml;

namespace FileManager.Entities
{
    public class ExcelFile
    {

        public static List<Dictionary<string, string>> Load(string path)
        {

            return FileToDict(path);
        }

        public static List<Dictionary<string, string>> FileToDict(string path)
        {
            var result = new List<Dictionary<string, string>>();

            FileInfo fileInfo = new FileInfo(path);
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (ExcelPackage package = new ExcelPackage(fileInfo))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets[0];

                // Read the headers from the first row of the worksheet
                int headerRow = 1;
                var headers = new List<string>();
                for (int col = 1; col <= worksheet.Dimension.Columns; col++)
                {
                    headers.Add(worksheet.Cells[headerRow, col].Value.ToString());
                }

                // Read the data from the remaining rows of the worksheet
                for (int row = 2; row <= worksheet.Dimension.Rows; row++)
                {
                    var dict = new Dictionary<string, string>();
                    for (int col = 1; col <= worksheet.Dimension.Columns; col++)
                    {
                        dict.Add(headers[col - 1], worksheet.Cells[row, col].Value.ToString());
                    }
                    result.Add(dict);
                }
            }

            return result;
        }
    }
}
