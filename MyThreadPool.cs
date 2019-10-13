using System;
using System.Collections.Generic;
using System.Threading;

namespace SPP_5
{ 
    class MyThreadPool
    {
        private const int TIME_OF_SLEEPING = 100;

        public int CountOfThreads { get; private set; }
        private Queue<IAction> tasks;
        private object locker;
        private List<Thread> threads;
        
        public MyThreadPool(int countOfThreads)
        {
            if (countOfThreads > 0)
            {
                CountOfThreads = countOfThreads;
            }
            else
            {
                throw new ArgumentException("Было получено некорректное значение количества потоков(countOfThreads)");
            }

            locker = new object();
            tasks = new Queue<IAction>();
            threads = new List<Thread>();

            for(int i = 0; i <= this.CountOfThreads; i++)
            {
                Thread thread = new Thread(new ThreadStart(ProcessThread));
                threads.Add(thread);
                thread.Start();
            }
        }

        private void ProcessThread()
        {
            while (true)
            {
                if (tasks.Count != 0)
                {
                    lock (locker)
                    {
                        if (tasks.Count != 0)
                        {
                            IAction task = tasks.Dequeue();
                            task.Action();
                        }
                    }
                }
                else
                {
                    Thread.Sleep(TIME_OF_SLEEPING);
                }
            }
        }

        public void Queue(IAction task)
        {
            lock (locker)
            {
                tasks.Enqueue(task);
            }
        }
    }
}
