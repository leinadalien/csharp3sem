using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _053501_SOK_Lab5.Entities
{
    class Airport
    {
        public Dictionary<(string, double), Tariff> Tariffs;
        public List<Entities.Tariff> CurrentTariffs;
        private Collections.MyCustomCollection<Passenger> BlackList;
        public List<Passenger> Passengers;
        private double proceeds;
        public delegate void MyDelegate(string message);
        public event MyDelegate DataBaseNotify;
        public event MyDelegate RegisterNotify;
        public Airport()
        {
            proceeds = 0;
            Tariffs = new();
            BlackList = new();
            Passengers = new();
            CurrentTariffs = new();
        }
        public double GetProceeds()
        {
            return proceeds;
        }
        public void ShowTariffs(int selected)
        {
            if (Tariffs.Count > 0)
            {
                for (int i = 0; i < CurrentTariffs.Count; i++)
                {
                    CurrentTariffs[i].Show(i);
                }
                Console.ForegroundColor = ConsoleColor.Black;
                Console.BackgroundColor = ConsoleColor.DarkYellow;
                CurrentTariffs[selected].Show(selected);
                Console.ResetColor();
            } else
            {
                Console.SetCursorPosition(0, 0);
                Console.WriteLine("There is no any tariff");
            }
        }
        public void ShowPassengers(int selected)
        {
            if (Passengers.Count > 0)
            {
                for (int i = 0; i < Passengers.Count; i++)
                {
                    Passengers[i].Show(i);
                    
                }
                Console.ForegroundColor = ConsoleColor.Black;
                Console.BackgroundColor = ConsoleColor.DarkYellow;
                Passengers[selected].Show(selected);
                Console.ResetColor();
            }
            else
            {
                Console.SetCursorPosition(0, 0);
                Console.WriteLine("There is no any passenger");
            }
        }
        public void RegisterFlight(Passenger passenger, Tariff tariff)
        {
            bool success = true;
            for(int i = 0; i < BlackList.Count; i++)
            {
                if (BlackList[i].Equals(passenger))
                {
                    success = false;
                    break;
                }
            }
            if (success)
            {
                passenger.CountOfFlights++;
                proceeds += tariff.Price;
                passenger.TotalSum += tariff.Price;
                RegisterNotify?.Invoke($"The flight {tariff} has been successfully registred for a passenger {passenger}");
            } else
            {
                RegisterNotify?.Invoke($"Register error! The passenger {passenger} is blacklisted");
            }
        }
        public void AddToBlackList(Passenger passenger)
        {
            BlackList.Add(passenger);
        }
        public void AddPassenger(Passenger passenger)
        {
            Passengers.Add(passenger);
            DataBaseNotify?.Invoke($"The passenger {passenger} has been added");
        }
        public void RemovePassenger(Passenger passenger)
        {
            Passengers.Remove(passenger);
            DataBaseNotify?.Invoke($"The passenger {passenger} has been removed");
        }
        public void AddTariff(Tariff tariff)
        {
            try
            {
                Tariffs.Add((tariff.Destination, tariff.Price), tariff);
                CurrentTariffs.Add(tariff);
                DataBaseNotify?.Invoke($"The tariff {tariff} has been added");
            }
            catch (ArgumentException)
            {
                DataBaseNotify?.Invoke($"The tariff {tariff} already exist");
            }
            
        }
        public void RemoveTariff(Tariff tariff)
        {
            Tariffs.Remove((tariff.Destination, tariff.Price));
            CurrentTariffs.Remove(tariff);
            DataBaseNotify?.Invoke($"The tariff {tariff} has been removed");
        }
        public void SortByPrice()
        {
            for (int i = 1; i < CurrentTariffs.Count; i++)
            {
                for (int j = i; j > 0 && CurrentTariffs[j].Price < CurrentTariffs[j - 1].Price; j--)
                {
                    Entities.Tariff buff = CurrentTariffs[j];
                    CurrentTariffs[j] = CurrentTariffs[j - 1];
                    CurrentTariffs[j - 1] = buff;
                }
            }
            DataBaseNotify?.Invoke("Tariffs have been sorted by price");
        }
    }
}
