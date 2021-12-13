using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Lab8
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Classes.Employee> data = new();
            data.Add(new Classes.Employee("Kolya", 23, true));
            data.Add(new Classes.Employee("Vasya", 17, false));
            data.Add(new Classes.Employee("Nadya", 19, true));
            data.Add(new Classes.Employee("Anya", 45, true));
            data.Add(new Classes.Employee("Vitya", 103, false));
            Classes.FileService service = new();
            service.SaveData(data, "workers.txt");
            if (File.Exists("Employeers.txt")) File.Delete("Employeers.txt");
            File.Move("workers.txt", "Employeers.txt");
            List<Classes.Employee> NewData = new(service.ReadFile("Employeers.txt"));
            for (int i = 0; i < NewData.Count; i++)
            {
                Console.WriteLine(NewData[i]);
            }
            var Sorted = NewData.OrderBy(a => a, new Classes.EmpoyeeComparer());
            foreach(var i in Sorted)
            {
                Console.WriteLine(i);
            }
        }
    }
}
