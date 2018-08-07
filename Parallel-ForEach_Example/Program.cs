using System;
using System.Collections.Generic;
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
                "http://3lk.pl",
                "http://policja.pl",
                "http://pg.gda.pl",
                "http://ztm.pl",
                "http://reddit.com",
                "http://wykop.pl",
            };

            //The following is crazy shit and everyone will kill You during review - but it's cool :)

            //Parallel.ForEach(urls,
            //    () => new WebClient(),
            //    (url, loopstate, index, client) =>
            //    {
            //        ConsumerProducerTest();
            //        Donwload(client, url); // implementation of downloading method.
            //        return client;
            //    },
            //    client => client.Dispose());

            Parallel.ForEach(urls,
            url =>
            {
                Console.WriteLine($"[{Thread.CurrentThread.ManagedThreadId}] ---> {url}");
                Thread.Sleep(TimeSpan.FromSeconds(5));
                Console.WriteLine($"[{Thread.CurrentThread.ManagedThreadId}] <--- {url}");
            });
        }
    }
}
