using FileManager.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManager.Interfaces
{
    public interface IFileType
    {
        public List<Dictionary<string, string>> Load(string path);

        public void Save(string location, string fileName, List<Dictionary<string, string>> exportDict);

        public List<string> PrepFileForExport(List<Dictionary<string, string>> exportDict);
    }
}
