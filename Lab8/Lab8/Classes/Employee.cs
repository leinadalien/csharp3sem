using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab8.Classes
{
    public class Employee
    {
        public int Age { get; }
        public bool IsWorking { get; set; }
        public string Name { get; }
        public Employee(string name, int age)
        {
            Name = name;
            Age = age;
            IsWorking = false;
        }
        public Employee(string name, int age, bool is_working)
        {
            Name = name;
            Age = age;
            IsWorking = is_working;
        }
        public override string ToString()
        {
            return $"Name: {Name}, Age: {Age}." + (IsWorking ? "Is working" : "Isn't working");
        }
    }
}
