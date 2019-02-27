using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Plinq_Example
{
    class Program
    {
        static void Main(string[] args)
        {
            Example_PLINQ();
            Console.WriteLine("Press key to exit...");
            Console.ReadKey();
        }

        public static void Example_PLINQ()
        {
            var list = new List<string>
            {
                "dddd",
                "aaaa",
                "cccc",
                "dd",
                "aa",
                "cc",
                "ddd",
                "aaa",
                "ccc",
                "NOT NEEDED"
            };

            var filtered = list
                    .AsParallel()
                    .Where(elem => BadStrlen(elem) < 5)
                    .OrderBy(elem => elem.Length)
                    .ThenBy(elem => elem);

            // Force to evaluate lazy values
            var evaluated = new List<string>(filtered);
            foreach (string elem in evaluated)
            {
                Console.WriteLine(elem);
            }
        }
        public static void Example_PLINQ_to_SQL()
        {
            var list = new List<string>
            {
                "dddd",
                "aaaa",
                "cccc",
                "dd",
                "aa",
                "cc",
                "ddd",
                "aaa",
                "ccc",
                "NOT NEEDED"
            };

            var filtered = from elem in list.AsParallel()
                           where BadStrlen(elem) < 5
                           orderby elem.Length, elem
                           select elem;

            // Force to evaluate lazy values
            var evaluated = new List<string>(filtered);
            foreach (string elem in evaluated)
            {
                Console.WriteLine(elem);
            }
        }

        private static int BadStrlen(string msg)
        {
            Console.WriteLine($"[{Thread.CurrentThread.ManagedThreadId}] Length of {msg}");
            Thread.Sleep(TimeSpan.FromSeconds(0.5));
            Console.WriteLine($"[{Thread.CurrentThread.ManagedThreadId}] ...is {msg.Length}!");
            return msg.Length;
        }
    }
}
