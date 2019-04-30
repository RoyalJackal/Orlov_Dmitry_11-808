using System;
using System.Collections;
using System.Collections.Generic;

namespace Heapsort
{
    public class ArrayBinaryHeap : IEnumerable<int>
    {
        //массив в виде бинарного дерева и количество совершённых итераций
        private int[] heap;
        public int Iterations { get; private set; }

        //создаём бинарное дерево из массива в конструкции
        public ArrayBinaryHeap(int[] heap)
        {
            this.heap = heap;
            BuildHeap();
        }

        //создаём дерево под index элементом массива до границы border
        private void Heapify(int index, int border)
        {
            //задаём наибольший под i, левый под 2i+1, правый под 2i+2
            int left = index * 2 + 1;
            int right = index * 2 + 2;
            int largest = index;
            //если боковой больше наибольшего, то меняем их местами
            if (left < border && heap[left] > heap[largest])
                largest = left;
            if (right < border && heap[right] > heap[largest])
                largest = right;
            //если наибольшмий поменялся, то меняем местами значение
            //и вызываем метод от предыдущей позиции наибольшего
            if (largest != index)
            {
                {
                    int temp = heap[index];
                    heap[index] = heap[largest];
                    heap[largest] = temp;
                }
                Iterations++;
                Heapify(largest, border);
            }
        }

        //метод создания бинарного дерева из массива
        private void BuildHeap()
        {
            //создаём дерево по элементами от i/2-1 до 0
            for (int i = heap.Length / 2 - 1; i >= 0; i--)
                Heapify(i, heap.Length);
        }

        //метод сортировки бинарного листа
        public void Sort()
        {
            //для каждого элемента с конца
            for (int i = heap.Length - 1; i > 0; i--)
            {
                {
                    //меняем местами первый и последний
                    int temp = heap[0];
                    heap[0] = heap[i];
                    heap[i] = temp;
                }
                Iterations++;
                //заного создаём дерево под 0 элементом
                Heapify(0, i);
            }
        }

        //вывод дерева в виде массива
        public void Write()
        {
            foreach (var item in heap)
            {
                Console.WriteLine(item);
            }
        }
        
        //реализация IEnumerable
        public IEnumerator<int> GetEnumerator()
        {
            foreach (var item in heap)
            {
                yield return item;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
