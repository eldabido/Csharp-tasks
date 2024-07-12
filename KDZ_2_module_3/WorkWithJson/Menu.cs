namespace WorkWithJson
{
    /// <summary>
    /// Класс Menu - статический класс, содержащий меню программы.
    /// </summary>
    public static class Menu
    {
        /// <summary>
        /// Метод MenuOfProg - метод, реализующий меню программы.
        /// </summary>         
        public static void MenuOfProg(Hero[] heroes)
        {
            while (true)
            {
                // Вывод возможностей программы.
                OtherMethods.ChooseOption1();
                string? option = Console.ReadLine();
                if (option != "1" && option != "2" && option != "3")
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Вы ввели что-то не то.");
                    Console.ResetColor();
                }
                else
                {
                    // Выход из программы.
                    if (option == "1")
                    {
                        break;
                    }
                    // Сортировка.
                    else if (option == "2")
                    {
                        Console.WriteLine("Выберите поле, по которому хотите отсортировать данные");
                        OtherMethods.ChooseOption2();
                        option = Console.ReadLine();
                        if (option != "1" && option != "2" && option != "3" && option != "4"
                            && option != "5" && option != "6")
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Вы ввели что-то не то.");
                            Console.ResetColor();
                        }
                        else
                        {
                            // Вызов метода для сортировки.
                            heroes = SortingAndChanging.ToSort(heroes, option!);
                        }
                    }
                    // Изменение значение поля.
                    else if (option == "3")
                    {
                        Console.WriteLine("Введите hero_id объекта, в котором хотите изменить поле. " +
                            "(Если вы введете несуществующего героя, то ничего не изменится)");
                        string? id = Console.ReadLine();
                        // Вызов метода для изменения поля.
                        heroes = SortingAndChanging.ToChange(id!, heroes);
                    }

                    Console.WriteLine("Хотите ли вы сохранить изменения в отдельном файле? Если да, то нажмите Enter");
                    Console.WriteLine("(Иначе что-то другое)");
                    option = Console.ReadLine();
                    if (option == "")
                    {
                        // Сохранение в файл.
                        Console.WriteLine("Введите имя для файла");
                        string? fName = Console.ReadLine();
                        ReadingAndWritingJson.WriteJson(heroes, fName + ".json");
                    }
                }
            }
        }
    }
}
