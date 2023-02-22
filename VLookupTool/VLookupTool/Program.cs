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


            Console.WriteLine("Select target file for VLOOKUP");
            string fileB = FileManager.Program.Start(false);


            //begin processing
            List<Dictionary<string, string>> loadFileA = FileManager.Entities.CSVFile.Load(fileA);
            List<Dictionary<string, string>> loadFileB = FileManager.Entities.CSVFile.Load(fileB);

            List<string> keysFileA = FileManager.Services.ExtractDictKeys.Execute(loadFileA[0]);
            List<string> keysFileB = FileManager.Services.ExtractDictKeys.Execute(loadFileB[0]);

            string columnFileA = StringFromListSelector.GetString(keysFileA, "select match column file A");
            string columnFileB = StringFromListSelector.GetString(keysFileB, "select match column file B");

            List<string> additionalColumns = GetAdditionalColumns(keysFileB, columnFileB);

            //build match
            List<Dictionary<string, string>> vlookupDict = PerformVLookup(loadFileA, loadFileB, columnFileA, columnFileB, additionalColumns);

            //export
            Console.WriteLine("Select export file location");
            string exportLocation = FileManager.Program.Start(true);

            Console.WriteLine($"you selected export location as : {exportLocation}");

            FileManager.Entities.CSVFile.Save(exportLocation, "parsedFile.csv", vlookupDict);
            
        }

        private static List<Dictionary<string, string>> PerformVLookup(List<Dictionary<string, string>> loadFileA, List<Dictionary<string, string>> loadFileB, string columnFileA, string columnFileB, List<string> additionalColumns)
        {
            List<Dictionary<string, string>> resultList = new List<Dictionary<string, string>>();

            foreach (Dictionary<string, string> rowA in loadFileA)
            {
                Dictionary<string, string> resultRow = new Dictionary<string, string>(rowA);

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

                        if (resultRow.ContainsKey(concatKeyName))
                        {
                            resultRow[concatKeyName] = rowB[column];
                        }
                        else
                        {
                            resultRow.Add(concatKeyName, rowB[column]);
                        }
                    }
                }

                resultList.Add(resultRow);
            }

            return resultList;
        }

        private static List<string> GetAdditionalColumns(List<string> keysFileB, string columnFileB)
        {
            List<string> neededColumns = new List<string>();
            foreach (string key in keysFileB)
            {
                if (key != columnFileB)
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
            return additionalColumns;
        }
    }
}