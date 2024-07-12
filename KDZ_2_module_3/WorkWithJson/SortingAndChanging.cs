namespace WorkWithJson
{
    /// <summary>
    /// Класс SortingAndChanging - статический класс, содержащий методы сортировки и изменения полей в массиве данных. 
    /// </summary>
    public static class SortingAndChanging
    {
        /// <summary>
        /// Метод ToSort - метод, занимающийся сортировкой массива героев по определенному полю.
        /// </summary>
        public static Hero[] ToSort(Hero[] heroes, string option) 
        {
            // Сортировка пузырьком по целочисленным полям.
            if (option == "4" || option == "6")
            {
                for (int i = 0; i < heroes.Length; i++)
                {
                    for (int j = 0; j < heroes.Length - 1 - i; j++)
                    {
                        if (option == "4")
                        {
                            if (heroes[j].Level > heroes[j + 1].Level)
                            {
                                Hero tmp = heroes[j];
                                heroes[j] = heroes[j + 1];
                                heroes[j + 1] = tmp;
                            }
                        }
                        if (option == "6")
                        {
                            if (heroes[j].Troops > heroes[j + 1].Troops)
                            {
                                Hero tmp = heroes[j];
                                heroes[j] = heroes[j + 1];
                                heroes[j + 1] = tmp;
                            }
                        }
                    }
                }
            }
            else {
                // Сортировка пузырьком по строковым полям.
                for (int i = 0; i < heroes.Length; i++)
                {
                    for (int j = 0; j < heroes.Length - 1 - i; j++)
                    {
                        if (option == "1")
                        {
                            if (string.Compare(heroes[j].Hero_id, heroes[j + 1].Hero_id) > 0)
                            {
                                Hero tmp = heroes[j];
                                heroes[j] = heroes[j + 1];
                                heroes[j + 1] = tmp;
                            }
                        }
                        else if (option == "2")
                        {
                            if (string.Compare(heroes[j].Hero_name, heroes[j + 1].Hero_name) > 0)
                            {
                                Hero tmp = heroes[j];
                                heroes[j] = heroes[j + 1];
                                heroes[j + 1] = tmp;
                            }
                        }
                        else if (option == "3")
                        {
                            if (string.Compare(heroes[j].Faction, heroes[j + 1].Faction) > 0)
                            {
                                Hero tmp = heroes[j];
                                heroes[j] = heroes[j + 1];
                                heroes[j + 1] = tmp;
                            }
                        }
                        else if (option == "5")
                        {
                            if (string.Compare(heroes[j].Castle_location, heroes[j + 1].Castle_location) > 0)
                            {
                                Hero tmp = heroes[j];
                                heroes[j] = heroes[j + 1];
                                heroes[j + 1] = tmp;
                            }
                        }
                    }
                }
            }
            // Вывод данных.
            for (int i = 0; i < heroes.Length; i++)
            {
                Console.WriteLine(heroes[i].ToJSON());
                Console.WriteLine("-----------------------------------");
            }
            return heroes;
        }

        /// <summary>
        /// Метод ToChange - метод, изменяющий значение поля.
        /// </summary>
        public static Hero[] ToChange(string id, Hero[] heroes)
        {
            // Поиск по id нужного героя. Если не нашелся, то ничего не делаем.
            int num = -1;
            while (true) {
                for (int i = 0; i < heroes.Length; i++)
                {
                    if (heroes[i].Hero_id == id)
                    {
                        num = i;
                        break;
                    }
                }
                if (num == -1)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Нет такого героя.");
                    Console.WriteLine("Вы ввели что-то не то. Чтобы попробовать еще раз, нажмите enter. " +
                        "Иначе - что-то другое");
                    Console.ResetColor();
                    string? des = Console.ReadLine();
                    if (des != "")
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Введите hero_id объекта, в котором хотите изменить поле. " +
                            "(Если вы введете несуществующего героя, то ничего не изменится)");
                        id = Console.ReadLine()!;
                    }
                }
                else
                {
                    break;
                }
            }

            if (num != -1)
            {
                Console.WriteLine("Какое поле вы хотите изменить?");
                // Вывод полей для изменения.
                OtherMethods.ChooseOption3();
                string? ans = Console.ReadLine();
                // При смене значения происходит событие Updated.
                if (ans == "1")
                {
                    Console.WriteLine("Введите новое значение для поля");
                    string? value = Console.ReadLine();
                    heroes[num].Hero_name = value;
                    heroes[num].ToUpdate(heroes);
                }
                else if (ans == "2")
                {
                    Console.WriteLine("Введите новое значение для поля");
                    string? value = Console.ReadLine();
                    heroes[num].Faction = value;
                    heroes[num].ToUpdate(heroes);
                }
                else if (ans == "3")
                {
                    while (true)
                    {
                        // Здесь есть проверка на целое число, если ввели не то, то заново.
                        Console.WriteLine("Введите новое значение для поля (целое неотрицательное число)");
                        bool res = int.TryParse(Console.ReadLine(), out int value);
                        if (res && value >= 0)
                        {
                            heroes[num].Level = value;
                            heroes[num].ToUpdate(heroes);
                            break;
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Вы ввели что-то не то. Чтобы попробовать еще раз, нажмите enter. " +
                                "Иначе - что-то другое");
                            Console.ResetColor();
                            string? des = Console.ReadLine();
                            if (des != "")
                            {
                                break;
                            }
                        }
                    }
                }
                else if (ans == "4")
                {
                    Console.WriteLine("Введите новое значение для поля");
                    string? value = Console.ReadLine();
                    heroes[num].Castle_location = value;
                    heroes[num].ToUpdate(heroes);
                }
                else if (ans == "5")
                {
                    // Так как Units - вложенный массив классов, то в нем другие объекты, спрашиваем, где надо поменять.
                    Console.WriteLine("Введите unit_name, у которого вы хотите поменять поле");
                    string? value = Console.ReadLine();
                    int num_of_unit = -1;
                    for (int i = 0; i < heroes[num].Units.Length; i++)
                    {
                        if (heroes[num].Units[i].Unit_name == value)
                        {
                            num_of_unit = i;
                            break;
                        }
                    }
                    // Если не нашлось такого unit'а.
                    if (num == -1)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Нет такого героя.");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.WriteLine("Теперь выберите поле, которое хотите поменять");
                        OtherMethods.ChooseOption4();
                        string? answer = Console.ReadLine();
                        if (answer == "1")
                        {
                            Console.WriteLine("Введите новое значение для поля");
                            string? new_value = Console.ReadLine();
                            heroes[num].Units[num_of_unit].Unit_name = new_value;
                        }
                        else if (answer == "2")
                        {
                            while (true)
                            {
                                Console.WriteLine("Введите новое значение для поля (целое неотрицательное число)");
                                bool res = int.TryParse(Console.ReadLine(), out int new_value);
                                if (res && new_value >= 0)
                                {
                                    heroes[num].Units[num_of_unit].Quantity = new_value;
                                    heroes[num].ToUpdate(heroes);
                                    break;
                                }
                                else
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("Вы ввели что-то не то. Чтобы попробовать еще раз, нажмите enter. " +
                                        "Иначе - что-то другое");
                                    Console.ResetColor();
                                    string? des = Console.ReadLine();
                                    if (des != "")
                                    {
                                        break;
                                    }
                                }
                            }
                        }
                        else if (answer == "3")
                        {
                            while (true)
                            {
                                Console.WriteLine("Введите новое значение для поля (целое неотрицательное число)");
                                bool res = int.TryParse(Console.ReadLine(), out int new_value);
                                if (res && new_value >= 0)
                                {
                                    heroes[num].Units[num_of_unit].Experience = new_value;
                                    heroes[num].ToUpdate(heroes);
                                    break;
                                }
                                else
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("Вы ввели что-то не то. Чтобы попробовать еще раз, нажмите enter. " +
                                        "Иначе - что-то другое");
                                    Console.ResetColor();
                                    string? des = Console.ReadLine();
                                    if (des != "")
                                    {
                                        break;
                                    }
                                }
                            }
                        }
                        // Если изменилась локация войска, то происходит событие, в котором пересчитывается параметр troops.
                        else if (answer == "4")
                        {
                            Console.WriteLine("Введите новое значение для поля");
                            string? new_value = Console.ReadLine();
                            heroes[num].Units[num_of_unit].Current_location = new_value;
                            heroes = heroes[num].ToRecountTroops(heroes, num);
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Вы ввели что-то не то.");
                            Console.ResetColor();
                        }
                    }
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Вы ввели что-то не то.");
                    Console.ResetColor();
                }
            }
            return heroes;
        }
    }
}
