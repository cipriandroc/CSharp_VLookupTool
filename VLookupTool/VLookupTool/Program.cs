using Spectre.Console;
using VLookupTool.FileLoaders;

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
                Console.Write("\r" + new string(' ', Console.WindowWidth) + "\r");
                Console.WriteLine($"selected: {fileB}");
            }

            if ((!String.IsNullOrEmpty(fileA)) && (!String.IsNullOrEmpty(fileB))) 
            {
                //begin processing
                List<Dictionary<string, string>> loadFileA = CSVLoader.Load(fileA);
                List<Dictionary<string, string>> loadFileB = CSVLoader.Load(fileB);

                //get dict fileA keys
                //get dict fileB keys
                //ask user for column match point
                //perform match - create new dict with A and B dict
                //ask user for columns to add from dict B to dict A
                //export 
            }

        }
    }
}