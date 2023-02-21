using FileManager.Enums;

namespace FileManager.Services
{
    public static class LoadFileBuilder
    {
        public static void Input(string fileName)
        {
            string fileExtension = GetFileExtension(fileName);

            if (fileExtension == SupportedFileExtensions.csv.ToString())
            {

            }
        }

        private static string GetFileExtension(string fileName)
        {
            string[] splitFilename = fileName.Split('.');

            return splitFilename.Last();
        }
    }
}
