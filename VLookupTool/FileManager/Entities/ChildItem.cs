using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManager.Entities
{
    public class ChildItem
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string ParentItem { get; set; }

        public ChildItem() 
        { 
            
        }
    }
}
