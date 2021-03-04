using System;
using System.Threading;

namespace ConsoleTest
{
    class Program
    {
        /// <summary>
        /// Бегущая строка в заголовке
        /// </summary>
        private static void TitleTicker()
        {
            string str = "Hello World!      ";
            int counter = 0;
            while (true)
            {
                Console.Title = str.Substring(counter, str.Length - counter) + str.Substring(0, counter);
                counter = (++counter != str.Length) ? counter : 0;
                Thread.Sleep(200);
            }
        }

        private static long Factorial(long x)
        {
            if (x != 0) return x * Factorial(x - 1);
            return 1;
        }

        static void Main(string[] args)
        {
            new Thread(TitleTicker).Start();

            while (true)
            {
                int a = int.Parse(Console.ReadLine());
                long s = Factorial(a);

                Console.WriteLine(s.ToString());
            }
        }
    }
}
