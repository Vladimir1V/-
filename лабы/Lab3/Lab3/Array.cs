using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3
{
    class Array<T> : Base <T>
    {
        public Array()
        {
            count = 0;
            items = new T[length];
        }

        public Array(int length)
        {
            items = new T[length];
        }

        public void Add(T item) //Добавление элемента
        {
            if (count == items.Length)
            {
                var newArray = new T[items.Length + 1];
                Array.Copy(items, newArray, items.Length);
                items = newArray;
            }
            items[count] = item;
            count++; 
        }

        public void RemoveAt (int i) //Удаление элемента по индексу.
        {
            items[i] = default(T);
            for (int j = i; j < count - 1; j++)
            {
                items[j] = items[j + 1];
                items[j + 1] = default(T);
            }
            count--;
        }
        public void Clear() // Очистка массива.
        {
            Array.Clear(items, 0, count);
            head = 0;
            tail = 0;
            count = 0;
        }
    }
}
