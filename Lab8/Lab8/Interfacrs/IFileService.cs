using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab8.Interfacrs
{
    interface IFileService 
    {
        IEnumerable<Classes.Employee> ReadFile(string fileName);
        void SaveData(IEnumerable<Classes.Employee> data, string fileName);
    }
}
