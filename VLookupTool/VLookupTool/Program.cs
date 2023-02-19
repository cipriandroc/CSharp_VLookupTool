namespace VLookupTool
{
    internal class Program
    {
        static void Main(string[] args)
        {

            //supported file extensions
            List<string> fileExtensions = new List<string> { ".csv", ".json", ".xlsx", ".xml" };

            string file = FileManager.Program.Start(fileExtensions);

            if (!String.IsNullOrEmpty(file)) 
            { 
                Console.WriteLine($"you returned: {file}"); 
            }

        }
    }
}