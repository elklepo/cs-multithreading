using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace DownloadingWebPages
{
    class Program
    {
        static void Main(string[] args)
        {
            var t1 = TaskDownload.DownloadStringAsync("http://www.intel.com");

            Console.WriteLine("Let's wait for async task.");
            t1.Wait();

            Console.WriteLine("Press key to exit...");
            Console.ReadKey();
        }
    }
    class TaskDownload
    {
        public static async Task DownloadStringAsync(string uri)
        {
            using (var client = new WebClient())
            {
                var content = await client.DownloadStringTaskAsync(uri);
                Console.WriteLine($"{uri} content length = {content.Length}");
            }
        }
    }

}
