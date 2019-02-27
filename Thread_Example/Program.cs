using System;
using System.Threading;

namespace Thread_Example
{
    class Program
    {
        static void Main(string[] args)
        {
            var t1 = new Thread(DoWork);
            //t1.IsBackground = true; // Main thread will not wait for this thread to finish.

            var t2 = new Thread(() => DoWork());
            t1.Start();
            t2.Start();
            DoWork();
            t1.Join();
            t2.Join();

            Console.WriteLine("Press key to exit...");
            Console.ReadKey();
        }

        static void DoWork()
        {
            Console.WriteLine($"[{Thread.CurrentThread.ManagedThreadId}] starting work...");

            Thread.Sleep(TimeSpan.FromSeconds(1));

            Console.WriteLine($"[{Thread.CurrentThread.ManagedThreadId}] ...finished");
        }
    }
}
