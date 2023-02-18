namespace VLookupTool
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            List<string> fileExtensions = new List<string> { ".csv", ".json", ".xlsx", ".svf", ".xml" };

            FileManager.Program.Start(fileExtensions);
        }
    }
}