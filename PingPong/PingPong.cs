using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PingPong
{
    public static class PingPong
    {
        static SemaphoreSlim semPing = new SemaphoreSlim(1, 1);
        static SemaphoreSlim semPong = new SemaphoreSlim(0, 1);
        static void Ping()
        {
            while (true)
            {
                semPing.Wait();
                Console.WriteLine("Ping");
                semPong.Release();
            }
        }

        static void Pong()
        {
            while (true)
            {
                semPong.Wait();
                Console.WriteLine("Pong");
                semPing.Release();
            }
        }

        public static void Start()
        {

            Task.Run(Ping);
            Task.Run(Ping);
            Task.Run(Pong);
            Task.Run(Pong);

            Console.ReadLine();
        }
    }
}
