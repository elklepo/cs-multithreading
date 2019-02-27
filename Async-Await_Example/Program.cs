using System;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncAwait_Example
{
    class Program
    {
        static void Main(string[] args)
        {
            // Workaround for not being able to declare main() as async
            var t = Workaround();

            Console.WriteLine($"[{Thread.CurrentThread.ManagedThreadId}] Press key to exit...");
            Console.ReadKey();
        }

        private static async Task Workaround()
        {
            Console.WriteLine($"[{Thread.CurrentThread.ManagedThreadId}] Starting FIRST task.");
            var t1 = Task.Factory.StartNew(() => BadStrlen("FIRST"));
            Console.WriteLine($"[{Thread.CurrentThread.ManagedThreadId}] Starting SECOND task.");
            var t2 = Task.Factory.StartNew(() => BadStrlen("SECOND"));

            await Task.WhenAll(t1, t2);

            Console.WriteLine($"[{Thread.CurrentThread.ManagedThreadId}] t1={t1.Result}, t2={t2.Result}");

        }

        private static int BadStrlen(string msg)
        {
            Console.WriteLine($"[{Thread.CurrentThread.ManagedThreadId}] Length of {msg}");
            Thread.Sleep(TimeSpan.FromSeconds(3));
            Console.WriteLine($"[{Thread.CurrentThread.ManagedThreadId}] ...is {msg.Length}!");
            return msg.Length;
        }

    }
}
