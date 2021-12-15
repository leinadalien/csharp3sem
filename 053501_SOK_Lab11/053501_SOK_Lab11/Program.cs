using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using System.IO;
using _053501_SOK_Lab11.Lib;

namespace _053501_SOK_Lab11
{
    class Program
    {
        static async Task Main(string[] args)
        {
            //task 1 
            MyMath.MyMathEvent += PrintInfoInConsole;
            Thread thread1 = new(new ThreadStart(() => MyMath.IntegralOfSin(0, 5, 0.00000001)));
            thread1.Priority = ThreadPriority.Highest;
            Thread thread2 = new(new ThreadStart(() => MyMath.IntegralOfSin(2, 4, 0.00000001)));
            thread2.Priority = ThreadPriority.Lowest;
            thread1.Start();
            thread2.Start();
            //task 2
            List<Worker> workers = new();
            Random rand = new();
            string[] names = { "Vasiliy", "Petr", "Sergey", "Mihail", "Andrey", "Vladislav", "Stanislav", "Dmitriy", "Maksim" };
            for (int i = 0; i < 100; i++)
            {
                workers.Add(new(names[rand.Next(names.Length)], ((byte)rand.Next(16, 80))));
            }
            StreamService.StreamServiceEvent += PrintInfoInConsole;
            MemoryStream stream = new();
            Task.WaitAll(StreamService.WriteToStream(stream, workers), StreamService.CopyFromStream(stream, "fileworkers"));
            Console.WriteLine("Count of workers older then 35: " + await StreamService.GetStatisticsAsync("fileworkers", Filter));
        }
        public static bool Filter(Worker worker)
        {
            return worker.Age > 35;
        }
        static void PrintInfoInConsole(string msg)
        {
            Console.WriteLine(msg);
        }
    }
}
