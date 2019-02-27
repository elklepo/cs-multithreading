using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace ParallelForEach_Example
{
    class Program
    {
        static void Main(string[] args)
        {
            ForEachTest();

            Console.WriteLine("Press key to exit...");
            Console.ReadKey();
        }

        public static void ForEachTest()
        {
            var urls = new List<string>
            {
                "http://wp.pl",
                "http://intel.com",
                "http://f1.com",
                "http://google.com",
                "http://o2.pl",
                "http://youtube.com",
                "http://reddit.com",
                "http://wykop.pl",
            };

            Parallel.ForEach(urls,
            url =>
            {
                Console.WriteLine($"[{Thread.CurrentThread.ManagedThreadId}] ---> {url}");
                Thread.Sleep(TimeSpan.FromSeconds(5));
                Console.WriteLine($"[{Thread.CurrentThread.ManagedThreadId}] <--- {url}");
            });


            Parallel.ForEach(urls,
            () => new WebClient(),
            (url, loopstate, index, client) =>
            {
                var content = client.DownloadString(url);
                Console.WriteLine($"{url}, len={content.Length}");
                return client;
            },
            client => client.Dispose());

        }
    }
}
