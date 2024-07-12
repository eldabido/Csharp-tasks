using WorkWithCsvLibrary;
class Program
{
    static void Main()
    {
        Console.WriteLine("Здравствуйте!");
        while (true)
        {
            Console.WriteLine("Введите абсолютный путь к файлу формата csv");
            string? fPath = Console.ReadLine();
            // Присваиваем значение статическому полю fPath.
            CsvProcessing.fPathProp = fPath;
            // Считвыание данных файла методом Read().
            string[] DataFromCSV = CsvProcessing.Read();
            // Проверка на неудачное чтение.
            if (DataFromCSV == null)
            {
                if (OtherMethods.WhatYouDesire() == "STOP")
                {
                    break;
                }
            }
            else
            {
                // Если все хорошо, то переходим в меню программы.
                int result = Menu.MenuOfProg(DataFromCSV);
                // Метод вернет 0, если пользователь захотел выйти из программы.
                if (result == 0)
                {
                    break;
                }
                if (OtherMethods.WhatYouDesire() == "STOP")
                {
                    break;
                }
            }
        }
    }
}