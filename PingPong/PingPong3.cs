using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PingPong
{
    public static class PingPong3
    {
        static int _cur = 0;

        static void Ping()
        {
            while (true)
            {
                if (Interlocked.CompareExchange(ref _cur, 1, 0) == 0)
                {
                    Console.WriteLine("Ping");
                    Interlocked.Exchange(ref _cur, 2);
                }
            }
        }

        static void Pong()
        {
            while (true)
            {
                if (Interlocked.CompareExchange(ref _cur, 3, 2) == 2)
                {
                    Console.WriteLine("Pong");
                    Interlocked.Exchange(ref _cur, 0);
                }
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
