using Microsoft.VisualBasic.FileIO;

namespace FileManager.Entities
{
    public class ExcelFile
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
    }
}
