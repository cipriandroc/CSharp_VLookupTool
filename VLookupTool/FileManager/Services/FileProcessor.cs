namespace FileManager.Services
{
    public static class FileProcessor
    {

        public static string Get(string directory, string file)
        {
            string filePath = Path.Combine(directory, file);

            if (File.Exists(filePath))
            {
                return filePath;
            }

            return null;
        }

        public static List<string> GetFilesByExtensions(string currentDir, List<string> extensions)
        {
            string[] files = Directory.GetFiles(currentDir);

            var matchingFiles = new List<string>();

            foreach (string file in files)
            {
                string extension = Path.GetExtension(file);

                if (extensions.Contains(extension))
                {
                    matchingFiles.Add(file);
                }
            }

            return matchingFiles;
        }

        public static void SavePlainFile(string location, string fileName, List<string> listOfLines)
        {
            string exportFileLocation = String.Concat(location, Path.DirectorySeparatorChar, "parsedFile.csv");

            System.IO.File.WriteAllLines(exportFileLocation, listOfLines);
        }
    }
}
