using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.VisualBasic.FileIO;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace VLookupTool.FileLoaders
{
    public class CSVLoader
    {
        public CSVLoader(string fileName) 
        {
            using (TextFieldParser parser = new TextFieldParser(fileName))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(",");

                // Read the headers
                string[] headers = parser.ReadFields();

                while (!parser.EndOfData)
                {
                    // Read a line of data
                    string[] fields = parser.ReadFields();

                    // Access the fields by their index or header name
                    string field1 = fields[0]; // Or: string field1 = fields["Column1"]
                    int field2 = int.Parse(fields[1]); // Or: int field2 = int.Parse(fields["Column2"])
                }
            }
        }


    }
}
