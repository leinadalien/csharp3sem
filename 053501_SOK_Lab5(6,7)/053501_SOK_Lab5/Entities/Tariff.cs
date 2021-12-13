using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _053501_SOK_Lab5.Entities
{
    class Tariff
    {
        public double Price;
        public string Destination;
        public string Remark;

        public Tariff(double price, string destination)
        {
            Price = price;
            Destination = destination;
        }
        public Tariff(double price, string destination, string remark)
        {
            Price = price;
            Destination = destination;
            Remark = remark;
        }

        public void Show(int selected)
        {
            Console.SetCursorPosition(0, selected * 2);
            Console.Write($"----------------------------------------\n{selected + 1}. ");
            Console.Write(this);
            if (Remark != null)
            {
                Console.Write(". " + Remark);
            }
            while (Console.CursorLeft < 40)
            {
                Console.Write(' ');
            }
            Console.WriteLine("\n----------------------------------------");
        }

        public override string ToString()
        {
            return $"To {Destination} for ${Price}";
        }
    }
}
