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
            Example_Subject();

            Console.WriteLine("Press key to exit...");
            Console.ReadKey();

        }

        private static void Example_Random()
        {
            var random = new Random();
            var randomIntGenerator = Observable
                .Interval(TimeSpan.FromSeconds(1))
                .Select(x => random.Next());

            var randomIntSource = new ReplaySubject<int>(NewThreadScheduler.Default);
            randomIntGenerator.Subscribe(randomIntSource);

            var randomCharSource = randomIntSource.Select(n => (char)((n % 94) + 33));

            var randomCharPrinter = randomCharSource.Subscribe(item => Console.WriteLine(item));

            var randomEvenIntPrinter = randomIntSource
                .Where(n => n % 2 == 0)
                .Subscribe(item => Console.WriteLine(item));

            var randomThreeIntsPrinter = randomIntSource
                .Buffer(3)
                .Subscribe(item => Console.WriteLine(String.Join(", ", item)));
        }

        private static void Example_Subject()
        {
            var source = new ReplaySubject<string>(NewThreadScheduler.Default); // Subject

            source.OnNext("no subs");

            var sub1 = source.Subscribe(
                item => Console.WriteLine($"--->[{Thread.CurrentThread.ManagedThreadId}] Received: {item}"),
                e => Console.WriteLine($"--->[{Thread.CurrentThread.ManagedThreadId}] Exception: {e.Message}"),
                () => Console.WriteLine($"<---[{Thread.CurrentThread.ManagedThreadId}] Finished."));

            source.OnNext("only one sub");

            var sub2 = source.Subscribe(
                item => Console.WriteLine($"+++>[{Thread.CurrentThread.ManagedThreadId}] Received: {item}"),
                e => Console.WriteLine($"+++>[{Thread.CurrentThread.ManagedThreadId}] Exception: {e.Message}"),
                () => Console.WriteLine($"<+++[{Thread.CurrentThread.ManagedThreadId}] Finished."));

            source.OnNext("two subs!");

            var sub3 = source.Subscribe(
                item => Console.WriteLine($"===>[{Thread.CurrentThread.ManagedThreadId}] Received: {item}"),
                e => Console.WriteLine($"===>[{Thread.CurrentThread.ManagedThreadId}] Exception: {e.Message}"),
                () => Console.WriteLine($"<===[{Thread.CurrentThread.ManagedThreadId}] Finished."));

            source.OnNext("wow! three subscribers!");
            source.OnCompleted();

            Console.WriteLine("<--- DONE --->");

        }
    }
}
