using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3
{
    class Stack<T> : Base<T>
    {
        public Stack()
        {
            head = 0;
            count = 0;
            items = new T[length];
        }

        public Stack(int length)
        {
            items = new T[length];
        }

        public T Pop() //Извлечение элемента из вершины.
        {
            if (IsEmpty())
            {
                throw new InvalidOperationException("Стек пуст");
            }
            T first = items[--head];
            count--;
            items[head] = default(T);
            return first;            
        }

        public void Push(T item)    //Добавление элемента в стек на первое место.
        {
            if (head == items.Length)
            {
                var newArray = new T[items.Length + 1];
                Array.Copy(items, 0, newArray, 0, head);
                items = newArray;
            }
            items[head++] = item;
            count++;
        }
        
        public T Peek() //Возврашение первого элемента.
         {
             if (head == 0)
             {
                 throw new InvalidProgramException();
             }
             return items[head - 1];
         }

        public void Clear()
        {
            Array.Clear(items, 0, count);
            head = 0;
            tail = 0;
            count = 0;
        }
    }
}
