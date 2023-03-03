using FileManager.Entities;
using FileManager.Enums;

namespace FileManager.Services
{
    public static class LoadFileBuilder
    {
        public static List<Dictionary<string, string>> Load(string fileName)
        {
            string fileExtension = GetFileExtension(fileName);

            if (fileExtension == SupportedImportFileExtensions.csv.ToString())
            {
                return CSVFile.Load(fileName);
            }
            if (fileExtension == SupportedImportFileExtensions.xls.ToString())
            {
                return ExcelFile.Load(fileName);
            }
            if (fileExtension == SupportedImportFileExtensions.xlsx.ToString())
            {
                return ExcelFile.Load(fileName);
            }

            throw new Exception("unhandled file type detected");
        }

        private static string GetFileExtension(string fileName)
        {
            string[] splitFilename = fileName.Split('.');

            return splitFilename.Last();
        }
    }
}
