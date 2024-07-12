namespace WorkWithWords;

class Program
{
    static void Main()
    {
        // Цикл для повтора решений.
        while (true)
        {
            Console.WriteLine("Введите название файла, в котором находятся данные");
            Console.WriteLine("(В задании написано, что файлы размещаются обязательно рядом " +
                "с исполняемым файлом консольного приложения (.EXE)).");
            string? fPath = Console.ReadLine() + ".txt";

            // Считывание файла.
            CharArr2D[] A;
            int res = WorkWithFile.Read(out A, fPath);
            if (res == 0)
            {
                if (WorkWithFile.WhatYouDesire() == "STOP")
                {
                    break;
                }
            }
            else
            {
                // Получение массива B из A.
                CharArr2D[] B = WorkWithFile.OnlyVow(A);

                // Печать массива B в консоль.
                for (int i = 0; i < B.Length; i++)
                {
                    B[i].Print();
                }

                // Запись в файл.
                Console.WriteLine("Данные успешно выведены и получены." +
                    " Введите название файла, куда их сохранить.");
                fPath = Console.ReadLine() + ".txt";
                WorkWithFile.Write(B, fPath);
                if (WorkWithFile.WhatYouDesire() == "STOP")
                {
                    break;
                }
            }
        }
    }
}
