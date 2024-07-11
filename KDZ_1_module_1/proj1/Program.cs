using Kaspari_19_KDZ_1;
using System;

class Program
{
    static void Main()
    {
        // Бесконечный цикл для повтора решения.
        while (true)
        {
            Console.WriteLine("Введите N - размерность двумерного массива (1 <= N <= 17)");
            bool result = int.TryParse(Console.ReadLine(), out int N);
            // Для проверки корректности ввода.
            string str;
            // Если вместо N ввели даже не число.
            if (!result)
            {
                Console.WriteLine("Неверный ввод N!!!");
                Console.WriteLine("N принимает целые значения от 1 до 17");
                str = Class1.WhatYouDesire();
                // Если ввели S, то STOP.
                if (str == "S")
                {
                    break;
                }
            }
            // Проверка на корректность ввода.
            else if (N < 1 || N > 17)
            {
                Console.WriteLine("Неверный ввод N!!!");
                Console.WriteLine("N принимает целые значения от 1 до 17");
                str = Class1.WhatYouDesire();
                // Если ввели S, то STOP.
                if (str == "S")
                {
                    break;
                }

            }
            // Если все хорошо, то начинаем работу.
            else
            {
                // Заводим двумерный массив A.
                double[,] A;
                // Вызываем метод GetValuesForArray, которая заполняет массив значениями.
                Class1.GetValuesForArray(out A, N);

                // Начинаем работу с файлом.
                Console.WriteLine("Введите название файла (без использования запрещенных символов):");
                string path = Console.ReadLine();
                // Вызов метода, заполняющего файл.
                // Проверка на запрещенные символы (название файла пробелами или точками) и другие ошибки из IOException.
                if (!Class1.CheckProhibSym(path))
                {
                    Console.WriteLine("Ошибка в именовании файла!");
                }
                else
                {
                    path = path + ".txt";
                    try
                    {
                        Class1.WriteToFile(path, A, N);
                    }
                    catch (IOException)
                    {
                        Console.WriteLine("Ошибка в именовании файла!");
                    }
                }
                // Желает ли пользователь продолжить.
                str = Class1.WhatYouDesire();

                // Если ввели S, то STOP.
                if (str == "S")
                {
                    break;
                }
            }
        }
    }
}