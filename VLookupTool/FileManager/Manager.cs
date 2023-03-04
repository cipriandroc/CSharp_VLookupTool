using FileManager.Enums;
using FileManager.Services;
using FileManager.Entities;
using Spectre.Console;

namespace FileManager
{

    public class Manager
    {
        private string Path { get; set; }

        public Manager() 
        {
            Path = System.IO.Path.GetPathRoot(Directory.GetCurrentDirectory());
        }

        public List<Dictionary<string, string>> FileLoader()
        {
            List<string> fileExtensions = GetSupportedImportFileExtensions();

            string filePath =  Service.Start(fileExtensions, false, Path);

            Path = ExtractFolderFromFilePath(filePath);

            AnsiConsole.Write(new Markup($"[bold green]Selected[/] [yellow]{filePath}[/]" + "\n"));

            return LoadFileBuilder.Load(filePath);
        }

        public string DirectorySelector()
        {
            string getDirectory = Service.Start(new List<string> { }, true, Path);

            Path = getDirectory;

            return getDirectory;
        }

        private static List<string> GetSupportedImportFileExtensions()
        {
            List<string> fileExtensions = new List<string>();
            foreach (SupportedImportFileExtensions fileExtension in (SupportedImportFileExtensions[])Enum.GetValues(typeof(SupportedImportFileExtensions)))
            {
                string convertToString = fileExtension.ToString();
                fileExtensions.Add((String.Concat('.', convertToString)));
            }

            return fileExtensions;
        }

        private static List<string> GetSupportedExportFileExtensions()
        {
            List<string> fileExtensions = new List<string>();
            foreach (SupportedExportFileExtensions fileExtension in (SupportedExportFileExtensions[])Enum.GetValues(typeof(SupportedExportFileExtensions)))
            {
                string convertToString = fileExtension.ToString();
                fileExtensions.Add((String.Concat('.', convertToString)));
            }

            return fileExtensions;
        }

        public void SaveFile(List<Dictionary<string, string>> vlookupDict)
        {
            Console.WriteLine();

            List<string> tempExportFileTypes = GetSupportedExportFileExtensions();

            var input = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[grey]Select export file type:[/]")
                    .PageSize(15)
                    .AddChoices(tempExportFileTypes)
                    );

            //implement translation from input to return interface for saving file from method
            //from export file builder class

            Console.WriteLine("Select export file location");
            string exportLocation = DirectorySelector();

            Console.WriteLine($"You selected export location as : {exportLocation}");

            string thisDay = String.Join(".", ((DateTime.Today).ToString("d")).Split("/").SkipLast(1));

            string fileName = String.Concat(thisDay,"_","parsedFile");

            ExcelFile.Save(exportLocation, fileName, vlookupDict);
        }

        private static string ExtractFolderFromFilePath(string filePath)
        {
            string[] splitPath = filePath.Split(System.IO.Path.DirectorySeparatorChar).SkipLast(1).ToArray();
            string newPath = string.Join(System.IO.Path.DirectorySeparatorChar, splitPath);

            return newPath;
        }
    }
}