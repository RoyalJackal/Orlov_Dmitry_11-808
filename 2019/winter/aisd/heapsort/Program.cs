using System;
using System.IO;
using System.Drawing;
using System.Windows.Forms.DataVisualization.Charting;
using System.Windows.Forms;

namespace Heapsort
{
    class Program
    {
        static void Main(string[] args)
        {
            //создание графов
            var arrayGraph = new Series();
            var linkedListGraph = new Series();

            //генерация файлов
            Generator.Generete();
            string[] input;
            for (int i = 1; i <= 100; i++)
            {
                //считываем файлы в массив и измеряем его значения
                input = File.ReadAllLines(String.Format("data{0}.txt", i));
                //Measurements.MeasureArrayTime(input, arrayGraph);
                //Measurements.MeasureLinkedListTime(input, linkedListGraph);
                //Measurements.MeasureArrayIterations(input, arrayGraph);
                //Measurements.MeasureLinkedListIterations(input, linkedListGraph);
            }
            //создание графика
            var chart = MakeChart(arrayGraph, linkedListGraph);
            var form = new Form() { ClientSize = new Size(800, 600) };
            form.Controls.Add(chart);
            Application.Run(form);
        }

        //метод создания графика
        private static Chart MakeChart(Series graph1, Series graph2)
        {
            var chart = new Chart();
            chart.ChartAreas.Add(new ChartArea());

            graph1.ChartType = SeriesChartType.FastLine;
            graph1.Color = Color.Red;

            graph2.ChartType = SeriesChartType.FastLine;
            graph2.Color = Color.Green;
            graph2.BorderWidth = 3;

            chart.Series.Add(graph1);
            chart.Series.Add(graph2);
            chart.Dock = DockStyle.Fill;
            return chart;
        }
    }
}
