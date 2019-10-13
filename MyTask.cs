using System;
using System.IO;

namespace SPP_5
{
    class MyTask : IAction
    {
        private string oldFilePath;
        private string newFilePath;


        public MyTask(string oldFilePath, string recvDir)
        {
            string fileName = new FileInfo(oldFilePath).Name;
            this.oldFilePath = oldFilePath;
            newFilePath = recvDir + "\\" + fileName;
        }

        public void Action()
        {
            if (!File.Exists(newFilePath))
            {
                File.Copy(oldFilePath, newFilePath);
                Console.WriteLine($"На основе файла {oldFilePath} был создан файл {newFilePath}");
            }
            else
            {
                Console.WriteLine($"Произошла попытка скопировать уже существующий файл {oldFilePath}");
            }
        }
    }
}
