using System;
using System.IO;

namespace SPP_5
{
    class Program
    {
        static public void ProcessDir(string srcDir, string recvDir, MyThreadPool myThreadPool)
        {
            string dirName = new DirectoryInfo(srcDir).Name;
            recvDir = recvDir + "\\" + dirName;
            if (!Directory.Exists(recvDir))
            {
                Directory.CreateDirectory(recvDir);
            }

            foreach(string file in Directory.GetFiles(srcDir))
            {
                myThreadPool.Queue(new MyTask(file, recvDir));
            }

            foreach (string s in Directory.GetDirectories(srcDir))
            {
                ProcessDir(s, recvDir, myThreadPool);
            }
        }

        static void Main(string[] args)
        {
            string srcDir;
            string recvDir;
            string strCountOfThreads;
            int countOfThreads;

            while (true)
            {
                Console.WriteLine("Введите директорию, из которой будут копироваться файлы: ");
                srcDir = Console.ReadLine();
                if (Directory.Exists(srcDir))
                    break;
            }

            while (true)
            {
                Console.WriteLine("Введите директорию, в которую будут копироваться файлы: ");
                recvDir = Console.ReadLine();
                if (Directory.Exists(recvDir))
                    break;
            }

            while (true)
            {
                Console.WriteLine("Введите максимальное киличество потоков: ");
                strCountOfThreads = Console.ReadLine();
                if (int.TryParse(strCountOfThreads, out countOfThreads))
                    break;
            }

            MyThreadPool myThreadPool = new MyThreadPool(countOfThreads);
            foreach (string dir in Directory.GetDirectories(srcDir))
            {
                ProcessDir(dir, recvDir, myThreadPool);
            }

            foreach (string file in Directory.GetFiles(srcDir))
            {
                myThreadPool.Queue(new MyTask(file, recvDir));
            }

            Console.ReadLine();
        }
    }
}
