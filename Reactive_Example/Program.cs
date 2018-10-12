using System;
using System.Linq;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading;

namespace Reactive_Example
{
    class Program
    {
        static void Main(string[] args)
        {
            TimeSample();

            Console.WriteLine("Press key to exit...");
            Console.ReadKey();

        }

        private static void TimeSample()
        {
            var random = new Random();
            var baseSource = Observable
                .Interval(TimeSpan.FromSeconds(0.5))
                .Select(x => random.Next(1, 100));

            var source = new ReplaySubject<int>(NewThreadScheduler.Default);
            baseSource.Subscribe(source);

            var sub1 = source.Subscribe(item => Console.WriteLine($"+++>[{Thread.CurrentThread.ManagedThreadId}] Received: {item}"));

            // FILTROWANIE
            var filtered = source
                .Where(n => n > 20)
                .Where(n => n < 50);
            filtered.Subscribe(item => Console.WriteLine($"--->[{Thread.CurrentThread.ManagedThreadId}] Received: {item}"));

            var buffered = source.Buffer(3); //.Buffer(xxxx, 1); for 1 elem shift. xxxx can be number of elements or TimeSpan
            buffered.Subscribe(item => Console.WriteLine($"===>[{Thread.CurrentThread.ManagedThreadId}] Received: {String.Join(", ", item)}"));

            //"awdgbwe3houerfwe4gfvwsedfcjhasd;'lkjwdfnadckvNISdmazs:dnkdsnfgpoWSDAlskn"
            //    .ToObservable()
            //    .Buffer(3)
            //    .Subscribe(item => Console.WriteLine($"ebe>[{Thread.CurrentThread.ManagedThreadId}] Received: {String.Join(", ", item)}"));
        }

        private static void SubjectSample()
        {
            var source = new ReplaySubject<string>(NewThreadScheduler.Default); //gorace, ReplySubject to zimne zrodlo

            source.OnNext("Before");

            var sub1 = source.Subscribe(
                item => Console.WriteLine($"--->[{Thread.CurrentThread.ManagedThreadId}] Received: {item}"),
                e => Console.WriteLine($"--->[{Thread.CurrentThread.ManagedThreadId}] Exception: {e.Message}"),
                () => Console.WriteLine($"<---[{Thread.CurrentThread.ManagedThreadId}] Finished."));


            source.OnNext("Hello");
            source.OnNext("World");

            var sub2 = source.Subscribe(
                item => Console.WriteLine($"+++>[{Thread.CurrentThread.ManagedThreadId}] Received: {item}"),
                e => Console.WriteLine($"+++>[{Thread.CurrentThread.ManagedThreadId}] Exception: {e.Message}"),
                () => Console.WriteLine($"<+++[{Thread.CurrentThread.ManagedThreadId}] Finished."));

            source.OnNext("Siemanko");
            source.OnNext("Siema");

            var sub3 = source.Subscribe(
                item => Console.WriteLine($"===>[{Thread.CurrentThread.ManagedThreadId}] Received: {item}"),
                e => Console.WriteLine($"===>[{Thread.CurrentThread.ManagedThreadId}] Exception: {e.Message}"),
                () => Console.WriteLine($"<===[{Thread.CurrentThread.ManagedThreadId}] Finished."));

            sub1.Dispose();

            source.OnNext("Wyjazd");
            source.OnNext("Do domu.");

            source.OnCompleted();

        }
    }
}
