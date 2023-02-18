using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManager.Services
{
    public class Manager
    {
        public static void DisplayDriveContents(List<string> fileExtensions)
        {
            string currentDir = Path.GetPathRoot(Directory.GetCurrentDirectory());
            bool exit = false;

            while (!exit)
            {
                Console.WriteLine("Current directory: " + currentDir);
                List<string> childItems = ConcatDirectoriesAndFiles(fileExtensions, currentDir);

                List<string> optionList = new List<string>
                    { "UpOneLevel", "exit"};

                var input = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("Enter a directory name to change to, or 'exit' to quit: ")
                        .PageSize(15)
                        .MoreChoicesText("[grey](Move up and down to reveal more objects)[/]")
                        .AddChoiceGroup("options", optionList)
                        .AddChoices(childItems)
                        );

                ParseInput(ref currentDir, ref exit, input);
            }
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

        private static void ParseInput(ref string currentDir, ref bool exit, string input)
        {
            if (input.ToLower() == "exit")
            {
                exit = true;
            }
            else if (input == "UpOneLevel")
            {
                string[] splitPath = currentDir.Split(Path.DirectorySeparatorChar).SkipLast(1).ToArray();
                string newPath = String.Join(Path.DirectorySeparatorChar, splitPath);

                if (newPath == currentDir.Split(Path.DirectorySeparatorChar)[0])
                {
                    newPath = String.Concat(newPath, Path.DirectorySeparatorChar);
                }

                if (Directory.Exists(newPath))
                {
                    currentDir = newPath;
                }
                else
                {
                    Console.WriteLine($"Invalid directory. {newPath}");
                }
            }
            else
            {
                // check if the input is a valid directory
                string newPath = Path.Combine(currentDir, input);

                if (File.Exists(newPath))
                {
                    Console.WriteLine("You picked a file!!!2123");
                }
                else if (Directory.Exists(newPath))
                {
                    currentDir = newPath;
                }
                else
                {
                    Console.WriteLine("Invalid directory.");
                }
            }
        }
    }
}
