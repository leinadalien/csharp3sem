using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab8.Classes
{
    class FileService : Interfacrs.IFileService
    {
        public IEnumerable<Employee> ReadFile(string file_name)
        {
            if (!File.Exists(file_name))
            {
                throw new ArgumentException(nameof(file_name));
            }
            BinaryReader reader = new(File.Open(file_name, FileMode.Open));
            using (reader)
            {
                while (reader.PeekChar() > -1)
                {
                    string name = reader.ReadString();
                    int age = reader.ReadInt32();
                    bool is_working = reader.ReadBoolean();
                    yield return new Employee(name, age, is_working);
                }
            }
        }

        public void SaveData(IEnumerable<Employee> data, string file_name)
        {
            if(File.Exists(file_name))
            {
                File.Delete(file_name);
            }
            using (File.Create(file_name)) { } ;
            BinaryWriter writer = new BinaryWriter(File.Open(file_name, FileMode.Create));
            using (writer)
            {
                foreach (var item in data)
                {
                    writer.Write(item.Name);
                    writer.Write(item.Age);
                    writer.Write(item.IsWorking);
                }
            }
        }
    }
}
