using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _053501_SOK_Lab5.Entities
{
    class Hints
    {
        class Hint
        {
            public string Tip;
            public string Word;
            public Hint(string tip, string word)
            {
                Tip = tip;
                Word = word;
            }
        }
        int x, y;
        List<Hint> Tips;
        public Hints(int x, int y)
        {
            this.x = x;
            this.y = y;
            Tips = new();
        }
        public void Update(string tip, string word)
        {
            Tips.Add(new Hint(tip, word));
        }
        public void RemoveLatest()
        {
            Console.SetCursorPosition(x, y + Tips.Count - 1);
            for (int i = 0; i < Tips[Tips.Count - 1].Tip.Length; i++)
            {
                Console.Write(' ');
            }
            Tips.Remove(Tips[Tips.Count - 1]);  
        }
        public void RemoveLatest(int num)
        {
            for (int i = 0; i < num; i++)
            {
                RemoveLatest();
            }
        }
        public void Rewrite()
        {
            for (int i = 0; i < Tips.Count; i++)
            {
                Console.SetCursorPosition(x, y + i);
                Console.Write(Tips[i].Tip);
                Console.SetCursorPosition(x + 6, y + i);
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Write(Tips[i].Word);
                Console.ResetColor();
            }
        }
    }
}
