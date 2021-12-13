using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _053501_SOK_Lab5.Exceptions
{
    class NoItemException<T> : Exception
    {
        public T Item { get; }
        public NoItemException(string message, T item) : base(message)
        {
            Item = item;
        }
    }
}
