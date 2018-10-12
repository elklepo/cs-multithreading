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
            Task aProducer = Task.Run(() => Producer(buff, "A", 0));
            Task bProducer = Task.Run(() => Producer(buff, "B", 5));

            Task cConsumer = Task.Run(() => Consumer(buff, "C"));
            Task.WaitAll(aProducer, bProducer, cConsumer);
        }

        public static void Consumer(BlockingCollection<int> buff, string name)
        {
            foreach (var item in buff.GetConsumingEnumerable())
            {
                Console.WriteLine($"-{name} {item}");
                Thread.Sleep(TimeSpan.FromMilliseconds(150));
            }
            Console.WriteLine($"Consumer {name} finished.");
        }

        public static void Producer(BlockingCollection<int> buff, string name, int start)
        {
            for (int i = start; i < start + 5; i++)
            {
                buff.Add(i);
                Console.WriteLine($"+{name} {i}");
                Thread.Sleep(TimeSpan.FromMilliseconds(200));
            }
            Console.WriteLine($"Producer {name} finished.");
            buff.CompleteAdding();
        }
    }
}
