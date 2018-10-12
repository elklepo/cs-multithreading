using System;
using System.Threading;
using System.Threading.Tasks;

namespace Task_Example
{
    class Program
    {
        static void Main(string[] args)
        {
            //TaskCreationOptions

            var t1 = new Task(DoWork);
            t1.Start();
            var t2 = new Task(() => Strlen("hi"));
            t2.Start();
            var t3 = Task.Factory.StartNew(DoWork);
            var t4 = Task.Run(() => Strlen("hello"));

            Task.WaitAll(t1, t2, t3, t4);
            Console.WriteLine("WaitAll done.");

            var t5 = t4.ContinueWith(t => Console.WriteLine($"[{Thread.CurrentThread.ManagedThreadId}] t5 returned {t.Result}"))
                            .ContinueWith(t => Console.WriteLine($"[{ Thread.CurrentThread.ManagedThreadId}] We"))
                            .ContinueWith(t => Console.WriteLine($"[{ Thread.CurrentThread.ManagedThreadId}] should"))
                            .ContinueWith(t => Console.WriteLine($"[{ Thread.CurrentThread.ManagedThreadId}] be"))
                            .ContinueWith(t => Console.WriteLine($"[{ Thread.CurrentThread.ManagedThreadId}] run"))
                            .ContinueWith(t => Console.WriteLine($"[{ Thread.CurrentThread.ManagedThreadId}] sequentially"))
                            .ContinueWith(t => Console.WriteLine($"[{ Thread.CurrentThread.ManagedThreadId}] from"))
                            .ContinueWith(t => Console.WriteLine($"[{ Thread.CurrentThread.ManagedThreadId}] same"))
                            .ContinueWith(t => Console.WriteLine($"[{ Thread.CurrentThread.ManagedThreadId}] thread"));

            Console.WriteLine("Press key to exit...");
            Console.ReadKey();
        }

        static int Strlen(string msg)
        {
            Console.WriteLine($"[{Thread.CurrentThread.ManagedThreadId}] calculating length of: {msg}...");
            Thread.Sleep(TimeSpan.FromSeconds(3));
            return msg.Length;
        }

        static void DoWork()
        {
            Console.WriteLine($"[{Thread.CurrentThread.ManagedThreadId}] starting work...");

            Thread.Sleep(TimeSpan.FromSeconds(3));

            Console.WriteLine($"[{Thread.CurrentThread.ManagedThreadId}] ...finished");
        }
    }
}
