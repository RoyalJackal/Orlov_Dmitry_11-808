using System;
using System.Collections.Generic;
using System.Windows.Forms.DataVisualization.Charting;
using System.Diagnostics;

namespace Heapsort
{
    public static class Measurements
    {
        //вычисление времени сортировки дерева на массиве из input эелементов и запись его в граф
        public static void MeasureArrayTime(string[] input, Series arrayGraph)
        {
            int repetitions = 30;
            var watch = new Stopwatch();
            //переписываем входной массив в массив целочисленного типа
            var array = new int[input.Length];
            for (int j = 0; j < input.Length; j++)
                array[j] = Int32.Parse(input[j]);
            //замер времени
            watch.Start();
            for (int i = 0; i < repetitions; i++)
            {
                //создание и сортировка дерева
                var heap = new ArrayBinaryHeap(array);
                heap.Sort();
            }
            watch.Stop();
            //запись полученного результата в граф
            arrayGraph.Points.Add(new DataPoint(input.Length, (double)watch.ElapsedMilliseconds / repetitions));
        }

        //вычисление времени сортировки дерева на листе из input эелементов и запись его в граф
        public static void MeasureLinkedListTime(string[] input, Series linkedListGraph)
        {
            var watch = new Stopwatch();
            //переписываем входной массив в лист целочисленного типа
            var linkedList = new LinkedList<int>();
            foreach (var item in input)
                linkedList.AddLast(int.Parse(item));
            //замер времени
            watch.Start();
            //создание и сортировка дерева
            var heap = new LinkedListBinaryHeap(linkedList);
            heap.Sort();
            watch.Stop();
            //запись полученного результата в граф
            linkedListGraph.Points.Add(new DataPoint(input.Length, watch.ElapsedMilliseconds));
        }

        //вычисление итераций сортировки дерева на массиве из input эелементов и запись его в граф
        public static void MeasureArrayIterations(string[] input, Series arrayGraph)
        {
            //переписываем входной массив в массив целочисленного типа
            var array = new int[input.Length];
            for (int j = 0; j < input.Length; j++)
                array[j] = Int32.Parse(input[j]);
            //создание и сортировка дерева
            var heap = new ArrayBinaryHeap(array);
            heap.Sort();
            //запись поля с количеством итераций в граф
            arrayGraph.Points.Add(new DataPoint(input.Length, heap.Iterations));
        }

        //вычисление итераций сортировки дерева на листе из input эелементов и запись его в граф
        public static void MeasureLinkedListIterations(string[] input, Series linkedListGraph)
        {
            //переписываем входной массив в лист целочисленного типа
            var linkedList = new LinkedList<int>();
            foreach (var item in input)
                linkedList.AddLast(int.Parse(item));
            //создание и сортировка дерева
            var heap = new LinkedListBinaryHeap(linkedList);
            heap.Sort();
            //запись поля с количеством итераций в граф
            linkedListGraph.Points.Add(new DataPoint(input.Length, heap.Iterations));
        }
    }
}
