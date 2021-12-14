using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using System.IO;

namespace _053501_SOK_Lab11.Lib
{
    public class StreamService
    {
        private static object locker = new();
        public delegate void StreamServiceDelegate(string msg);
        public static event StreamServiceDelegate StreamServiceEvent;
        public static Task WriteToStream(Stream stream, List<Worker> workers)
        {
            return Task.Run(() =>
            {
                lock(locker)
                {
                    StreamServiceEvent?.Invoke("Start writing to stream");
                    StreamWriter streamWriter = new(stream) { AutoFlush = true };
                    streamWriter.WriteLine(JsonSerializer.Serialize<List<Worker>>(workers));
                    StreamServiceEvent?.Invoke("End writing to stream");
                }
            } );
        }
        public static Task CopyFromStream(Stream stream, string fileName)
        {
            return Task.Run(() =>
            {
                lock (locker)
                {
                    StreamServiceEvent?.Invoke("Start reading from stream");
                    stream.Seek(0, SeekOrigin.Begin);
                    if (File.Exists(fileName))
                    {
                        File.Delete(fileName);
                    }
                    var file = File.Open(fileName, FileMode.Create);
                    stream.CopyTo(file);
                    file.Close();
                    StreamServiceEvent?.Invoke("End reading from stream and moved to file");
                }
            } );
        }
        public async static Task<int> GetStatisticsAsync(string fileName, Func<Worker, bool> filter)
        {
            int count = 0;
            await Task.Run(() => 
            {
                StreamServiceEvent?.Invoke("Start getting statisrics from stream");
                List<Worker> workers = new();
                try
                {
                    workers = JsonSerializer.Deserialize<List<Worker>>(File.ReadAllText(fileName));
                } catch (JsonException)
                {
                    StreamServiceEvent?.Invoke("fileName hasn't json tokens");
                }
                foreach( Worker worker in workers)
                {
                    if (filter(worker)) {
                        count++;
                    }
                }
            });
            return count;
        }
    }
}
