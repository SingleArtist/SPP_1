using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

//Задание 1
/*Создать класс на языке C#, который:
- называется TaskQueue и реализует логику пула потоков;
-создает указанное количество потоков пула в конструкторе;
-содержит очередь задач в виде делегатов без параметров:
delegate void TaskDelegate();
-обеспечивает постановку в очередь и последующее выполнение
делегатов с помощью метода
void EnqueueTask(TaskDelegate task);*/

namespace FirstTask
{
    public class TaskQueue
    {
        private Thread[] threadPoool;
        public delegate void taskDelegate();
        private Queue<taskDelegate> TaskDelegateQueue = new Queue<taskDelegate>();

        public bool ContinueWork = true;

        public TaskQueue(int quantityThread)
        {
            this.threadPoool = new Thread[quantityThread];
            for (int i = 0; i < quantityThread; i++)
            {
                threadPoool[i] = new Thread(ThreadWork);//передается информация какой метод будет в потоке
                threadPoool[i].Start();//запуск потока
            }
        }
        public void ThreadWork()
        {
            taskDelegate task;
            while (ContinueWork)
            {
                try
                {
                    while (TaskDelegateQueue.Count > 0)
                    {
                        task = null;
                        lock (TaskDelegateQueue)//lock - значит не дает доступ к этому оду другим потокам
                        {
                            task = TaskDelegateQueue.Dequeue();//достаем задачу из очереди
                        }
                        if (task != null)
                        {
                            task.Invoke();//вызвать задачу (метод который находится в делегате)
                        }
                    }
                }
                catch
                {//похороны
                }
            }
        }
        public void EnqueueTask(taskDelegate task)//заполнение очереди
        {
            lock (TaskDelegateQueue)
            {
                TaskDelegateQueue.Enqueue(task);//запись задачи в очередь
            }
        }



        public void Finish()
        {
            ContinueWork = false;
        }
    }
}