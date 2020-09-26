using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3
{
    abstract class Base<T>
    {      
        public int head; 
        public int tail;
        public T[] items;
        public int count;
        public const int length = 2;

        public bool IsEmpty()
        {
            return head == 0;
        }
    }
}
