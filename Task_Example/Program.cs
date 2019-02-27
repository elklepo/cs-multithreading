using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Task_Example
{
    class Program
    {
        static void Main(string[] args)
        {
            Example_DifferentWays();

            Console.WriteLine("Press key to exit...");
            Console.ReadKey();
        }
        static void Example_DifferentWays()
        {
            var t1 = new Task(DoWork);
            t1.Start();
            var t2 = new Task(() => Strlen("hi"));
            t2.Start();
            var t3 = Task.Factory.StartNew(DoWork);
            var t4 = Task.Run(() => Strlen("hello"));

            Task.WaitAll(t1, t2, t3, t4);
            Console.WriteLine("WaitAll done.");
        }

        static void Example_TaskSchedulerHints()
        {
            var list = new List<Task>();

            for (int i = 0; i < 10; i++)
            {
                var tmp = new Task(() => DoWork(), TaskCreationOptions.LongRunning);

                tmp.Start();
                list.Add(tmp);
            }

            Task.WaitAll(list.ToArray());
        }

        static void Example_TaskContinuation()
        {
            var t1 = Task.Run(() => Strlen("Let's go"));

            var t2 = t1.ContinueWith(t => Console.WriteLine($"[{ Thread.CurrentThread.ManagedThreadId}] Previous task said that \"Let's go\" is  {t.Result} char long"))
                            .ContinueWith(t => Console.WriteLine($"[{ Thread.CurrentThread.ManagedThreadId}] We"))
                            .ContinueWith(t => Console.WriteLine($"[{ Thread.CurrentThread.ManagedThreadId}] should"))
                            .ContinueWith(t => Console.WriteLine($"[{ Thread.CurrentThread.ManagedThreadId}] be"))
                            .ContinueWith(t => Console.WriteLine($"[{ Thread.CurrentThread.ManagedThreadId}] run"))
                            .ContinueWith(t => Console.WriteLine($"[{ Thread.CurrentThread.ManagedThreadId}] sequentially"))
                            .ContinueWith(t => Console.WriteLine($"[{ Thread.CurrentThread.ManagedThreadId}] from"))
                            .ContinueWith(t => Console.WriteLine($"[{ Thread.CurrentThread.ManagedThreadId}] the"))
                            .ContinueWith(t => Console.WriteLine($"[{ Thread.CurrentThread.ManagedThreadId}] same"))
                            .ContinueWith(t => Console.WriteLine($"[{ Thread.CurrentThread.ManagedThreadId}] thread"));
            t2.Wait();
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
