using FileManager.Enums;
using FileManager.Services;
using FileManager.Entities;
using Spectre.Console;
using System.IO;

namespace FileManager
{

    public class Program
    {
        private string LastPath { get; set; }

        public Program() 
        {
            LastPath = Path.GetPathRoot(Directory.GetCurrentDirectory());
        }

        public List<Dictionary<string, string>> FileLoader()
        {
            List<string> fileExtensions = GetSupportedFileExtensions();

            string filePath =  Manager.Start(fileExtensions, false, LastPath);

            if (String.IsNullOrEmpty(filePath))
            {
                throw new Exception("No selection made");
            }

            LastPath = ExtractFolderFromFilePath(filePath);

            AnsiConsole.Write(new Markup($"[bold green]Selected[/] [yellow]{filePath}[/]" + "\n"));

            return LoadFileBuilder.Load(filePath);
        }

        public string DirectorySelector()
        {
            string getDirectory = Manager.Start(new List<string> { }, true, LastPath);

            LastPath = getDirectory;

            return getDirectory;
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

        public void SaveFile(List<Dictionary<string, string>> vlookupDict)
        {
            Console.WriteLine("Select export file location");
            string exportLocation = DirectorySelector();

            Console.WriteLine($"You selected export location as : {exportLocation}");

            string thisDay = String.Join(".", ((DateTime.Today).ToString("d")).Split("/").SkipLast(1));

            string fileName = String.Concat(thisDay,"_","parsedFile");

            CSVFile.Save(exportLocation, fileName, vlookupDict);
        }

        private static string ExtractFolderFromFilePath(string filePath)
        {
            string[] splitPath = filePath.Split(Path.DirectorySeparatorChar).SkipLast(1).ToArray();
            string newPath = String.Join(Path.DirectorySeparatorChar, splitPath);

            return newPath;
        }
    }
}