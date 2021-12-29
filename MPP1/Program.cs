using System;
using System.Threading;

//Задание 1
/*Создать класс на языке C#, который:
- называется TaskQueue и реализует логику пула потоков;
- создает указанное количество потоков пула в конструкторе;
- содержит очередь задач в виде делегатов без параметров:
delegate void TaskDelegate();
- обеспечивает постановку в очередь и последующее выполнение
делегатов с помощью метода
void EnqueueTask(TaskDelegate task);*/

namespace FirstTask
{
    class Program
    {

        static void Main()
        {
            TaskQueue taskQueue = new TaskQueue(18);
            for (int i = 0; i < 18; i++)
            {
                taskQueue.EnqueueTask(OurTask);
            }
 
            taskQueue.Finish();
        }

        private static void OurTask()
        {
            for (int i = 0; i < 20; i++)
            {
                Console.WriteLine("current proc id = " + Thread.CurrentThread.ManagedThreadId + " task = " + i);
            }
        }
    }
}