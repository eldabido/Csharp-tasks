namespace WorkWithCsvFile
{
    public class OtherMethods
    {
        /// <summary>
        /// Метод WhatYouDesire - спрашивает у пользователя, чего он хочет.
        /// </summarry>
        public static string WhatYouDesire()
        {
            Console.WriteLine("Если хотите закончить работу, то введите STOP");
            Console.WriteLine("Если хотите продолжить и заново ввести значение, то что-то другое");
            return Console.ReadLine()!;
        }

        /// <summary>
        /// Метод ChooseOption - вывод в меню того, что можно сделать.
        /// </summary>
        public static void ChooseOption()
        {
            Console.WriteLine("Укажите номер пункта меню для запуска действия:");
            Console.WriteLine("(Если хотите, например, 1-ый пункт, то нужно ввести 1, и т. д.)");
            Console.WriteLine("1. Предоставить для просмотра N первых или последних записей из файла");
            Console.WriteLine("2. Выполнить сортировку данных по полю okrug");
            Console.WriteLine("3. Провести фильтрацию по полю form_of_incorporation");
            Console.WriteLine("4. Провести фильтрацию по полю submission");
            Console.WriteLine("5. Выйти из меню");
        }
    }
}
