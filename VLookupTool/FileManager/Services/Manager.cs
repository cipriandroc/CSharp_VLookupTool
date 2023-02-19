using FileManager.Enums;
using Spectre.Console;

namespace FileManager.Services
{
    public class Manager
    {
        public static string Start(List<string> fileExtensions)
        {
            string getFile;
            string currentDir = Path.GetPathRoot(Directory.GetCurrentDirectory());
            bool exit = false;

            while (!exit)
            {
                string input = DisplayLocationContents(fileExtensions, currentDir);

                if(ContinueLoop(input))
                {
                    break;
                }

                getFile = GetTargetFile(currentDir, input);

                if (!String.IsNullOrEmpty(getFile))
                {
                    return getFile;
                }

                currentDir = GetFolder(currentDir, input);
            }

            return null;
        }
        private static string DisplayLocationContents(List<string> fileExtensions, string currentDir)
        {
            Console.WriteLine("Current directory: " + currentDir);
            List<string> childItems = ConcatDirectoriesAndFiles(fileExtensions, currentDir);

            List<string> optionList = new List<string>
                    { ConsoleOptions.UpOneLevel.ToString(), ConsoleOptions.exit.ToString() };

            var input = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Enter a directory name to change to, or 'exit' to quit: ")
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

            List<string> filterFilesByExtension = GetFilesByExtensions(currentDir, fileExtensions);

            directories.AddRange(GetFilesByExtensions(currentDir, fileExtensions));
            return directories;
        }

        private static List<string> GetFilesByExtensions(string currentDir, List<string> extensions)
        {
            string[] files = Directory.GetFiles(currentDir);

            var matchingFiles = new List<string>();

            foreach (string file in files)
            {
                string extension = Path.GetExtension(file);

                if (extensions.Contains(extension))
                {
                    matchingFiles.Add(file);
                }
            }

            return matchingFiles;
        }

        private static bool ContinueLoop(string input)
        {
            if (input == ConsoleOptions.exit.ToString())
            {
                return true;
            }

            return false;
        }

        private static string GetTargetFile(string currentDir, string input)
        {
            string newPath = Path.Combine(currentDir, input);

            if (File.Exists(newPath))
            {
                Console.WriteLine("You picked a file!!!2123");
                return newPath;
            }

            return null;
        }
        private static string GetFolder(string currentDir, string input)
        {
            string newPath;

            if (input == ConsoleOptions.UpOneLevel.ToString())
            {
                newPath = GetPreviousDirectory(currentDir);

                return CheckValidDirectory(currentDir, newPath);
            }
            else
            {
                // check if the input is a valid directory
                newPath = Path.Combine(currentDir, input);

                return CheckValidDirectory(currentDir, newPath);
            }
        }

        private static string GetPreviousDirectory(string currentDir)
        {
            string newPath;
            string[] splitPath = currentDir.Split(Path.DirectorySeparatorChar).SkipLast(1).ToArray();
            newPath = String.Join(Path.DirectorySeparatorChar, splitPath);

            if (newPath == currentDir.Split(Path.DirectorySeparatorChar)[0])
            {
                newPath = String.Concat(newPath, Path.DirectorySeparatorChar);
            }

            return newPath;
        }

        private static string CheckValidDirectory(string currentDir, string newPath)
        {

            string errorMessage = "Invalid directory specified: " + newPath;

            if (Directory.Exists(newPath))
            {
                try
                {
                    Directory.GetDirectories(newPath);
                    return newPath;
                }
                catch 
                {
                    errorMessage = "Cannot access path, check read permissions: " + newPath;
                }
            }

            Console.WriteLine(errorMessage);
            return currentDir;
        }
    }
}
