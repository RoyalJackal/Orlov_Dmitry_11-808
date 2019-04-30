using System;
using System.Collections;
using System.Collections.Generic;

namespace Heapsort
{
    class LinkedListBinaryHeap : IEnumerable<int>
    {
        //связанный лист в виде бинарного дерева и количество совершённых итераций
        private LinkedList<int> heap;
        public int Iterations { get; private set; }

        //создаём бинарное дерево из листа в конструкции
        public LinkedListBinaryHeap(LinkedList<int> heap)
        {
            this.heap = heap;
            BuildHeap();
        }

        //создаём дерево под index элементом листа до границы border
        private void Heapify(int index, int border)
        {
            //задаём наибольший под i, левый под 2i+1, правый под 2i+2
            int left = index * 2 + 1;
            int right = index * 2 + 2;
            int largest = index;
            //если боковой больше наибольшего, то меняем их местами
            if (left < border && NodeAt(left).Value > NodeAt(largest).Value)
                largest = left;
            if (right < border && NodeAt(right).Value > NodeAt(largest).Value)
                largest = right;
            //если наибольшмий поменялся, то меняем местами значение
            //и вызываем метод от предыдущей позиции наибольшего
            if (largest != index)
            {
                {
                    int temp = NodeAt(index).Value;
                    NodeAt(index).Value = NodeAt(largest).Value;
                    NodeAt(largest).Value = temp;
                }
                Iterations++;
                Heapify(largest, border);
            }
        }

        //метод создания бинарного дерева из массива
        private void BuildHeap()
        {
            //создаём дерево по элементами от i/2-1 до 0
            for (int i = heap.Count / 2 - 1; i >= 0; i--)
                Heapify(i, heap.Count);
        }

        //метод сортировки бинарного листа
        public void Sort()
        {
            //для каждого элемента с конца
            for (int i = heap.Count - 1; i > 0; i--)
            {
                {
                    //меняем местами первый и последний
                    int temp = heap.First.Value;
                    heap.First.Value = NodeAt(i).Value;
                    NodeAt(i).Value = temp;
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

        //поиск элемента по индексу
        public LinkedListNode<int> NodeAt(int index)
        {
            var node = heap.First;
            //проход по всем элементам листа до элемента index
            for (int i = 0; i < index; i++)
            {
                node = node.Next;
                Iterations++;
            }
            return node;
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
