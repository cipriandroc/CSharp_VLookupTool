using Microsoft.VisualBasic.FileIO;

namespace FileManager.Services
{
    public static class CheckExistingFile
    {

        private static string SetFileNameExtension(string fileName, string fileType) 
        {
            return String.Concat(fileName, '.', fileType);
        }
        public static string GetExisitingFileIncremenet(string directory, string fileName, string fileType) 
        {

            bool process = true;
            int i = 0;
            string newFileName = fileName;

            string fullFileName = SetFileNameExtension(newFileName, fileType);
    
            while (process) 
            {
                if (!File.Exists( (String.Concat(directory, Path.DirectorySeparatorChar, fullFileName)) ) )
                {
                    process = false;
                    continue;
                }

                i++;
                newFileName = String.Concat(fileName, '(', i, ')');
    
                fullFileName = SetFileNameExtension(newFileName, fileType);
            }

            return fullFileName;
        }

}
}
