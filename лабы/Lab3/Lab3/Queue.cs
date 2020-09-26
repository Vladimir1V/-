using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3
{
    class Queue<T> : Base<T>
    {
        
        public Queue()
        {
            head = 0;
            tail = 0;
            count = 0;
            items = new T[length];
        }

        public Queue(int capacity)
        {
            if (capacity < 0)
                throw new ArgumentOutOfRangeException();
            items = new T[capacity];
            head = 0;
            tail = 0;
            count = 0;
        }
        
        public void Enqueue(T item) // Добавление элемента в очередь.
        {
            if (count == items.Length)
            {
                var newArray = new T[items.Length + 1];
                Array.Copy(items, 0, newArray, 0, count);
                items = newArray;
                tail = items.Length - 1;
            }
            items[tail] = item;
            tail = (tail + 1) % items.Length;
            count++;
        }
        
        public T Dequeue() // Извлечение первого элемента из очереди.
        {
            if (count == 0)
                throw new InvalidOperationException();
            T first = items[head];
            items[head] = default(T);
            head = (head + 1) % items.Length;
            count--;
            return first;
        }
          
        public T Peek() //Возврашение первого элемента.
        {
            if (count == 0)
                throw new InvalidOperationException();
            return items[head];
        }
        
        public void Clear() // Очистка очереди.
        {
            if (head < tail)
                Array.Clear(items, head, count);
            else
            {
                Array.Clear(items, head, items.Length - head);
                Array.Clear(items, 0, tail);
            }
            head = 0;
            tail = 0;
            count = 0;
        }
    }
}

