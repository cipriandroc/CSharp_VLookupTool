﻿using FileManager.Services;

namespace FileManager
{
    public class Program
    {

        public static string Start(List<string> fileExtensions, bool DirectorySelector)
        {
            return Manager.Start(fileExtensions, DirectorySelector);
        }
        
    }
}