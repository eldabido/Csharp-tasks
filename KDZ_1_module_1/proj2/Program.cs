using Kaspari_19_KDZ_1_2;
using System;

class Program
{
    static void Main()
    {
        // Цикл повтора решений.
        while (true)
        {
            Console.WriteLine("Введите имя файла со значениями массива");
            string path = Console.ReadLine() + ".txt";
            // fullPath для проверки файлов из папки первого проекта.
            string fullPath = Class1.GetFullPath(path);
            // Проверка на существование файла и доступ к нему.
            if (File.Exists(path) || File.Exists(fullPath))
            {
                bool result;
                // Вызов метода CheckFormat для проверки формата файла.
                if (File.Exists(path))
                {
                    result = Class1.CheckFormat(path);
                }
                else
                {
                    result = Class1.CheckFormat(fullPath);
                    path = fullPath;
                }
                if (!result)
                    {
                        Console.WriteLine("Файл не удовлетворяет формату");

                        // Желает ли пользователь продолжить.
                        path = Class1.WhatYouDesire();
                        if (path == "S")
                        {
                            break;
                        }
                    }
                else
                {
                    // Перекидываем данные из файла в массив B методом PutValuesToArray.
                    double[,] B;
                    int N = Class1.PutValuesToArray(out B, path);
                    Console.WriteLine("Массив до изменений");
                    Class1.PrintArray(B, N);
                    Console.WriteLine();

                    // Преобразуем массив согласно заданию, и полученный результат записывается в new_B.
                    double[,] new_B = new double[N, N];
                    new_B = Class1.DeleteDiagElem(B, new_B);
                    Console.WriteLine("Массив после изменений");
                    if (new_B != null)
                    {
                        Class1.PrintArray(new_B, N);
                    }
                    else
                    {
                        // Если после удаления массив пуст и равен null, то ничего не выводим.
                        Console.WriteLine();
                    }

                    // Желает ли пользователь продолжить.
                    path = Class1.WhatYouDesire();
                    if (path == "S")
                    {
                        break;
                    }
                }
            }
            else
            {
                Console.WriteLine("Такого файла не существует, или он вне доступа!");

                // Желает ли пользователь продолжить.
                path = Class1.WhatYouDesire();
                if (path == "S")
                {
                    break;
                }
            }
        }
    }
}