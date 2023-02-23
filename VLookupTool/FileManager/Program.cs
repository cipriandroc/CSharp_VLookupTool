using FileManager.Enums;
using FileManager.Services;
using FileManager.Entities;
using Spectre.Console;

namespace FileManager
{
    public class Program
    {
        public static List<Dictionary<string, string>> FileLoader()
        {
            List<string> fileExtensions = GetSupportedFileExtensions();

            string filePath =  Manager.Start(fileExtensions, false);

            if (String.IsNullOrEmpty(filePath))
            {
                throw new Exception("No selection made");
            }

            AnsiConsole.Write(new Markup($"[bold green]Selected[/] [yellow]{filePath}[/]" + "\n"));

            List<Dictionary<string, string>> loadFile = FileManager.Entities.CSVFile.Load(filePath);

            return loadFile;
        }

        public static string DirectorySelector()
        {
            List<string> fileExtensions = GetSupportedFileExtensions();

            return Manager.Start(fileExtensions, true);
        }

        private static List<string> GetSupportedFileExtensions()
        {
            List<string> fileExtensions = new List<string>();
            foreach (SupportedFileExtensions fileExtension in (SupportedFileExtensions[])Enum.GetValues(typeof(SupportedFileExtensions)))
            {
                string convertToString = fileExtension.ToString();
                fileExtensions.Add((String.Concat('.', convertToString)));
            }

            return fileExtensions;
        }

        public static void SaveFile(List<Dictionary<string, string>> vlookupDict)
        {
            Console.WriteLine("Select export file location");
            string exportLocation = DirectorySelector();

            Console.WriteLine($"You selected export location as : {exportLocation}");

            string fileName = "parsedFile.csv";

            CSVFile.Save(exportLocation, fileName, vlookupDict);
        }
    }
}