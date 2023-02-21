using Spectre.Console;
using VLookupTool.Services;

namespace VLookupTool
{
    internal class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("Select source file for VLOOKUP");
            string fileA = FileManager.Program.Start(false);
            if (!String.IsNullOrEmpty(fileA))
            {
                AnsiConsole.Write(new Markup($"[bold green]Selected[/] [yellow]{fileA}[/]" + "\n"));
            }

            Console.WriteLine("Select target file for VLOOKUP");
            string fileB = FileManager.Program.Start(false);
            if (!String.IsNullOrEmpty(fileB))
            {
                AnsiConsole.Write(new Markup($"[bold green]Selected[/] [yellow]{fileB}[/]" + "\n"));
            }

            if ((!String.IsNullOrEmpty(fileA)) && (!String.IsNullOrEmpty(fileB))) 
            {
                //begin processing
                List<Dictionary<string, string>> loadFileA = FileManager.Entities.CSVFile.Load(fileA);
                List<Dictionary<string, string>> loadFileB = FileManager.Entities.CSVFile.Load(fileB);

                List<string> keysFileA = ExtractDictKeys.Execute(loadFileA[0]);
                List<string> keysFileB = ExtractDictKeys.Execute(loadFileB[0]);

                string columnFileA = StringFromListSelector.GetString(keysFileA, "select match column file A");
                string columnFileB = StringFromListSelector.GetString(keysFileB, "select match column file B");

                List<string> neededColumns = new List<string>();
                foreach (string key in keysFileB)
                {
                    if(key != columnFileB)
                    {
                        neededColumns.Add(key);
                    }
                }

                var additionalColumns = AnsiConsole.Prompt(
                    new MultiSelectionPrompt<string>()
                        .Title("Select columns from match files to append.")
                        .PageSize(10)
                        .MoreChoicesText("[grey](Move up and down to reveal more fruits)[/]")
                        .InstructionsText(
                            "[grey](Press [blue]<space>[/] to toggle an item, " +
                            "[green]<enter>[/] to accept)[/]")
                        .AddChoices(neededColumns)
                        );


                foreach (Dictionary<string, string> rowA in loadFileA) 
                { 
                    foreach (Dictionary<string, string> rowB in loadFileB) 
                    {
                        if (rowA[columnFileA] != rowB[columnFileB])
                        {
                            continue;
                        }

                        Console.WriteLine($"found match for : {rowB[columnFileA]}");

                        foreach (string column in additionalColumns)
                        {
                            string concatKeyName = String.Concat("vlookup_", column);

                            if (rowA.ContainsKey(concatKeyName))
                            {
                                rowA[concatKeyName] = rowB[column];
                            }
                            else
                            {
                                rowA.Add(concatKeyName, rowB[column]);
                            }
                        }
                    }
                }

                List<string> newColumnKeys = ExtractDictKeys.Execute(loadFileA[0]);

                //parse column keys to identify if any contains comma and add double quotes so CSV doesn't split headers
                List<string> parseNewColumnKeys = new List<string>();
                foreach (string key in newColumnKeys)
                {
                    string parseKey;

                    if (key.Contains(','))
                    {
                        parseKey = '"' + key + '"';
                    }
                    else
                    {
                        parseKey = key;
                    }

                    parseNewColumnKeys.Add(parseKey);
                }

                string header = String.Join(',', parseNewColumnKeys);

                List<string> parseListOfDictsToStrings = new List<string>();
                parseListOfDictsToStrings.Add(header);

                //parse list of dicts and add double quotes for any value that contains comma so CSV doesn't split column order
                foreach (Dictionary<string, string> rowA in loadFileA)
                {
                    foreach (string key in rowA.Keys)
                    {
                        if (rowA[key].Contains(','))
                        {
                            rowA[key] = '"' + rowA[key] + '"';
                        }
                    }
                }

                //add dict values to list 
                foreach (Dictionary<string, string> rowA in loadFileA)
                {
                    parseListOfDictsToStrings.Add(String.Join(',', rowA.Values));                    
                }

                Console.WriteLine("Select export file location");
                string exportLocation = FileManager.Program.Start(true);


                Console.WriteLine($"you selected export location as : {exportLocation}");

                FileManager.Entities.CSVFile.Save(exportLocation, "parsedFile.csv", parseListOfDictsToStrings);

            }
        }
    }
}