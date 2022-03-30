using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PingPong
{
    public static class PingPong2
    {
        internal struct SimpleSpinLock
        {
            private Int32 m_ResourceInUse; // 0=false (по умолчанию), 1=true
            public void Enter()
            {
                while (true)
                {        // Всегда указывать, что ресурс используется.
                         // Если поток переводит его из свободного состояния,        
                         // вернуть управление        
                    if (Interlocked.Exchange(ref m_ResourceInUse, 1) == 0) return;         // Здесь что-то происходит... 
                }
            }
            public void Exit()
            {     // Помечаем ресурс, как свободный    
                Volatile.Write(ref m_ResourceInUse, 0);
            }
        }



        static int _cur;
        static SimpleSpinLock _lock = new SimpleSpinLock();

        static void Ping()
        {
            while (true)
            {
                _lock.Enter();
                if (_cur == 0)
                {
                    Console.WriteLine("Ping");
                    _cur++;
                }
                _lock.Exit();
            }
        }

        static void Pong()
        {
            while (true)
            {
                _lock.Enter();
                if (_cur == 1)
                {
                    Console.WriteLine("Pong");
                    _cur--;
                }
                _lock.Exit();
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
