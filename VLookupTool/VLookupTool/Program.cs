using Spectre.Console;
using VLookupTool.Entities;
using VLookupTool.Services;

namespace VLookupTool
{
    internal class Program
    {
        static void Main(string[] args)
        {

            FileManager.Program fileManager = new FileManager.Program();

            Console.WriteLine("Select source data(A) for VLOOKUP");
            DataObject dataA = new DataObject(fileManager.FileLoader());

            Console.WriteLine("Select target data(B) for VLOOKUP");
            DataObject dataB = new DataObject(fileManager.FileLoader());

            //begin processing
            string columnDataA = StringFromListSelector.GetString(dataA.DictionaryKeys, "select match column data A");
            string columnDataB = StringFromListSelector.GetString(dataB.DictionaryKeys, "select match column data B");

            List<string> additionalColumns = GetAdditionalColumns(dataB.DictionaryKeys, columnDataB);

            //build match
            List<Dictionary<string, string>> vlookupDict = DisplayPerformTaskToConsole(dataA, dataB, columnDataA, columnDataB, additionalColumns);

            Console.WriteLine("Match complete! Below is a sample of matched values");
            ResultsTable.Preview(vlookupDict);

            fileManager.SaveFile(vlookupDict);
        }

        private static List<Dictionary<string, string>> DisplayPerformTaskToConsole(DataObject dataA, DataObject dataB, string columnDataA, string columnDataB, List<string> additionalColumns)
        {
            List<Dictionary<string, string>> vlookupDict = new List<Dictionary<string, string>>();
            AnsiConsole.Status()
            .Start("Performing match...", ctx =>
            {
                vlookupDict = PerformVLookup(dataA._data, dataB._data, columnDataA, columnDataB, additionalColumns);
            });
            return vlookupDict;
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