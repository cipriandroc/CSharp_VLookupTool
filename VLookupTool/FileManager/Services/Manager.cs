﻿using FileManager.Enums;
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

                getFile = FileProcessor.Get(currentDir, input);

                if (!String.IsNullOrEmpty(getFile))
                {
                    return getFile;
                }

                currentDir = DirectoryProcessor.Get(currentDir, input);
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

            directories.AddRange(FileProcessor.GetFilesByExtensions(currentDir, fileExtensions));
            return directories;
        }

        private static bool ContinueLoop(string input)
        {
            if (input == ConsoleOptions.exit.ToString())
            {
                return true;
            }

            return false;
        }

    }
}
