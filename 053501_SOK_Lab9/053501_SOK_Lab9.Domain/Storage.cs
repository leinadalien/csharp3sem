using System;
using System.Collections.Generic;

namespace _053501_SOK_Lab9.Domain
{
    public class Storage
    {
        
        public List<Detail> Details { get; set; }
        public string Address { get; set; }
        public int Capasity { get; set; }
        public Storage()
        {
            Details = new();
            Capasity = 10;
            Address = null;
        }
        public Storage(int capasity, string address)
        {
            Details = new();
            this.Capasity = capasity;
            Address = address;
        }
        public void AddDetail(Detail detail)
        {
            if(Capasity > Details.Count)
            {
                Details.Add(detail);
            } else
            {
                throw new OverflowException("The storage is full");
            }
        }
        public void RemoveDetail(Detail detail)
        {
            if (Details.Count > 0)
            {
                if(Details.Contains(detail))
                {
                    Details.Remove(detail);
                } else
                {
                    throw new ArgumentException("The storage hasn't such element");
                }
            }
            else
            {
                throw new InvalidOperationException("The storage is empty");
            }
        }
    }
}
