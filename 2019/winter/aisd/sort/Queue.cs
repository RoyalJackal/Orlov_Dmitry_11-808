using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sort
{
    //самодельный элемент очереди
    class QueueItem<T>
    {
        public T Value;
        public QueueItem<T> Next;
        public QueueItem<T> Prev;

        public QueueItem(T item)
        {
            Value = item;
        }
    }
    
    //самодельная очередь(потому что я не знал что в шарпе уже есть очередь)
    class Queue<T>
    {
        private QueueItem<T> head;
        private QueueItem<T> tail;

        public void Push(T item)
        {
            if (head == null)
            {
                head = new QueueItem<T>(item);
                tail = head;
            }
            else
            {
                tail.Next = new QueueItem<T>(item);
                tail = tail.Next;
            }
        }

        public T Pop()
        {
            if (head == null) throw new InvalidOperationException();
            T result = head.Value;
            head = head.Next;
            return result;
        }

        public bool IsEmpty()
        {
            return head == null;
        }
    }
}
