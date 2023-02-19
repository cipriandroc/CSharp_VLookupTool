using Spectre.Console;

namespace VLookupTool
{
    internal class Program
    {
        static void Main(string[] args)
        {

            //supported file extensions
            List<string> fileExtensions = new List<string> { ".csv", ".json", ".xlsx", ".xml" };
            
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
            }

        }
    }
}