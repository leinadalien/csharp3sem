using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _053501_SOK_Lab5.Entities
{
    class Passenger
    {
        public string Name { get; }
        public string Surname { get; }
        public int CountOfFlights { get; set; }
        public double TotalSum { get; set; }
        public Passenger(string name, string surname)
        {
            Name = name;
            Surname = surname;
            CountOfFlights = 0;
            TotalSum = 0;
        }
        public override string ToString()
        {
            return Name + ' ' + Surname;
        }

        public void Show(int selected)
        {
            Console.SetCursorPosition(0, selected * 4);
            Console.Write($"----------------------------------------\n{selected + 1}. ");
            Console.Write(this);
            while (Console.CursorLeft < 40)
            {
                Console.Write(' ');
            }
            Console.Write("\nTotal sum: " + TotalSum);
            while (Console.CursorLeft < 40)
            {
                Console.Write(' ');
            }
            Console.Write("\nCount of flights: " + CountOfFlights);
            while (Console.CursorLeft < 40)
            {
                Console.Write(' ');
            }
            Console.WriteLine("\n----------------------------------------");
        }
    }
}
