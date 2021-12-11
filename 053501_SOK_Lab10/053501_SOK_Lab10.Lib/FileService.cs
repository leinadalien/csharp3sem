using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace _053501_SOK_Lab10.Lib
{
    public class FileService<T> : IFileService<T> where T : class
    {
        public IEnumerable<T> ReadFile(string fileName)
        {
            return JsonSerializer.Deserialize<IEnumerable<T>>(File.ReadAllText(fileName));
        }

        public void SaveData(IEnumerable<T> data, string fileName)
        {
            File.WriteAllText(fileName, JsonSerializer.Serialize<IEnumerable<T>>(data));
        }
    }
}
