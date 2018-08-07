using System;

namespace SourceObserver_Example
{
    class Program
    {
        static void Main(string[] args)
        {
            ColdSourceTest();

            Console.WriteLine("Press key to exit...");
            Console.ReadKey();
        }

        private static void ColdSourceTest()
        {
            var source = new MySource();
            var observer1 = new MyObserver();

            using (var sub = source.Subscribe(observer1))
            {
            }
        }
    }
}
