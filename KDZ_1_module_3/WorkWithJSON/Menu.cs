namespace WorkWithJSON
{
    /// <summary>
    /// Класс Menu - содержит меню программы.
    /// </summary>
    public class Menu
    {
        /// <summary>
        /// Метод MenuOfProg - метод, реализующий меню программы.
        /// </summary>
        public static void MenuOfProg(Event[] Events)
        {
            while (true)
            {
                OtherMethods.ChooseOption1();
                string? option = Console.ReadLine();
                // В New_events будут лежать полученные данные.
                Event[] New_events = null!;
                if (option == "1" || option == "2")
                {
                    // Фильтрация.
                    if (option == "1")
                    {
                        OtherMethods.ChooseOption2();
                        option = Console.ReadLine();
                        if (option == "1" || option == "2" || option == "3" || option == "4"
                            || option == "5" || option == "6" || option == "7")
                        {
                            Console.WriteLine("Введите значение, по которому будет фильтрация");
                            Console.WriteLine("Если вводите id или attendees, то просто число вводите.");
                            Console.WriteLine("В случае speakers вводите имена и фамилии выступающих" +
                                "через запятую (Пример (порядок важен): Lionel Messi,Vladimir Putin," +
                                "... и т. д.)");
                            Console.WriteLine("Если вводите остальное, то вводите в кавычках (и внимательно!).");
                            string? field = Console.ReadLine();
                            // Вызов фильтрации по полю field.
                            New_events = SortAndFilter.ToFilter(Events, field!, option);
                        }
                        else
                        {
                            Console.WriteLine("Вы ввели что-то не то!");
                        }
                    }
                    // Сортировка.
                    else if (option == "2")
                    {
                        OtherMethods.ChooseOption2();
                        option = Console.ReadLine();
                        if (option == "1" || option == "2" || option == "3" || option == "4"
                            || option == "5" || option == "6" || option == "7")
                        {
                            // Вызов сортировки по полю под номером option.
                            New_events = SortAndFilter.ToSort(Events, option!);
                        }
                        else
                        {
                            Console.WriteLine("Вы ввели что-то не то!");
                        }
                    }
                    // Если никакие данные не подошли, то выводить и нечего.
                    if (New_events != null)
                    {
                        Console.WriteLine("Хотите ли вы сохранить данные в файл?");
                        Console.WriteLine("Нажмите Enter, если да");
                        option = Console.ReadLine();
                        if (option == "")
                        {
                            Console.WriteLine("Как сохранить?");
                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.WriteLine("1. Вывести в System.Console (введите 1)");
                            Console.WriteLine("2. В другой файл (введите 2)");
                            Console.ResetColor();
                            string? ans = Console.ReadLine();
                            if (ans == "1")
                            {
                                Console.WriteLine("Введите имя файла, в который хотите перенаправить поток (без .json)");
                                Console.WriteLine("(Можно нажать Enter, тогда введётся файл из задания).");
                                string? answer = Console.ReadLine();
                                if (answer == "")
                                    JsonParser.WriteJson(New_events, ans!, "data_10V.json");
                                else
                                    JsonParser.WriteJson(New_events, ans!, answer! + ".json");
                            }
                            else if (ans == "2")
                            {
                                Console.ForegroundColor = ConsoleColor.Blue;
                                Console.WriteLine("3. Создать новый или заменить содержимое в существующем");
                                Console.WriteLine("4. Добавить в существующий");
                                Console.ResetColor();
                                option = Console.ReadLine();
                                if (option == "3" || option == "4")
                                {
                                    Console.WriteLine("Введите название файла");
                                    string? fPath = Console.ReadLine();
                                    JsonParser.WriteJson(New_events, option!, fPath!);
                                }
                                else
                                {
                                    Console.WriteLine("Вы ввели не то, что нужно");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Вы ввели что-то не то.");
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("Никаких подходящих данных не обнаружено.");
                    }
                }
                // Выход из меню.
                else if (option == "3")
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Вы ввели что-то не то.");
                }
            }
        }
    }
}
