using System;
using System.Linq;
using System.Threading;

namespace Plinq_Example
{
    class Program
    {
        static void Main(string[] args)
        {
            PlinqTest();
            Console.WriteLine("Press key to exit...");
            Console.ReadKey();
        }

        public static void PlinqTest()
        {
            var cs = "abcdefgh"
                .AsParallel()
                .AsOrdered()
                .Select(c => foo(c))
                .AsSequential();
            Console.WriteLine(new string(cs.ToArray()));
            // if .OrderBy are stacked - last wins e.g.:
            // ...
            // .OrderBy(Name)
            // .OrderBy(LastName)
            // ...
            // will be sorted by LastName
            // better use .OrderBy and follow by .ThenBy
            Console.WriteLine("---------------------------------------------");

            Enumerable.Range(1, 30)
                .AsParallel() //by removing this line plinq will run sequentially but stop at 15
                .First(n => find(n, 15));



            //following will download pageges from 'sites' list and parse output to anonymous object list.

            //var q = from url in sites
            //        let result = Download(url) //implementation of downloading method
            //        select new <--- anonymous object
            //        {
            //            site = url,
            //            length = result.Length
            //        };
        }

        private static char foo(char c)
        {
            Console.WriteLine($"[{Thread.CurrentThread.ManagedThreadId}] {c}");
            return c;
        }

        private static bool find(int n, int lookFor)
        {
            Console.WriteLine(n);
            return n == lookFor;
        }
    }
}
