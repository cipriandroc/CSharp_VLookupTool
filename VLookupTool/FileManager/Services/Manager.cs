﻿using FileManager.Enums;
using Spectre.Console;

namespace FileManager.Services
{
    public class Manager
    {
        public static string Start(List<string> fileExtensions, bool DirectorySelector, string currentDir)
        {
            string getFile;
            bool exit = false;

            while (!exit)
            {
                string input = DisplayLocationContents(fileExtensions, currentDir, DirectorySelector);

                if(ContinueLoop(input))
                {
                    break;
                }

                getFile = FileProcessor.Get(currentDir, input);

                if (!String.IsNullOrEmpty(getFile))
                {
                    return getFile;
                }
                if (DirectorySelector && input == ConsoleOptions.SelectDirectory.ToString())
                {
                    return currentDir;
                }

                currentDir = DirectoryProcessor.Get(currentDir, input);
            }

            return null;
        }
        private static string DisplayLocationContents(List<string> fileExtensions, string currentDir, bool DirectorySelector)
        {
            List<string> childItems = ConcatDirectoriesAndFiles(fileExtensions, currentDir);

            List<string> optionList = new List<string>
                    { ConsoleOptions.UpOneLevel.ToString(), ConsoleOptions.CurrentPath.ToString(), ConsoleOptions.GoToRoot.ToString(), ConsoleOptions.exit.ToString() };

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

        private static bool ContinueLoop(string input)
        {
            if (input == ConsoleOptions.exit.ToString())
            {
                Console.WriteLine("Application terminated.");
                Environment.Exit(0);
            }

            return false;
        }

    }
}
