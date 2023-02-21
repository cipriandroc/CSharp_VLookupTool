using FileManager.Services;
using Microsoft.VisualBasic.FileIO;

namespace FileManager.Entities
{
    public static class CSVFile
    {

        public static List<Dictionary<string, string>> Load(string path)
        {

            return FileToDict(path);
        }

        public static List<Dictionary<string, string>> FileToDict(string path)
        {
            var result = new List<Dictionary<string, string>>();

            using (var parser = new TextFieldParser(path))
            {
                parser.CommentTokens = new string[] { "#" };
                parser.SetDelimiters(new string[] { "," });
                parser.HasFieldsEnclosedInQuotes = true;

                string[] headers = parser.ReadFields();

                while (!parser.EndOfData)
                {
                    string[] fields = parser.ReadFields();
                    var dict = new Dictionary<string, string>();

                    for (int i = 0; i < headers.Length && i < fields.Length; i++)
                    {
                        dict.Add(headers[i], fields[i]);
                    }

                    result.Add(dict);
                }
            }

            return result;
        }

        public static void Save(string location, string fileName, List<Dictionary<string, string>> exportDict)
        {
            string exportFileLocation = String.Concat(location, Path.DirectorySeparatorChar, "parsedFile.csv");

            List<string> listOfLines = PrepFileForExport(exportDict);

            try
            {
                System.IO.File.WriteAllLines(exportFileLocation, listOfLines);
            }
            catch
            {
                Console.Error.WriteLine("Could not write file to location. Verify permissions/file in use!");
            }
        }

        public static List<string> PrepFileForExport(List<Dictionary<string, string>> exportDict)
        {
            //rebuild dict for csv export
            List<string> newColumnKeys = ExtractDictKeys.Execute(exportDict[0]);

            //parse column keys to identify if any contains comma and add double quotes so CSV doesn't split headers
            List<string> parseNewColumnKeys = ParseCSVColumnsToQuotes(newColumnKeys);

            string header = String.Join(',', parseNewColumnKeys);

            List<string> parseListOfDictsToStrings = new List<string>();
            parseListOfDictsToStrings.Add(header);

            ParseCSVDictLinesToQuotes(exportDict);

            //add dict values to list 
            foreach (Dictionary<string, string> rowA in exportDict)
            {
                parseListOfDictsToStrings.Add(String.Join(',', rowA.Values));
            }

            return parseListOfDictsToStrings;
        }
        private static void ParseCSVDictLinesToQuotes(List<Dictionary<string, string>> loadFileA)
        {
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
        }

        private static List<string> ParseCSVColumnsToQuotes(List<string> newColumnKeys)
        {
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

            return parseNewColumnKeys;
        }

    }
}
