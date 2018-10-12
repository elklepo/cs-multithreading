    using System;
using System.Linq;
using System.Threading;

namespace Delegate_Example
{
    class Program
    {
        delegate void Send(string msg);
        static private Send d_send;

        private static void SendSms(string msg)
        {
            Console.WriteLine($"Sending SMS: {msg}");
        }

        private static void SendEmail(string msg)
        {
            Console.WriteLine($"Sending e-mail: {msg}");
        }


        static void Main(string[] args)
        {
            d_send = SendSms;

            d_send("Hi");

            d_send += SendEmail;
            d_send += SendSms;
            d_send += SendEmail;
            //anonymous method
            d_send += delegate (string msg)
            {
                Console.WriteLine($"Sending anonymous: {msg}");
            };
            //lambda expression
            d_send += msg => Console.WriteLine($"Sending lambda: {msg}");

            d_send("Hello");

            d_send -= SendSms;
            d_send -= SendEmail;
            d_send -= SendEmail;
            d_send("Bye");

            //get delegates list - good to know but if You need this it means that You've missed good way.
            var delegates = d_send.GetInvocationList().ToList();

            Console.WriteLine("Press key to exit...");
            Console.ReadKey();
        }
    }
}