using System;
using System.Reflection;
using System.Collections.Generic;

namespace _053501_SOK_Lab10
{
    class Program
    {
        static void Main()
        {
            List<Employee> employees = new() {
                new("Valera", 30),
                new("Genadiy", 43),
                new("Vladimir", 67),
                new("Denis", 17),
                new("Inokentiy", 69, true),
                new("Vasiliy", 47),
            };
            Console.WriteLine("List with objects has been created");
            Assembly asm = Assembly.LoadFrom("053501_SOK_Lab10.Lib.dll");
            Type FileService = asm.GetTypes()[0].MakeGenericType(new Type[] { typeof(Employee)});
            object fileService = Activator.CreateInstance(FileService);
            Console.WriteLine("Library with class has been loaded dinamically\nObject of the class has been created");
            MethodInfo methodInfo = FileService.GetMethod("SaveData");
            Console.WriteLine("The SaveData method was received");
            methodInfo.Invoke(fileService, new object[] { employees, "Employees.json" });
            Console.WriteLine("List with objects has been saved in JSON file");
            methodInfo = FileService.GetMethod("ReadFile");
            Console.WriteLine("The ReadFile method was received");
            List <Employee> writtenEmployers = (List<Employee>)methodInfo.Invoke(fileService, new object[] { "Employees.json"});
            Console.WriteLine("List with objects has ben written from JSON file:");
            for(int i = 0; i < writtenEmployers.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {writtenEmployers[i].Name}, {writtenEmployers[i].Age} years old. " + (writtenEmployers[i].IsWorking ? "Works" : "Doesn't work"));
            }
        }
    }
}
