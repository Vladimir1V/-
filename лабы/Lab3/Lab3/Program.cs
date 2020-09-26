using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("СТЕК");
            Stack<string> Stack = new Stack<string>();
            // Добавляем элементы методом Push
            Stack.Push("мяч");
            Stack.Push("дерево");
            Stack.Push("космолет");
            Stack.Push("январь");
            Console.WriteLine("Добавленные элементы:");
            foreach (Object o in Stack.items)
                Console.Write("{0} ",o);

            // Извлекаем элемент методом Pop
            var head = Stack.Pop();
            Console.WriteLine("\nИзвлеченный элемент методом Pop - {0}", head);

            // Возвращаем первый элемент методом Peek
            head = Stack.Peek();
            Console.WriteLine("Возвращаемый элемент методом Peek - {0}", head);
            Console.WriteLine("Оставшиеся элементы:");
            foreach (Object o in Stack.items)
                Console.Write("{0} ", o);
            Stack.Clear();
            Console.ReadLine();


            Console.WriteLine("\nОЧЕРЕДЬ");
            Queue<string> Queue = new Queue<string>();
            // Добавляем элементы методом Enqueue
            Queue.Enqueue("мяч");
            Queue.Enqueue("дерево");
            Queue.Enqueue("космолет");
            Queue.Enqueue("январь");
            Console.WriteLine("Добавленные элементы:");
            foreach (Object o in Queue.items)
                Console.Write("{0} ", o);

            //Извлекаем элемент методом Dequeue
            var qhead = Queue.Dequeue();
            Console.WriteLine("\nИзвлеченный элемент методом Dequeue - {0}", qhead);

            //Возвращаем первый элемент методом Peek
            qhead = Queue.Peek();
            Console.WriteLine("Возвращаемый элемент методом Peek - {0}", qhead);
            Console.WriteLine("Оставшиеся элементы:");
            foreach (Object o in Queue.items)
                Console.Write("{0} ", o);
            Queue.Clear();
            Console.ReadLine();


            Console.WriteLine("\nМАССИВ");
            Array<string> Array = new Array<string>();
            //Добавляем элементы методом Add
            Array.Add("мяч");
            Array.Add("дерево");
            Array.Add("космолет");
            Array.Add("январь");
            Console.WriteLine("Добавленные элементы:");
            foreach (Object o in Array.items)
                Console.Write("{0} ", o);

            //Удаление элемента
            Console.WriteLine("\nУдаляемый элемент - {0}", Array.items[1]);
            Array.RemoveAt(1);

            Console.WriteLine("Оставшиеся элементы:");
            foreach (Object o in Array.items)
                Console.Write("{0} ", o);
            Array.Clear();
            Console.ReadKey();
        }  
    }
}
