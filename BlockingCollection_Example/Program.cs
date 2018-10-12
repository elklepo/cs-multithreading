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
            Task.Run(() => ConsumerProducerTest());

            Console.WriteLine("Press key to exit...");
            Console.ReadKey();
        }

        public static void ConsumerProducerTest()
        {
            var buff = new BlockingCollection<int>();
            Task tProducer = Task.Run(() => Producer(buff));
            Task tConsumer = Task.Run(() => Consumer(buff));
            Task.WaitAll(tProducer, tConsumer);
        }

        public static void Consumer(BlockingCollection<int> buff)
        {
            foreach (var item in buff.GetConsumingEnumerable())
            {
                Console.WriteLine($"- {item}");
            }
            Console.WriteLine("Consumer finished.");
        }

        public static void Producer(BlockingCollection<int> buff)
        {
            for (int i = 0; i < 10; i++)
            {
                buff.Add(i);
                Console.WriteLine($"+ {i}");
                Thread.Sleep(TimeSpan.FromMilliseconds(200));
            }
            Console.WriteLine("Producer finished.");
            buff.CompleteAdding();
        }
    }
}
