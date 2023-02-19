﻿using Spectre.Console;
using VLookupTool.FileLoaders;
using VLookupTool.Services;

namespace VLookupTool
{
    internal class Program
    {
        static void Main(string[] args)
        {

            //supported file extensions
            List<string> fileExtensions = new List<string> { ".csv" };
            
            Console.WriteLine("Select source file for VLOOKUP");
            string fileA = FileManager.Program.Start(fileExtensions);
            if (!String.IsNullOrEmpty(fileA))
            {
                AnsiConsole.Write(new Markup($"[bold green]Selected[/] [yellow]{fileA}[/]" + "\n"));
            }

            Console.WriteLine("Select target file for VLOOKUP");
            string fileB = FileManager.Program.Start(fileExtensions);
            if (!String.IsNullOrEmpty(fileB))
            {
                AnsiConsole.Write(new Markup($"[bold green]Selected[/] [yellow]{fileB}[/]" + "\n"));
            }

            if ((!String.IsNullOrEmpty(fileA)) && (!String.IsNullOrEmpty(fileB))) 
            {
                //begin processing
                List<Dictionary<string, string>> loadFileA = CSVLoader.Load(fileA);
                List<Dictionary<string, string>> loadFileB = CSVLoader.Load(fileB);

                List<string> keysFileA = ExtractDictKeys.Execute(loadFileA[0]);
                List<string> keysFileB = ExtractDictKeys.Execute(loadFileB[0]);

                string columnFileA = StringFromListSelector.GetString(keysFileA);
                string columnFileB = StringFromListSelector.GetString(keysFileB);

                
                foreach (Dictionary<string, string> rowA in loadFileA) 
                { 
                    foreach (Dictionary<string, string> rowB in loadFileB) 
                    { 
                        if (rowA[columnFileA] == rowB[columnFileB])
                        {
                            Console.WriteLine($"found match for:{rowA[columnFileA]}");
                        }
                    }
                }

                //perform match - create new dict with A and B dict
                //ask user for columns to add from dict B to dict A
                //export 
            }

        }
    }
}