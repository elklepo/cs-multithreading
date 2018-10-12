using System;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncAwait_Example
{
    class Program
    {
        static void Main(string[] args)
        {
            //Workaround for not being able to declare main() as async
            var t = Workaround();
            //t.Wait();
            Console.WriteLine($"[{Thread.CurrentThread.ManagedThreadId}] Press key to exit...");
            Console.ReadKey();
        }

        private static async Task Workaround()
        {
            var t1 = AsyncStrlen("FIRST");
            var t2 = AsyncStrlen("SECOND");
            var t3 = AsyncStrlen("THIRD");

            Console.WriteLine("See? I'm here!");

            //Task.WaitAll(t1, t2, t3); // this will lock us.
            var result = await Task.WhenAll(t1, t2, t3); // using this we'll return to main
            //'result' contains an array of results.
            Console.WriteLine($"[{Thread.CurrentThread.ManagedThreadId}] t1={t1.Result}, t2={t1.Result}, t3={t1.Result}");

        }

        private static async Task<int> AsyncStrlen(string msg)
        {
            Console.WriteLine($"[{Thread.CurrentThread.ManagedThreadId}] Running Strlen of {msg}...");
            int length = await Task.Run(() => Strlen(msg));
            Console.WriteLine($"[{Thread.CurrentThread.ManagedThreadId}] ...Length of {msg}:  {length}");
            return length;
        }

        static int Strlen(string msg)
        {
            Console.WriteLine($"[{Thread.CurrentThread.ManagedThreadId}] calculating length of: {msg}");
            Thread.Sleep(TimeSpan.FromSeconds(3));
            return msg.Length;
        }
    }
}
