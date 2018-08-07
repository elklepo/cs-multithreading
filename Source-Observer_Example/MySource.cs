using System;

namespace SourceObserver_Example
{
    class MySource : IObservable<string>
    {
        public IDisposable Subscribe(IObserver<string> observer)
        {
            observer.OnNext("Hello");
            observer.OnNext("World");

            observer.OnCompleted();

            return new Unsubscriber();
        }

        private class Unsubscriber : IDisposable
        {
            public void Dispose()
            {

            }
        }
    }
}
