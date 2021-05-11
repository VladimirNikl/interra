using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DopolnitZad
{
    class Program
    {
        static object bloked = new object(); // обязательно object ?
        static void Method(object argument)
        {
            int hash = Thread.CurrentThread.GetHashCode();
            lock (bloked)
            {
                if ((hash % 2) != 0)
                    Console.ForegroundColor = ConsoleColor.Yellow;
                else
                    Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(new string(' ', 23) + "Поток ID{0} первая часть", hash);
                //  }
                if ((int)argument < 6)
                {
                    ParameterizedThreadStart delegatethread = new ParameterizedThreadStart(Method);
                    Thread newthread = new Thread(delegatethread);
                    newthread.Start((int)argument + 2);
                }
                // }
                Console.WriteLine(new string(' ', 48) + "Поток ID{0} вторая часть", hash);
                Console.ForegroundColor = ConsoleColor.Gray;
            }
        } 
        static void Main()
        {
            ParameterizedThreadStart parameterized = new ParameterizedThreadStart(Method);
            Thread thread = new Thread(parameterized);
            thread.Start(0);
 
            int hash = Thread.CurrentThread.GetHashCode();
            Thread.Sleep(25);
            lock (bloked)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                for (int i = 0; i < 10; i++)
                {
                    Console.WriteLine("{1}.Основной поток ID{0}", hash, i);
                    Thread.Sleep(33);
                }
                Console.ForegroundColor = ConsoleColor.Gray;
            }
            Console.ReadKey();
        }
    }   // В критическую секцию объект блокировки запускает только один поток. Если в двух методах, в двух (и более) критических
}       // секциях один и тотже объект блокировки - в эти кр. секции одновременно запускается также любой один поток.
