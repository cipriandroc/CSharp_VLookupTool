using Spectre.Console;

namespace VLookupTool.Services
{
    public class ResultsTable
    {

        public static void Preview(List<Dictionary<string, string>> listDict)
        {

            List<string> columnNames = listDict[0].Keys.ToList<string>();

            List<Dictionary<string,string>> columnNamesDictList = new List<Dictionary<string, string>>();
            foreach (string column in columnNames)
            {

                Dictionary<string, string> keyValuePairs = new Dictionary<string, string>();
                keyValuePairs.Add("columnName", column);

                if(column.Contains("vlookup_"))
                {
                    keyValuePairs.Add("type", "vlookup");
                }
                else
                {
                    keyValuePairs.Add("type", "standard");
                }
                columnNamesDictList.Add(keyValuePairs);
            }


            // Create a table
            var table = new Table();

            //Populate Columns
            foreach (Dictionary<string, string> column in columnNamesDictList)
            {
                if (column["type"] == "vlookup")
                {
                    table.AddColumn(new TableColumn($"[green]{column["columnName"]}[/]").Centered());
                }
                else
                {
                    table.AddColumn(new TableColumn(column["columnName"]).Centered());
                }
            }

            // Add rows
            List<List<string>> listOfTableRows = new List<List<string>>();
            foreach (Dictionary<string, string> row in listDict)
            {
                List<string> checkBlankLines = new List<string>();
                foreach (Dictionary<string, string> column in columnNamesDictList)
                {
                    if ( (column["type"] == "vlookup") && row.ContainsKey(column["columnName"]) )
                    {
                        if (!string.IsNullOrEmpty(row[column["columnName"]]))
                        {
                            checkBlankLines.Add(column["columnName"]);
                        }
                    }
                }

                if(checkBlankLines.Count == 0)
                {
                    continue;
                }

                List<string> stringListRow = new List<string>();

                foreach (string key in  row.Keys)
                {
                    if (key.Contains("vlookup_"))
                    {
                        if (!string.IsNullOrEmpty(row[key])) 
                        {
                            stringListRow.Add(String.Concat("[green]", row[key], "[/]"));
                        }
                    }
                    else
                    {
                        stringListRow.Add(row[key]);
                    }
                }

                listOfTableRows.Add(stringListRow);
            }

            var firstTenRows = listOfTableRows.Take(5);

            if (!(firstTenRows.Count() == 0))
            {
                foreach (List<string> item in firstTenRows)
                {
                    table.AddRow(item.ToArray());
                }

                // Render the table to the console
                table.Centered();
                AnsiConsole.Write(table);
            }
            else
            {
                Console.WriteLine("There were no matching values on your query! You can still export the file.");
            }
        }
    }
}
