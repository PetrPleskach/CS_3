using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

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
                Console.Title = str[counter..] + str.Substring(0, counter);
                counter = (++counter != str.Length) ? counter : 0;
                Thread.Sleep(200);
            }
        }

        /// <summary>
        /// Рассчёт факторила, максимум до 65
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        private static ulong Factorial(ulong x)
        {
            if (x != 0) return x * Factorial(x - 1);
            return 1;
        }

        /// <summary>
        /// Расчёт суммы
        /// </summary>
        /// <param name="x"></param>
        private static void SumOfNumbersInNum(long x)
        {
            var thread = Thread.CurrentThread;
            Console.WriteLine("Поток {0} начал считать сумму", thread.ManagedThreadId);
            long sum = 0;

            if (x >= 0)
                for (long i = 0; i <= x; i++)
                    sum += i;

            if (x <= 0)
                for (long i = 1; i >= x; i--)
                    sum += i;

            Console.WriteLine("Сумма всех чисел от 1 до {0} равна: {1}", x, sum);
            Console.WriteLine("Поток {0} завешил работу", thread.ManagedThreadId);
        }

        static void Main(string[] args)
        {
            var titleThread = new Thread(TitleTicker)
            {
                IsBackground = true,
                Priority = ThreadPriority.Lowest
            };
            titleThread.Start();

            while (true)
            {
                bool flag = false;

                Console.WriteLine("Для расчета факториала введите число:");
                byte f = 0;
                do
                {
                    flag = byte.TryParse(Console.ReadLine(), out f);
                    if (!flag || f > 65)
                        Console.WriteLine("Введите целое число в диапазоне от {0} до 65", byte.MinValue);
                } while (!flag || f > 65);
                var taskFactoial = new Task<ulong>(() => Factorial(f));
                taskFactoial.Start();
                Console.WriteLine("Факториал {0} равен {1}", f, taskFactoial.Result);


                Console.WriteLine("Для подсчёта суммы от 1 до n введите n:");
                long n = 0;
                flag = false;
                while (!flag)
                {
                    flag = long.TryParse(Console.ReadLine(), out n);
                    if (!flag)
                        Console.WriteLine("Введите целое число в диапазоне от {0} до {1}", int.MinValue, int.MaxValue);
                }
                new Thread(() => SumOfNumbersInNum(n)).Start();

                Console.WriteLine("Нажмите ESC чтобы закочить, или любую клавишу чтобы продолжить...");
                var key = Console.ReadKey().KeyChar;
                Console.Clear();
                if (key == (char)ConsoleKey.Escape) break;                
            }

            Console.WriteLine("Основной поток завешил работу!");
            Console.ReadKey();
        }
    }
}
