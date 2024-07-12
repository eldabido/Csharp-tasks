namespace WorkWithCsvFile;
class Program
{
    static void Main()
    {
        // Цикл повтора решений.
        while (true)
        {
            Console.WriteLine("Здравствуйте!");
            Console.WriteLine("Введите абсолютный путь к файлу .csv");
            string? fPath = Console.ReadLine();
            // Считывание данных.
            string[] DataFromCsv = ReadingAndWriting.Read(fPath!);
            // Если не считалось, то произошла ошибка.
            if (DataFromCsv == null)
            {
                if (OtherMethods.WhatYouDesire() == "STOP")
                {
                    break;
                }
            }
            else
            {
                // Теперь представим наши данные в соответсвии с заданием.
                // Создаем массив с объектами типа School.
                School[] SchoolData = new School[DataFromCsv.Length - 1];
                // Инициализируем объекты.
                for (int i = 0; i < DataFromCsv.Length - 1; i++)
                {
                    SchoolData[i] = new School(DataFromCsv[i + 1]);
                }
                // Переход в меню.
                Menu.MenuOfProg(SchoolData);
                Console.WriteLine("Вы вышли из меню.");
                // Вопрос, желает ли пользователь завершить работу.
                if (OtherMethods.WhatYouDesire() == "STOP")
                {
                    break;
                }

            }
        }
    }
}