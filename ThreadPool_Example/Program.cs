using System;
using System.Threading;

namespace ThreadPool_Example
{
    class Program
    {
        static void Main(string[] args)
        {
            for (int i = 0; i < 100; i++)
            {
                ThreadPool.QueueUserWorkItem(DoWork, i);
            }
            Console.WriteLine("Press key to exit...");
            Console.ReadKey();
        }

        static void DoWork(object obj)
        {
            Console.WriteLine($"[{Thread.CurrentThread.ManagedThreadId}] starting work... {obj}");

            Thread.Sleep(TimeSpan.FromMilliseconds(1));

            Console.WriteLine($"[{Thread.CurrentThread.ManagedThreadId}] ...finished");
        }
    }
}
