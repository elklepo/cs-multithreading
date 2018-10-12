using System;
using System.Threading;
using System.Threading.Tasks;

namespace InterruptingTasks_Example
{
    /*
     * BETTER RUN THIS EXAMPLE FROM COMMANDLINE, VISUAL IS MESSING WITH EXCEPTIONS
     */
    class Program
    {
        static void Main(string[] args)
        {
            var cts = new CancellationTokenSource();
            var ct = cts.Token;
            var t1 = Task.Run(() => DoWork(ct));

            Console.ReadKey();
            try
            {
                cts.Cancel();
                t1.Wait();
            }
            catch (AggregateException e)
            {
                Console.WriteLine($"\nException: {e.Message}");
            }
            Console.WriteLine($"Task status: {t1.Status}");
            Console.WriteLine("Press key to exit...");
            Console.ReadKey();
        }

        private static void DoWork(CancellationToken ct)
        {
            for (int i = 0; ; i++)
            {
                //ct.ThrowIfCancellationRequested(); // or just check cancellation token and gracefully finish
                                                    // but then task status will be RunToCompletion instead of Faulted.
                Console.Write("\r \r" + @"|/-\|/-\"[i % 8]);
                Thread.Sleep(TimeSpan.FromMilliseconds(200));
            }
        }
    }
}
