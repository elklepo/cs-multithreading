using System;
using System.Threading;

namespace SourceObserver_Example
{
    class MyObserver : IObserver<string>
    {
        public void OnCompleted()
        {
            Console.WriteLine($"[{Thread.CurrentThread.ManagedThreadId}] <--- Finished.");
        }

        public void OnError(Exception error)
        {
            Console.WriteLine($"[{Thread.CurrentThread.ManagedThreadId}] XXXX Error occured: {error.Message}"); ;
        }

        public void OnNext(string value)
        {
            Console.WriteLine($"[{Thread.CurrentThread.ManagedThreadId}] ---> Received: {value}");
        }
    }
}
