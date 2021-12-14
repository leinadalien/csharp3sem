using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _053501_SOK_Lab11.Lib
{
    public class Worker
    {
        public uint ID { get; set; }
        private static uint CountOFworkers = 0;
        public string Name { get; set; }
        public byte Age { get; set; }
        public Worker()
        {
            CountOFworkers++;
            ID = CountOFworkers;
            Name = "No Name";
            Age = 0;
        }
        public Worker(string name, byte age)
        {
            CountOFworkers++;
            ID = CountOFworkers;
            Name = name;
            Age = age;
        }
        public override string ToString()
        {
            return $"{Name}, {Age} years old [{ID}]";
        }
    }
}
