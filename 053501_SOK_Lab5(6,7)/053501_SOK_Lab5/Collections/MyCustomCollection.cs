using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _053501_SOK_Lab5.Collections
{
    public class MyCustomCollection<T> : Interfaces.ICustomCollection<T>
    {

        private class Node
        {
            public T Data { get; set; }
            public Node Next { get; set; }
            public Node Prev { get; set; }
            public Node(T data)
            {
                this.Data = data;
            }
        }
        Node head;
        Node tail;
        int count;
        Node current;

        public T this[int index]
        {
            get
            {
                if (index > count - 1 || index < 0)
                {
                    throw new IndexOutOfRangeException($"Invalid index {index}");
                }
                else
                {
                    Node item = head;
                    for (int i = 0; i < index; i++)
                    {
                        item = item.Next;
                    }
                    return item.Data;
                }
            }
            set
            {
                if (index > count - 1 || index < 0)
                {
                    throw new IndexOutOfRangeException($"Invalid index {index}");
                }
                else
                {
                    Node item = head;
                    for (int i = 0; i < index; i++)
                    {
                        item = item.Next;
                    }
                    item.Data = value;
                }
            }
        }

        public void Reset()
        {
            current = head;
        }

        public void Next()
        {
            if (current != null)
            {
                current = current.Next;
            }
            else
            {
                throw new InvalidOperationException("Current is null");
            }
        }

        public T Current()
        {
            if (current != null)
            {
                return current.Data;
            }
            else
            {
                throw new InvalidOperationException("Current is null");
            }
        }

        public int Count { get { return count; } }

        public void Add(T item)
        {
            count++;
            Node node = new Node(item);
            if (head == null)
            {
                head = node;
            }
            else
            {
                tail.Next = node;
                node.Prev = tail;
            }
            tail = node;
        }

        public void Remove(T item)
        {
            count--;
            Node node = head;

            while (node != null)
            {
                if (node.Data.Equals(item))
                {
                    break;
                }
                node = node.Next;
            }

            if (node == null)
            {
                throw new Exceptions.NoItemException<T>("There is no item", item);
            }

            if (node.Next != null)
            {
                node.Next.Prev = node.Prev;
            }
            else
            {
                tail = node.Prev;
            }

            if (node.Prev != null)
            {
                node.Prev.Next = node.Next;
            }
            else
            {
                head = node.Next;
            }
        }

        public T RemoveCurrent()
        {
            count--;
            if (current != null)
            {
                T data = current.Data;
                if (current.Prev != null)
                {
                    current.Prev.Next = current.Next;
                }
                else
                {
                    head = current.Next;
                }
                if (current.Next != null)
                {
                    current.Next.Prev = current.Prev;
                }
                else
                {
                    tail = current.Prev;
                }
                return data;
            }
            else
            {
                throw new InvalidOperationException("Current is null");
            }
        }
        public MyCustomCollection()
        {
            count = 0;
            Reset();
        }
    }
}
