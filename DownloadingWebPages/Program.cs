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
            using (var cts = new CancellationTokenSource())
            {
                var ct = cts.Token;
                var t1 = TaskDownload.DownloadStringAsync("http://www.altkom.pl");
                var t2 = TaskDownload.DownloadStringAsync("http://www.intel.com", ct);
                cts.Cancel();
                Task.WaitAll();
                Console.WriteLine("Altkom length: " + t1.Result.Length);
                Console.WriteLine("Intel length: " + t2.Result.Length);
            }

            Console.WriteLine("Press key to exit...");
            Console.ReadKey();
        }
    }
    class TaskDownload
    {
        public static async Task<string> DownloadStringAsync(string uri, CancellationToken ct)
        {
            try
            {
                using (var client = new WebClient())
                using (var registration = ct.Register(() => client.CancelAsync()))
                {
                    var content = await client.DownloadStringTaskAsync(uri);
                    return content;
                }
            }
            catch (WebException ex) when (ex.Status == WebExceptionStatus.RequestCanceled)
            {
                Console.WriteLine($"RequestCanceled: {ex.Message}");
                return "";
            }
        }

        public static async Task<string> DownloadStringAsync(string uri)
        {
            using (var client = new WebClient())
            {
                var content = await client.DownloadStringTaskAsync(uri);
                return content;
            }
        }
    }

}
