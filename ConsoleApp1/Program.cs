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
            //реализуем интрефейс IEnumerable для использования foreach,
            //так же тип T должен реализовывать интерфейс IComparable
            //Этот интерфейс определяет метод CompareTo(T other), который используется для сравнения объектов типа T.
            //Он нужен для реализации сортировки
        {
            private T[] elements;
            private int head; //голова очереди
            private int tail; //хвост очереди

            public Queue() //O(1)
            {
                elements = new T[0]; //O(1)
                head = 0; //O(1)
                tail = -1; //O(1)
            }

            public void Push(T element) //добавляет элемент //O(1)
            {
                Array.Resize(ref elements, elements.Length + 1); //O(1)
                tail++; //O(1)
                elements[tail] = element; //O(1)
            }

            public T Pop() //Возвращает первый элемент очереди (добавленный раньше всех) и удаляет его //O(1)
            {
                if (head > tail)//O(1)
                    throw new InvalidOperationException("Queue is empty");

                T element = elements[head]; //O(1)
                head++; //O(1)
                return element; //O(1)
            }
            public T Peek() //Возвращает первый элемент очереди (добавленный раньше всех) //O(1)
            {
                if (head > tail) //O(1)
                    throw new InvalidOperationException("Queue is empty");

                return elements[head];//O(1)
            }


            public int Count //O(1)
            {
                get { return tail - head + 1; } //O(1)
            }
            public void InsertionSort() //O(n^2)
            {
                if (head == tail) //O(1)
                    return; //O(1)

                for (int i = head + 1; i <= tail; i++) //O(n^2)
                {
                    T current = elements[i]; //O(1)
                    int j = i - 1; //O(1)

                    while (j >= head && current.CompareTo(elements[j]) < 0)  //O(n)
                    {
                        elements[j + 1] = elements[j]; //O(1)
                        j--; //O(1)
                    }

                    elements[j + 1] = current;//O(1)
                }
            }


            public IEnumerator<T> GetEnumerator() //O(n)
            {
                int currentIndex = head; //O(1)
                for (int i = 0; i < Count; i++) //O(n)
                {
                    yield return elements[currentIndex]; //O(1)
                    currentIndex = (currentIndex + 1) % elements.Length; //O(1)
                }
            }

            IEnumerator IEnumerable.GetEnumerator() //O(n)
            {
                return GetEnumerator(); //O(n)
            }
        }
        

        static void Main()
        {
            Queue<int> queue = new Queue<int>();
            Random random = new Random();
            for (int i=0; i < 20; i++) 
            {
                queue.Push(random.Next(0,100));
            } //создаём очередь из случайных элементов
            Console.WriteLine("Исходная очередь: ");
            foreach (int item in queue)
            {
                Console.Write(item + " ");
            } //выводим очередь


            Console.WriteLine($"\n\nСработал метод Pop(): {queue.Pop()}"); //выписываем и удаляем самый первый элемент
            Console.WriteLine("Очередь после метода Pop: ");
            foreach (int item in queue)
            {
                Console.Write(item + " ");
            } //выводим очередь
            Console.WriteLine($"\n\nСработал метод Peek(): {queue.Peek()}"); //выписываем "новый" первый элемент
            Console.WriteLine("Очередь после метода Peek: ");
            foreach (int item in queue)
            {
                Console.Write(item + " ");
            } //выводим очередь
            
            queue.InsertionSort(); //сортируем очередь
            Console.WriteLine("\n\nПосле сортровки: ");
            foreach (int item in queue)
            {
                Console.Write(item + " ");
            } //выводим очередь
        }
    }
    
}