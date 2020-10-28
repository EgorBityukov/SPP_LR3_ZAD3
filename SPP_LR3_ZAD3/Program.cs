using System;
using System.Threading;

namespace SPP_LR3_ZAD3
{
    class Program
    {
        static Mutex mutex = new Mutex();

        static int x=0;
        static void Main(string[] args)
        {
            for (int i = 0; i < 5; i++)
            {
                Thread myThread = new Thread(Count);
                myThread.Name = "Поток " + i.ToString();
                myThread.Start();
            }

            Console.ReadLine();
        }
        public static void Count()
        {
            mutex.Lock();
            x = 1;
            for (int i = 1; i < 9; i++)
            {
                Console.WriteLine(Thread.CurrentThread.Name + ": " + x.ToString());
                x++;
                Thread.Sleep(100);
            }
            mutex.Unlock();
        }
    }

    class Mutex
    {
        private int idWorkThread=-1;

        public void Lock()
        {
            int idCurThread = Thread.CurrentThread.ManagedThreadId;

            while (Interlocked.CompareExchange(ref idWorkThread, idCurThread, -1) != -1)
            {
                Thread.Sleep(10);
            }
            
        }

        public void Unlock()
        {
            int idCurThread = Thread.CurrentThread.ManagedThreadId;
            Interlocked.CompareExchange(ref idWorkThread, -1, idCurThread);
        }
    }
}
