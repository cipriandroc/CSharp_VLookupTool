using FileManager.Enums;
using FileManager.Services;

namespace FileManager
{
    public class Program
    {

        public static string Start(bool DirectorySelector)
        {
            //todo implement directory passing

            List<string> fileExtensions = GetSupportedFileExtensions();

            return Manager.Start(fileExtensions, DirectorySelector);
        }

        private static List<string> GetSupportedFileExtensions()
        {
            List<string> fileExtensions = new List<string>();
            foreach (SupportedFileExtensions fileExtension in (SupportedFileExtensions[])Enum.GetValues(typeof(SupportedFileExtensions)))
            {
                string convertToString = fileExtension.ToString();
                fileExtensions.Add((String.Concat('.', convertToString)));
            }

            return fileExtensions;
        }
    }
}