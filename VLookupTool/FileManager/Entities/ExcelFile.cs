using System.Collections.Generic;
using FileManager.Services;
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

        public static void Save(string location, string fileName, List<Dictionary<string, string>> exportDict)
        {
            fileName = CheckExistingFile.GetExisitingFileIncremenet(location, fileName, "xlsx");
            string exportFileLocation = Path.Combine(location, fileName);

            using (ExcelPackage package = new ExcelPackage())
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Sheet1");

                // Write the headers to the first row of the worksheet
                int headerRow = 1;
                int headerCol = 1;
                foreach (string header in exportDict.First().Keys)
                {
                    worksheet.Cells[headerRow, headerCol].Value = header;
                    headerCol++;
                }

                // Write the data to the remaining rows of the worksheet
                int dataRow = 2;
                foreach (var row in exportDict)
                {
                    int dataCol = 1;
                    foreach (var value in row.Values)
                    {
                        worksheet.Cells[dataRow, dataCol].Value = value;
                        dataCol++;
                    }
                    dataRow++;
                }

                // Save the Excel package to the file system
                try
                {
                    Console.WriteLine($"writing file {fileName} to location {exportFileLocation}");
                    package.SaveAs(new FileInfo(exportFileLocation));
                }
                catch
                {
                    Console.Error.WriteLine("Could not write file to location. Verify permissions/file in use!");
                }
            }
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
