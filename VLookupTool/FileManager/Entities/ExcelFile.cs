using System.Collections.Generic;
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
            List<Dictionary<string, string>> placeholder =
                new List<Dictionary<string, string>> { new Dictionary<string, string>() };

            return placeholder;
        }
    }
}
