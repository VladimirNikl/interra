using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Zadanie1
{
    class Program
    {
        static object bloked = new object();
        static Random random = new Random();
        static void Method()
        {
            Console.SetWindowSize(80, 40);
            int horiz = random.Next(0, 79);
            int chain = random.Next(1, 20);
            int k = 40 + chain;
            int m = 0;
            int n = 1;
            for (int i = 0; i < k; i++)
            {
                lock (bloked)
                {
                    Paint();
                }
                void Paint()
                {
                    for (int j = m; j < n; j++)  //рисование очередной части цепочки.
                    {
                        Console.SetCursorPosition(horiz, j);
                        if (n < 40)
                        {
                            if (j == n - 1)
                                Console.ForegroundColor = ConsoleColor.White;
                            else if (j == n - 2)
                                Console.ForegroundColor = ConsoleColor.Green;
                            else
                                Console.ForegroundColor = ConsoleColor.DarkGreen;
                        }
                        else
                        {
                            if (j == chain + m - 1)
                                Console.ForegroundColor = ConsoleColor.White;
                            else if (j == chain + m - 2)
                                Console.ForegroundColor = ConsoleColor.Green;
                            else
                                Console.ForegroundColor = ConsoleColor.DarkGreen;
                        }

                        Console.Write(0);
                    }
                }

                Thread.Sleep(70);
                // стирание очередной части цепочки.
                lock (bloked)
                {
                    Erasure();
                }
                void Erasure()
                {
                    for (int j = m; j < n; j++)
                    {
                        Console.SetCursorPosition(horiz, j);
                        Console.Write(" ");
                        // if (j >= 39)// j это не номер цикла(из40), а результирующее положение курсора по вертикали
                        //  chain--; // - сумма из положения курсора из цикла текущего звена цепочки.
                    }
                    // подготовка значений переменных для рисования очередной части цепочки.
                    if (n < chain)
                    {
                        n++; m = 0;
                    }
                    else
                    {
                        m = i - chain + 2;  // начальное вертикальное положение положение первого звена цепочки.
                        n = chain + m;
                        if (n >= 40)  // j - от ноля, нулевая строка есть. n - с единицы. 1 -печатает первую строку.
                            n = 40;// внизу консоли максимальное отклонение ограничивается нижним значением, а начало цепочки также спускается вниз 
                    } // т.е. колличество циклов -звеньев цепочки сокращается.
                }
            }
        }
        static void Main()
        {
            // привет!
            ThreadStart threadStart = new ThreadStart(Method);
            do
            {
                Thread thread = new Thread(threadStart);
                thread.Start();
                int delay = random.Next(100, 300);
                Thread.Sleep(delay);

            } while (true);
        }
    }
}
