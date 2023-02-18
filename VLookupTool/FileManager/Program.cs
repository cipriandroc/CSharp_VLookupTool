using FileManager.Services;

namespace FileManager
{
    public class Program
    {

        public static void Start(List<string> fileExtensions)
        {
            Manager.DisplayDriveContents(fileExtensions);
        }
        
    }
}