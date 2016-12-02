using System;
using System.Diagnostics;
using System.Net;
using System.Threading;

namespace coc_api
{
    class Program
    {
        static void Main(string[] args)
        {
            // Proxy as a proof of concept starting point

            var sw = Stopwatch.StartNew();

            var proxy = new Proxy();
            proxy.Start(new IPEndPoint(IPAddress.Any, 9339));

            sw.Stop();

            Console.WriteLine("Done({0}ms)! Listening on *:9339", sw.Elapsed.TotalMilliseconds);
            Thread.Sleep(Timeout.Infinite);
        }
    }
}
