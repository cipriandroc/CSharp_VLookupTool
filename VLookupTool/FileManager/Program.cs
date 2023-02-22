using FileManager.Enums;
using FileManager.Services;
using Spectre.Console;

namespace FileManager
{
    public class Program
    {

        public static string Start(bool DirectorySelector)
        {
            //todo implement directory passing

            List<string> fileExtensions = GetSupportedFileExtensions();

            string filePath =  Manager.Start(fileExtensions, DirectorySelector);

            if (String.IsNullOrEmpty(filePath))
            {
                throw new Exception("No selection made");
            }

            AnsiConsole.Write(new Markup($"[bold green]Selected[/] [yellow]{filePath}[/]" + "\n"));

            return filePath;
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
    }
}