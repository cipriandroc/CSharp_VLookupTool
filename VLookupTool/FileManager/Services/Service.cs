using FileManager.Enums;
using FileManager.Processors;
using Spectre.Console;

namespace FileManager.Services
{
    public class Service
    {
        public static string Start(List<string> fileExtensions, bool ManualFilePath, bool DirectorySelector, string currentDir)
        {
            while (true)
            {
                string input = DisplayLocationContents(fileExtensions, currentDir, ManualFilePath, DirectorySelector);

                string selectedFile;

                if (input == ConsoleOptions.exit.ToString())
                {
                    Environment.Exit(0);
                }

                if (input == ConsoleOptions.ManualFilePath.ToString())
                {
                    Console.Write("Enter the manual file path: ");
                    input = Convert.ToString(Console.ReadLine());
                    selectedFile = FileProcessor.ResolveFilePath(input);
                }
                else
                {
                    selectedFile = FileProcessor.ResolveFile(currentDir, input);
                }                

                if (!String.IsNullOrEmpty(selectedFile))
                {
                    return selectedFile;
                }
                if (DirectorySelector && input == ConsoleOptions.SelectDirectory.ToString())
                {
                    return currentDir;
                }

                currentDir = DirectoryProcessor.ResolvePath(currentDir, input);
            }
        }
        private static string DisplayLocationContents(List<string> fileExtensions, string currentDir, bool ManualFilePath, bool DirectorySelector)
        {
            List<string> childItems = ConcatDirectoriesAndFiles(fileExtensions, currentDir);

            List<string> optionList = new List<string>
                    { ConsoleOptions.UpOneLevel.ToString(), ConsoleOptions.CurrentPath.ToString(), ConsoleOptions.GoToRoot.ToString(), ConsoleOptions.exit.ToString() };

            if(ManualFilePath)
            {
                optionList.Add(ConsoleOptions.ManualFilePath.ToString());
            }

            if (DirectorySelector)
            {
                optionList.Add(ConsoleOptions.SelectDirectory.ToString());
            }

            var input = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title($"[grey]current path: {currentDir}[/]")
                    .PageSize(15)
                    .MoreChoicesText("[grey](Move up and down to reveal more objects)[/]")
                    .AddChoiceGroup(ConsoleOptions.options.ToString(), optionList)
                    .AddChoices(childItems)
                    );

            return input;
        }

        private static List<string> ConcatDirectoriesAndFiles(List<string> fileExtensions, string currentDir)
        {
            string[] dirs = Directory.GetDirectories(currentDir);

            List<string> directories = new List<string>(dirs);

            directories.AddRange(FileProcessor.GetFilesByExtensions(currentDir, fileExtensions));
            return directories;
        }
    }
}
