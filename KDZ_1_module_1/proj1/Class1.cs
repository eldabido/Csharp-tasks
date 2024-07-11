namespace Kaspari_19_KDZ_1
{
    internal class Class1
    {
        /// <summary>
        /// Метод GetValuesForArray - заполняет массив A значениями по указанной формуле.
        /// </summary>
        public static void GetValuesForArray(out double[,] A, int N)
        {
            // Инициализируем массив.
            A = new double[N, N];
            // n для подсчета значений.
            int n = 1;
            // Два цикла для обращения к элементам двумерного массива.
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    // Вычисляем конкретное значение и присваиваем элементу двумерного массива.
                    A[i, j] = (Math.Sqrt(9.0 * Math.Pow(n, 2) - 9) - 2 * n) / (2 - Math.Cbrt(Math.Pow(n, 3) + 5));
                    // Увеличиваем n для подсчёта следующего элемента.
                    n++;
                }
            }
        }

        /// <summary>
        ///  Метод CheckProhibSym проверяет строку на то, является ли она точкой или пробелом.
        /// </summary>
        public static bool CheckProhibSym(string path)
        {
            int len = path.Length;
            for (int i = 0; i < len; i++)
            {
                // Если есть один из этих запрещенных символов, то стоп.
                if (path[i] != ' ' && path[i] != '.')
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Метод PrintArray - для печати двумерного массива.
        /// </summary>
        public static void PrintArray(double[,] A, int N)
        {
            // Два цикла для обхода всех значений массива.
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    // Печать конкретного элемента.
                    Console.Write("{0:f2} ", A[i, j]);
                }
                // Переход на следующую строку для удобного вида.
                Console.WriteLine();
            }
        }

        /// <summary>
        /// Метод WhatYouDesire - спрашивает у пользователя, хочет ли он продолжить.
        /// </summary>
        public static string WhatYouDesire()
        {
            Console.WriteLine("Если хотите остановиться, то введите S");
            Console.WriteLine("Если хотите начать заново, то введите любой другой символ");
            return Console.ReadLine();
            
        }

        /// <summary>
        /// Метод WriteToFile - заполняет файл значениями массива.
        /// </summary>
        public static void WriteToFile(string filename, double[,] A, int N)
        {
            using (StreamWriter sw = File.CreateText(filename))
            {
                // Запись в файл согласно заданию.
                sw.WriteLine("{0} {1}", N, N);
                for (int i = 0; i < N; i++)
                {
                    for (int j = 0; j < N; j++)
                    {
                        sw.Write("{0:f2}*", A[i, j]);
                    }
                    // Добавка в конце ещё символа '*' (в цикле в конце строки будет только одна звеззда).
                    sw.WriteLine("*");
                }
                Console.WriteLine("Успешно записано");
            }
        }
    }
}
