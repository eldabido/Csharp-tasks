namespace WorkWithCsvFile
{
    public static class Menu
    {
        /// <summary>
        /// Перечисление Choose - для выбора, какие N данных выводить.
        /// </summary>
        public enum Choose
        {
            top = 1,
            bottom
        }
        public static void MenuOfProg(School[] DataFromCSV)
        {
            while (true)
            {
                OtherMethods.ChooseOption();
                string? option = Console.ReadLine();
                School[] Data = null!;
                if (option == "1")
                {
                    Console.WriteLine($"Введите N (всего {DataFromCSV.Length} записей)");
                    bool res = int.TryParse(Console.ReadLine(), out int N);
                    // Проверка корректности ввода.
                    if (!res || N < 0 || N > DataFromCSV.Length)
                    {
                        Console.WriteLine("Вы ввели что-то плохое!!!");
                    }
                    else
                    {
                        Console.WriteLine("Теперь, если хотите первые N записей, то введите 1, " +
                            "а если последние N, то 2 (если передумали, то введите что-то другое)");
                        res = int.TryParse(Console.ReadLine(), out int k);
                        if (!res || (k != 1 && k != 2))
                        {
                            Console.WriteLine("Вы ввели что-то плохое!!!");
                        }
                        else if (Choose.top == (Choose)k)
                        {
                            Data = SortAndFilter.PrintFirstN(DataFromCSV, N);
                        }
                        else
                        {
                            Data = SortAndFilter.PrintLastN(DataFromCSV, N);
                        }
                    }

                }
                else if (option == "2")
                {
                    Console.WriteLine("Как вы хотите сортировать поле okrug?");
                    Console.WriteLine("1. По возрастанию");
                    Console.WriteLine("2. По убыванию");
                    string sort = Console.ReadLine()!;
                    if (sort == "1" || sort == "2")
                    {
                        Data = SortAndFilter.SortByOkrug(DataFromCSV, sort);
                    }
                    else
                    {
                        Console.WriteLine("Вы ввели что-то не то.");
                    }
                }

                else if (option == "3")
                {
                    Console.WriteLine("Введите значение, по которому будет фильтрация");
                    string ans = "\"" + Console.ReadLine() + "\"";
                    Data = SortAndFilter.FilterForm(DataFromCSV, ans);
                }
                else if (option == "4")
                {
                    Console.WriteLine("Введите значение, по которому будет фильтрация");
                    string ans = "\"" + Console.ReadLine() + "\"";
                    Data = SortAndFilter.FilterSubmission(DataFromCSV, ans);
                }
                else if (option == "5")
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Похоже, вы ввели что-то не то.");
                }

                // Если какая-то из функций была выполнена (то есть в Data что-то попало), то записываем.
                if (Data != null)
                {
                    Console.WriteLine("Как вы хотите сохранить данные? (Введите циферку)");
                    Console.WriteLine("1. Сохранить в новый файл");
                    Console.WriteLine("2. Заменить содержимое уже существующего файла");
                    Console.WriteLine("3. Добавить сохраняемые данные к содержимому существующего файла");
                    option = Console.ReadLine();
                    while (true)
                    {
                        if (option == "1" || option == "2" || option == "3")
                        {
                            Console.WriteLine("Введите название файла");
                            string? path = Console.ReadLine() + ".csv";
                            ReadingAndWriting.Write(Data, path, option!);
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Вы ввели что-то не то. Попробуете еще раз? Введите yes");
                            string? answer = Console.ReadLine();
                            if (answer != "yes")
                            {
                                break;
                            }
                            else
                            {
                                option = Console.ReadLine();
                            }
                        }
                    }
                }
                else
                {
                    Console.WriteLine("В данные ничего не попало, поэтому записывать в файлы нечего!");
                }
            }
        }
    }
}
