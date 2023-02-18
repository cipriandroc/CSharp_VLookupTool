using FileManager.Services;

namespace FileManager
{
    public class Program
    {

        public static string Start(List<string> fileExtensions)
        {
            return Manager.Start(fileExtensions);
        }
        
    }
}