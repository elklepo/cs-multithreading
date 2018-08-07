using System;
using System.IO;
using System.Linq;
using System.Reactive.Linq;

namespace ReactiveFileWatcher
{
    class Program
    {
        static void Main(string[] args)
        {
            FileSystemWatcherTest();

            Console.WriteLine("Press key to exit...");
            Console.ReadKey();
        }

        private static void FileSystemWatcherTest()
        {
            var path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            var fileSystemWatcher = new FileSystemWatcher(path);
            //fileSystemWatcher.IncludeSubdirectories = true;
            fileSystemWatcher.EnableRaisingEvents = true;

            var created = Observable.FromEventPattern<FileSystemEventHandler, FileSystemEventArgs>(
                    h => fileSystemWatcher.Created += h, //Subscribe ?
                    h => fileSystemWatcher.Created -= h) //Unsubscribe ?
                    .Select(x => x.EventArgs);

            var deleted = Observable.FromEventPattern<FileSystemEventHandler, FileSystemEventArgs>(
                    h => fileSystemWatcher.Deleted += h,
                    h => fileSystemWatcher.Deleted -= h)
                    .Select(x => x.EventArgs);

            created.Subscribe(file => Console.WriteLine($"Created: {file.Name}"));
            deleted.Subscribe(file => Console.WriteLine($"Deleted: {file.Name}"));

            //var both = created.Concat(deleted); // deleted will enable when created will disable.
            //var both = created.Merge(deleted); // both work
            var both = Observable.Merge(created, deleted); //also both work, can pass collection here
            both.Subscribe(file => Console.WriteLine($"Both: {file.Name}"));

        }
    }
}
