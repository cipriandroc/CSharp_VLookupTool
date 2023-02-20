using FileManager.Services;

namespace FileManager
{
    public class Program
    {

        public static string Start(List<string> fileExtensions, bool DirectorySelector)
        {
            //todo implement directory passing
            //string root = Path.GetPathRoot(Directory.GetCurrentDirectory());

            return Manager.Start(fileExtensions, DirectorySelector);
        }
        
    }
}