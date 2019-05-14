using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Refl1
{
    class Program
    {
        const string path = @"C:\Users\Юзер\source\repos\Refl1\Refl1\Folder";

        static void Main(string[] args)
        {
            //получаем список папок и вызываем метод проверки от каждой
            var students = Directory.EnumerateDirectories(path);
            Parallel.ForEach(students, CheckAnswers);
            Console.ReadKey();
        }

        public static void CheckAnswers(string studentFolder)
        {
            //получаем список заданий, список строк частей пути(для последней папки) и число правильных
            var tasks = Directory.EnumerateFiles(studentFolder);
            var studentFolderParts = studentFolder.Split('\\');
            int rightCount = 0;
            Parallel.ForEach(tasks, taskFolder =>
            {
                //разделяем информацию о задании через точку с запятой
                var taskData = File.ReadAllText(taskFolder).Split(';');
                //получаем тип, его пустой конструкто и создаём из неё переменную
                var type = Type.GetType("Refl1." + taskData[0]);
                var ctor = type.GetConstructor(new Type[] { });
                var classVariable = ctor.Invoke(new object[] { });
                //отделяем от информации о задании входные данные для метода и вызываем метод из переменной класса
                var input = new object[taskData.Length - 2];
                for (int i = 0; i < input.Length; i++)
                    input[i] = Int32.Parse(taskData[i + 2]);
                var result = type.GetMethod(taskData[1]).Invoke(classVariable, input);
                //разделяем путь до задания чтобы получить его названия и сравниваем результат с информацией из папки
                var folderParts = taskFolder.Split('\\');
                if (result.Equals(Int32.Parse(File.ReadAllText(path + @"\" + folderParts[folderParts.Length - 1]))))
                    rightCount++;
            });
            //когда досчитываем студента, выводим кол-во правильных ответов
            Console.WriteLine("{0} {1}", studentFolderParts[studentFolderParts.Length - 1], rightCount);
        }
    }
}
