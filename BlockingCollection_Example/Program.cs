using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace BlockingCollection_Example
{
    class Program
    {
        static void Main(string[] args)
        {
            Task.Run(() => Example_ConsumerProducer());

            Console.WriteLine("Press key to exit...");
            Console.ReadKey();
        }

        public static void Example_ConsumerProducer()
        {
            var bucket = new BlockingCollection<int>();

            Task producer = Task.Run(() => Producer(bucket, 0, 15));
            Task consumer = Task.Run(() => Consumer(bucket));

            Task.WaitAll(producer, consumer);
        }

        public static void Consumer(BlockingCollection<int> bucket)
        {
            foreach (var i in bucket.GetConsumingEnumerable())
            {
                Console.WriteLine($"- {i}");
            }
            Console.WriteLine($"Consumer finished.");
        }

        public static void Producer(BlockingCollection<int> bucket, int start, int stop)
        {
            for (int i = start; i < stop; i++)
            {
                bucket.Add(i);
                Console.WriteLine($"+ {i}");
                Thread.Sleep(TimeSpan.FromMilliseconds(200));
            }
            bucket.CompleteAdding();

            Console.WriteLine($"Producer finished.");
        }
    }
}
