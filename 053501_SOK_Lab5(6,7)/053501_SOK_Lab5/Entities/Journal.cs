using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _053501_SOK_Lab5.Entities
{
    class Journal
    {
        private Collections.MyCustomCollection<string> Messages = new();
        private int x, y, width, height;
        public Journal()
        {
            x = 0;
            y = 0;
            width = 50;
            height = 10;
        }
        public Journal(int x, int y, int width, int height)
        {
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
        }
        public void SetPosition(int x, int y)
        {
            this.x = x;
            this.y = y;
            Rewrite();
        }
        public void SetSize(int width, int height)
        {
            this.width = width;
            this.height = height;
            Rewrite();
        }
        public void Resize()
        {
            width = 50;
            height = 10;
            Rewrite();
        }
        public void Update(string message)
        {
            Messages.Add(message);
            Rewrite();
        }
        public void Rewrite()
        {
            Console.SetCursorPosition(x, y);
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("Journal:");
            int countOfVisible = 0;
            for (int freeSpace = height - 1, i = Messages.Count - 1; i >= 0 && freeSpace > Math.Ceiling((float)Messages[i].Length/width); i--)
            {
                freeSpace -= (int)Math.Ceiling((float)Messages[i].Length / width) + 1;
                countOfVisible++;
            }
            int currentLine = y + 1;
            for (int i = Messages.Count - countOfVisible; i < Messages.Count; i++)
            {
                currentLine++;
                string message = Messages[i];
                while (message.Length % width != 0)
                {
                    message += ' ';
                }
                for (int j = 0; j < message.Length/width; j++)
                {
                    Console.SetCursorPosition(x, currentLine);
                    Console.Write(message.Substring(j*width, width));
                    currentLine++;
                }
                
            }
        }
    }
}
