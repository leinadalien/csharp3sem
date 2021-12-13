using System;
using System.Collections.Generic;

namespace _053501_SOK_Lab5
{
    class Program
    {
        
        static void Main(string[] args)
        {
            Entities.Journal journal = new(60, 7, Console.WindowWidth - 65, Console.WindowHeight - 10);
            Entities.Hints hints = new(60,1);
            hints.Update("Press Escape to back to menu", "Escape");
            void ShowFlightHisory(string message) => journal.Update(message);
            Console.CursorVisible = false;
            Entities.Airport Domodedovo = new();
            Domodedovo.DataBaseNotify += journal.Update;
            Domodedovo.RegisterNotify += ShowFlightHisory; 
            int selected = 0;
            ConsoleKeyInfo button;
            bool quit = false;
            string[] ButtonNames = { "Show tariffs", "Register flight", "Show proceeds", "Exit" };
            while (!quit)
            {
                
                DrawMenu(selected, ButtonNames);
                selected = ManageWithArrows(ButtonNames, selected);
                Console.Clear();
                switch (selected)
                {
                    case 0:
                        journal.Rewrite();
                        hints.Update("Press A to add tariff", "A");
                        hints.Update("Press Delete to remove current tariff", "Delete");
                        hints.Update("Press P to sort tariffs by price", "P");
                        hints.Rewrite();
                        PickTariff(ref Domodedovo, ref hints, true);
                        hints.RemoveLatest(3);
                        break;
                    case 1:
                        journal.Rewrite();
                        hints.Update("Press A to add passenger", "A");
                        hints.Update("Press Delete to remove current passenger", "Delete");
                        hints.Update("Press M to select passenger with maximum Payments", "M");
                        hints.Rewrite();
                        Entities.Passenger passenger = PickPassenger(ref Domodedovo, ref hints);
                        hints.RemoveLatest(3);
                        if (passenger == null) break;
                        Console.Clear();
                        journal.Rewrite();
                        hints.Update("Press A to add tariff", "A");
                        hints.Update("Press Delete to remove current tariff", "Delete");
                        hints.Update("Press P to sort tariffs by price", "P");
                        hints.Rewrite();
                        Entities.Tariff tariff = PickTariff(ref Domodedovo, ref hints, false);
                        hints.RemoveLatest(3);
                        if (tariff == null) break;
                        Domodedovo.RegisterFlight(passenger, tariff);
                        do
                        {
                            button = Console.ReadKey(true);
                        } while (button.Key != ConsoleKey.Escape);
                        break;
                    case 2:
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine(Domodedovo.GetProceeds());
                        do
                        {
                            button = Console.ReadKey(true);
                        } while (button.Key != ConsoleKey.Escape);
                        break;
                    case 3:
                        quit = true;
                        break;
                       
                }

            }
        }
        static void DrawMenuItem(string Item, ConsoleColor ForegroundColor, ConsoleColor BackgroundColor) // отрисовка элемента меню
        {
            Console.ForegroundColor = ForegroundColor;
            Console.BackgroundColor = BackgroundColor;
            Console.WriteLine($" {Item} ");
        }
        static void DrawMenu(int selected, string[] ButtonNames) //отрисовка меню
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Clear();
            for (int i = 0; i < ButtonNames.Length; ++i)
            {
                DrawMenuItem(ButtonNames[i], i == selected ? ConsoleColor.Black : ConsoleColor.White, i == selected ? ConsoleColor.DarkYellow : ConsoleColor.Black);
            }
        }
        static bool DrawDialogMenu(string question) // отрисовка диалогового меню
        {
            bool result = true;
            ConsoleKeyInfo button;
            Console.WriteLine(question);
            Console.ForegroundColor = ConsoleColor.Black;
            Console.BackgroundColor = ConsoleColor.DarkYellow;
            Console.Write(" Yes ");
            Console.ResetColor();
            Console.WriteLine(" No ");
            do
            {
                button = Console.ReadKey(true);
                switch (button.Key)
                {
                    case ConsoleKey.LeftArrow:
                        Console.SetCursorPosition(0, Console.CursorTop - 1);
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.BackgroundColor = ConsoleColor.DarkYellow;
                        Console.Write(" Yes ");
                        Console.ResetColor();
                        Console.WriteLine(" No ");
                        result = true;
                        break;
                    case ConsoleKey.RightArrow:
                        Console.SetCursorPosition(0, Console.CursorTop - 1);
                        Console.Write(" Yes ");
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.BackgroundColor = ConsoleColor.DarkYellow;
                        Console.WriteLine(" No ");
                        Console.ResetColor();
                        result = false;
                        break;
                }
            } while (button.Key != ConsoleKey.Enter);
            Console.SetCursorPosition(0, Console.CursorTop - 2);
            for(int i = 0; i < question.Length; i++)
            {
                Console.Write(' ');
            }
            Console.Write("\n         ");
            Console.SetCursorPosition(0, Console.CursorTop - 1);
            return result;
        }
        static int ManageWithArrows(string[] ButtonNames, int selected)
        {
            ConsoleKeyInfo button;
            bool quit = false;
            while (!quit)
            {
                button = Console.ReadKey(true);
                switch (button.Key)
                {
                    case ConsoleKey.UpArrow:
                        if (selected > 0)
                        {
                            Console.SetCursorPosition(0, selected);
                            DrawMenuItem(ButtonNames[selected--], ConsoleColor.White, ConsoleColor.Black);
                            Console.SetCursorPosition(0, selected);
                            DrawMenuItem(ButtonNames[selected], ConsoleColor.Black, ConsoleColor.DarkYellow);
                        }
                        break;
                    case ConsoleKey.DownArrow:
                        if (selected < ButtonNames.Length - 1)
                        {
                            Console.SetCursorPosition(0, selected);
                            DrawMenuItem(ButtonNames[selected++], ConsoleColor.White, ConsoleColor.Black);
                            Console.SetCursorPosition(0, selected);
                            DrawMenuItem(ButtonNames[selected], ConsoleColor.Black, ConsoleColor.DarkYellow);
                        }
                        break;
                    case ConsoleKey.Enter:
                        Console.SetCursorPosition(0, selected);
                        DrawMenuItem(ButtonNames[selected], ConsoleColor.Black, ConsoleColor.DarkGreen);
                        System.Threading.Thread.Sleep(50);
                        quit = true;
                        Console.BackgroundColor = ConsoleColor.Black;
                        break;
                        
                }
            }
            return selected;
        }
        static Entities.Tariff PickTariff(ref Entities.Airport airport, ref Entities.Hints hints, bool show)
        {
            ConsoleKeyInfo button;
            int selectedTariff = 0;
            airport.ShowTariffs(selectedTariff);
            do
            {
                button = Console.ReadKey(true);
                switch (button.Key)
                {
                    case ConsoleKey.UpArrow:
                        if (selectedTariff > 0)
                        {
                            airport.CurrentTariffs[selectedTariff].Show(selectedTariff);
                            selectedTariff--;
                            Console.ForegroundColor = ConsoleColor.Black;
                            Console.BackgroundColor = ConsoleColor.DarkYellow;
                            airport.CurrentTariffs[selectedTariff].Show(selectedTariff);
                            Console.ResetColor();
                        }
                        break;
                    case ConsoleKey.DownArrow:
                        if (selectedTariff < airport.Tariffs.Count - 1)
                        {
                            airport.CurrentTariffs[selectedTariff].Show(selectedTariff);
                            selectedTariff++;
                            Console.ForegroundColor = ConsoleColor.Black;
                            Console.BackgroundColor = ConsoleColor.DarkYellow;
                            airport.CurrentTariffs[selectedTariff].Show(selectedTariff);
                            Console.ResetColor();
                        }
                        break;
                    case ConsoleKey.A:
                        Console.SetCursorPosition(0, airport.Tariffs.Count * 2 + 2);
                        Console.WriteLine("Enter the destination:");
                        string destination = Console.ReadLine();
                        Console.WriteLine("Enter the price:");
                        double price = 0;
                        string remark;
                        bool success;
                        do
                        {
                            try
                            {
                                price = Convert.ToDouble(Console.ReadLine());
                                success = true;
                            }
                            catch (Exception)
                            {
                                success = false;
                                Console.SetCursorPosition(0, Console.CursorTop - 2);
                                Console.WriteLine("Please enter the price correctly:");
                            }
                        } while (!success);
                        if (DrawDialogMenu("Do you want to add remark?"))
                        {
                            Console.WriteLine("Please, enter the remark:");
                            remark = Console.ReadLine();
                            Console.Clear();
                            airport.AddTariff(new Entities.Tariff(price, destination, remark));
                        }
                        else
                        {
                            Console.Clear();
                            airport.AddTariff(new Entities.Tariff(price, destination));
                        }
                        airport.ShowTariffs(selectedTariff);
                        hints.Rewrite();
                        break;
                    case ConsoleKey.Delete:
                        if (airport.Tariffs.Count > 0)
                        {
                            Console.Clear();
                            airport.RemoveTariff(airport.CurrentTariffs[selectedTariff]);
                            if (selectedTariff > airport.Tariffs.Count - 1)
                            {
                                selectedTariff--;
                            }
                            airport.ShowTariffs(selectedTariff);
                            hints.Rewrite();
                        }
                        break;
                    case ConsoleKey.P:
                        selectedTariff = 0;
                        airport.SortByPrice();
                        airport.ShowTariffs(selectedTariff);
                        break;
                    case ConsoleKey.Enter:
                        if (!show)
                        {
                            return airport.CurrentTariffs[selectedTariff];
                        }
                        break;
                }
            } while (button.Key != ConsoleKey.Escape);
            return null;
        }//Функция выбора тарифа или просто просмотра с возможностями добавлять/удалять их
        static Entities.Passenger PickPassenger(ref Entities.Airport airport, ref Entities.Hints hints)
        {
            ConsoleKeyInfo button;
            int selectedPassenger = 0;
            airport.ShowPassengers(selectedPassenger);
            do
            {
                button = Console.ReadKey(true);
                switch (button.Key)
                {
                    case ConsoleKey.UpArrow:
                        if (selectedPassenger > 0)
                        {
                            airport.Passengers[selectedPassenger].Show(selectedPassenger);
                            selectedPassenger--;
                            Console.ForegroundColor = ConsoleColor.Black;
                            Console.BackgroundColor = ConsoleColor.DarkYellow;
                            airport.Passengers[selectedPassenger].Show(selectedPassenger);
                            Console.ResetColor();
                        }
                        break;
                    case ConsoleKey.DownArrow:
                        if (selectedPassenger < airport.Passengers.Count - 1)
                        {
                            airport.Passengers[selectedPassenger].Show(selectedPassenger);
                            selectedPassenger++;
                            Console.ForegroundColor = ConsoleColor.Black;
                            Console.BackgroundColor = ConsoleColor.DarkYellow;
                            airport.Passengers[selectedPassenger].Show(selectedPassenger);
                            Console.ResetColor();
                        }
                        break;
                    case ConsoleKey.A:
                        Console.SetCursorPosition(0, airport.Passengers.Count * 4 + 2);
                        Console.ForegroundColor = ConsoleColor.White;
                        string name, surname;
                        Console.WriteLine("Enter the name of passenger:");
                        name = Console.ReadLine();
                        Console.WriteLine("Enter the surname of passenger:");
                        surname = Console.ReadLine();
                        Console.Clear();
                        airport.AddPassenger(new Entities.Passenger(name, surname));
                        airport.ShowPassengers(selectedPassenger);
                        hints.Rewrite();
                        break;
                    case ConsoleKey.Delete:
                        if (airport.Passengers.Count > 0)
                        {
                            Console.Clear();
                            airport.RemovePassenger(airport.Passengers[selectedPassenger]);
                            if (selectedPassenger > airport.Passengers.Count - 1)
                            {
                                selectedPassenger--;
                            }
                            airport.ShowPassengers(selectedPassenger);
                            hints.Rewrite();
                            
                        }
                        break;
                    case ConsoleKey.M:
                        double MaxPrice = 0;
                        for (int i = 0; i < airport.Passengers.Count; i++)
                        {
                            if (airport.Passengers[i].TotalSum > MaxPrice){
                                MaxPrice = airport.Passengers[i].TotalSum;
                                selectedPassenger = i;
                            }
                        }
                        airport.ShowPassengers(selectedPassenger);
                        break;
                    case ConsoleKey.Enter:
                        return airport.Passengers[selectedPassenger];
                }
            } while (button.Key != ConsoleKey.Escape);
            return null;
        }// Функция выбора пассажира с возможностями добавлять/удалять их
        
    }
}