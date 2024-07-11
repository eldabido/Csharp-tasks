using System;


namespace Kaspari_19_KDZ_1_2
{
    internal class Class1
    {
        /// <summary>
        /// Метод CheckFormat - проверяет правильность формата файла.
        /// </summary>
        public static bool CheckFormat(string filename)
        {
            using (StreamReader sr = File.OpenText(filename))
            {
                // Получаем первую строку, где указаны размерности.
                string[] size = sr.ReadLine().Split(' ');
                // Проверка этой строки методом CheckSize.
                bool result = Class1.CheckSize(size);
                if (!result)
                {
                    return false;

                }
                else
                {
                    // После проверки первой строки смотрим на правильность строк со значениями.
                    int N = int.Parse(size[0]);
                    // Цикл для проверки всех строк.
                    for (int i = 0; i < N; i++)
                    {
                        string[] values = sr.ReadLine().Split('*');
                        // Метод CheckValues осуществляет проверку.
                        result = Class1.CheckValues(values, N);
                        if (!result)
                        {
                            return false;
                        }
                        
                    }
                    return true;
                }
            }
        }
        /// <summary>
        /// Метод CheckValues - проверяет строки со значениями на корректность.
        /// </summary>
        public static bool CheckValues(string[] values, int N)
        {
            // Так как на конце в строке со значениями две звезды, то у массива строк values
            // Будет в конце два элемента, являющихся пустыми строками.
            // Учитывая это, сначала проверяем длину нашего массива.
            if (values.Length == N + 2)
            {
                if (values[N] != "" || values[N + 1] != "")
                {
                    return false;
                }
                else
                {
                    // Теперь проверяем сами значения - являются ли они вещественными.
                    for (int i = 0; i < N; i++)
                    {
                        bool result = double.TryParse(values[i], out double value);
                        if (!result)
                        {
                            return false;
                        }
                        // Проверка на 2 знака после запятой.
                        int j = 0;
                        // Доходим до запятой.
                        while (values[i][j] != ',')
                        {
                            j++;
                        }
                        int count = 0;
                        // Если кол-во знаков не равно двум, то формат неверен.
                        if (values[i].Length - j - 1 != 2)
                        {
                            return false;
                        }

                    }
                    return true;
                }
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// Метод CheckSize - проверяет строку с размерностями на корректность.
        /// </summary>
        public static bool CheckSize(string[] sizes)
        {
            // Так как массив двумерный, то указано должно быть два числа в строке.
            if (sizes.Length == 2)
            {
                // Проверка этих двух значений на корректность.
                bool result = int.TryParse(sizes[0], out int n1);
                if (!result)
                {
                    return false;
                }
                else
                {
                    // По условию, 1 <= N <= 17.
                    if (n1 < 1 || n1 > 17)
                    {
                        return false;
                    }
                    result = int.TryParse(sizes[1], out int n2);
                    if (!result)
                    {
                        return false;
                    }
                    else
                    {
                        // Здесь еще учитываем, что массив NxN, поэтому значения должны быть равны.
                        if (n2 < 1 || n2 > 17 || n1 != n2)
                        {
                            return false;
                        }
                        return true;
                    }
                }
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        ///  Метод PutValuesToArray - переправляет значения из файла в двумерный массив, возвращает размерность.
        /// </summary>
        public static int PutValuesToArray(out double[,] B, string path)
        {
            using (StreamReader sr = File.OpenText(path))
            {
                // Получаем размерности нашей матрицы. Они разделены пробелом и находятся на первой строке.
                string[] values = sr.ReadLine().Split(" ");
                int N = int.Parse(values[0]);
                B = new double[N, N];
                // Получаем сами значения.
                for (int i = 0; i < N; i++)
                {
                    values = sr.ReadLine().Split("*");
                    for (int j = 0; j < N; j++)
                    {
                        B[i, j] = double.Parse(values[j]);
                    }
                }
                return N;
            }
        }

        /// <summary>
        /// Преобразует массив, удаляя из него диагональные элементы со сдвигом столбцов вверх и записывая результат в новый.
        /// </summary>

        public static double[,] DeleteDiagElem(double[,] B, double[,] New_B)
        {
            // Так как в задании размерность не передается, то находим ее в функции (она NxN, делим на N => получаем порядок матрицы).
            int N = B.Length / (int)Math.Sqrt(B.Length);
            // Отдельно обработаем массив 1x1. Если мы его удаляем, то, по условию, должно вернуться null.
            // Потому что мы удалили элемент, и , по сути, ничего не должно быть.
            if (N == 1)
            {
                return null;
            }
            else
            {
                for (int i = 0; i < N; i++)
                {
                    for (int j = 0; j < N; j++)
                    {
                        // Если элемент на диагонали, то удаляем его и производим сдвиг вверх.
                        if (i == j)
                        {
                            for (int k = i + 1; k < N; k++)
                            {
                                // Сдвиг вверх означает, что минусуем номер строки на 1.
                                New_B[k - 1, j] = B[k, j];
                            }
                            // Дополняем элемент последней строки нулем.
                            New_B[N - 1, j] = 0;
                        }
                        // Если элементы выше главной диагонали, то с ними ничего не происходит, просто передаем их в новую структру.
                        else if (j > i)
                        {
                            New_B[i, j] = B[i, j];
                        }
                    }
                }
                return New_B;
            }
        }

        /// <summary>
        /// Меняет путь, чтобы в конце была папка с решением.
        /// </summary>
        public static string[] GetPath(string[] paths)
        {
            int i = 0;
            // Пока не встретим название папки с решением, идем дальше.
            while (paths[i] != "Kaspari_19_KDZ_1")
            {
                i++;
            }
            // Срезом возвращаем нужный путь.
            return paths[..i];
        }

        /// <summary>
        /// Метод GetFullPath - получает путь к файлу из первого проекта.
        /// </summary>
        public static string GetFullPath(string path)
        {
            // Получаем текущую директорию.
            string a = Environment.CurrentDirectory;
            string[] paths = a.Split("\\");
            // Уходим к пути, где в конце название решения.
            paths = Class1.GetPath(paths);
            string fullPath = "";
            // Вручную добавляем путь к нашему файлу.
            foreach (string val in paths)
            {
                fullPath += val + "\\";
            }
            fullPath += "\\Kaspari_19_KDZ_1\\Kaspari_19_KDZ_1\\bin\\Debug\\net6.0\\" + path;
            return fullPath;
        }

        /// <summary>
        ///  Метод PrintArray - печатает двумерный массив вещественных чисел.
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
    }
}
