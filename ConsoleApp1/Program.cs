using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

namespace ConsoleApp
{
    class Program
    {
        
        public class Queue<T> : IEnumerable<T> where T: IComparable<T>
        {
            private T[] elements;
            private int head;
            private int tail;

            public Queue()
            {
                elements = new T[0];
                head = 0;
                tail = -1;
            }

            public void Enqueue(T element)
            {
                Array.Resize(ref elements, elements.Length + 1);
                tail++;
                elements[tail] = element;
            }

            public T Dequeue()
            {
                if (head > tail)
                    throw new InvalidOperationException("Queue is empty");

                T element = elements[head];
                head++;
                return element;
            }
            public T Peek()
            {
                if (head > tail)
                    throw new InvalidOperationException("Queue is empty");

                return elements[head];
            }


            public int Count
            {
                get { return tail - head + 1; }
            }
            public void InsertionSort()
            {
                if (head == tail)
                    return;

                for (int i = head + 1; i <= tail; i++)
                {
                    T current = elements[i];
                    int j = i - 1;

                    while (j >= head && current.CompareTo(elements[j]) < 0)
                    {
                        elements[j + 1] = elements[j];
                        j--;
                    }

                    elements[j + 1] = current;
                }
            }


            public IEnumerator<T> GetEnumerator()
            {
                int currentIndex = head;
                for (int i = 0; i < Count; i++)
                {
                    yield return elements[currentIndex];
                    currentIndex = (currentIndex + 1) % elements.Length;
                }
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }
        

        static void Main()
        {
            Queue<int> queue = new Queue<int>();
            queue.Enqueue(3);
            queue.Enqueue(1);
            queue.Enqueue(4);
            queue.Enqueue(1);
            queue.Enqueue(5);
            queue.Enqueue(3);
            queue.Enqueue(3);
            queue.Enqueue(5);
            queue.Enqueue(8);
            queue.Enqueue(9);


            foreach (int item in queue)
            {
                Console.Write(item+" ");
            }

            queue.InsertionSort();
            Console.WriteLine("После сортровки: ");
            foreach (int item in queue)
            {
                Console.Write(item + " ");
            }
        }
    }
    
}