namespace WorkWithJSON
{
    /// <summary>
    /// Класс SortAndFilter - содержит методы фильтрации и сортировки.
    /// </summary>
    public class SortAndFilter
    {
        /// <summary>
        /// Метод GetArrayOfNames - метод, получающий имена из speakers.
        /// </summary>
        public static string[] GetArrayOfNames(string field)
        {
            int ind = 0;
            int i = 0;
            // С запасом создаём массив с именами. 
            string[] names = new string[field.Length];
            while (i < field.Length)
            {
                // Считываем до запятой.
                while (i < field.Length && field[i] != ',')
                {
                    names[ind] += field[i++];
                }
                i++;
                ind++;
            }
            // Возвращаем с помощью среза.
            return names[..ind];
        }

        /// <summary>
        /// Метод IsEqual - метод, проверяющий на равенство два массива string'ов.
        /// </summary>
        public static bool IsEqual(string[] a, string[] b)
        {
            // Проверка на равенство количества элементов.
            if (a.Length == b.Length)
            {
                // Сравнение элементов.
                for (int i = 0; i < a.Length; i++)
                {
                    if (a[i] != b[i])
                    {
                        return false;
                    }
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Метод ToFilter - метод, фильтрующий данные по значению поля.
        /// </summary>
        public static Event[] ToFilter(Event[] events, string field, string option)
        {
            // Создаем массив для итога.
            try
            {
                // Массив, в котором будет итог.
                Event[] Filtered = new Event[events.Length];
                // count для счета размера массива.
                int count = 0;
                // Сохраняем в зависимости от поля.
                if (option == "1")
                {
                    foreach (Event elem in events)
                    {
                        if (elem._event_id == int.Parse(field))
                        {
                            Filtered[count++] = elem;
                            Console.WriteLine(elem.ToString());
                            Console.WriteLine("-----------------------------------");
                        }
                    }
                    return Filtered[..count];
                }
                if (option == "2")
                {
                    foreach (Event elem in events)
                    {
                        if (elem._event_name == field)
                        {
                            Filtered[count++] = elem;
                            Console.WriteLine(elem.ToString());
                            Console.WriteLine("-----------------------------------");
                        }
                    }
                    return Filtered[..count];
                }
                if (option == "3")
                {
                    foreach (Event elem in events)
                    {
                        if (elem._location == field)
                        {
                            Filtered[count++] = elem;
                            Console.WriteLine(elem.ToString());
                            Console.WriteLine("-----------------------------------");
                        }
                    }
                    return Filtered[..count];
                }
                if (option == "4")
                {
                    foreach (Event elem in events)
                    {
                        if (elem._date == field)
                        {
                            Filtered[count++] = elem;
                            Console.WriteLine(elem.ToString());
                            Console.WriteLine("-----------------------------------");
                        }
                    }
                    return Filtered[..count];
                }
                if (option == "5")
                {
                    foreach (Event elem in events)
                    {
                        if (elem._attendees == int.Parse(field))
                        {
                            Filtered[count++] = elem;
                            Console.WriteLine(elem.ToString());
                            Console.WriteLine("-----------------------------------");
                        }
                    }
                    return Filtered[..count];
                }
                if (option == "6")
                {
                    foreach (Event elem in events)
                    {
                        if (elem._organizer == field)
                        {
                            Filtered[count++] = elem;
                            Console.WriteLine(elem.ToString());
                            Console.WriteLine("-----------------------------------");
                        }
                    }
                    return Filtered[..count];
                }
                if (option == "7")
                {
                    foreach (Event elem in events)
                    {
                        string[] names = GetArrayOfNames(field);
                        if (IsEqual(names, elem._speakers!))
                        {
                            Filtered[count++] = elem;
                            Console.WriteLine(elem.ToString());
                            Console.WriteLine("-----------------------------------");
                        }
                    }
                    return Filtered[..count];
                }
                return null!;
            }
            catch
            {
                Console.WriteLine("Проблема с введенными данными.");
                Console.WriteLine("(Если вы фильтруете по id или attendees, то вам нужно вводить число)");
                return null!;
            }
        }

        /// <summary>
        /// Метод ToSort - метод, сортирующий данные по возрастанию.
        /// </summary>
        public static Event[] ToSort(Event[] events, string option)
        {
            // Сортируем в зависимости от выбранного поля (Сортировка пузырьком).
            // Если option = 1 или 5, то сортируются числа.
            if (option == "1")
            {
                for (int i = 0; i < events.Length; i++)
                {
                    for (int j = 0; j < events.Length - 1 - i; j++)
                    {
                        if (events[j]._event_id > events[j + 1]._event_id)
                        {
                            Event tmp = events[j];
                            events[j] = events[j + 1];
                            events[j + 1] = tmp;
                        }
                    }
                }
            }
            else if (option == "5")
            {
                for (int i = 0; i < events.Length; i++)
                {
                    for (int j = 0; j < events.Length - 1 - i; j++)
                    {
                        if (events[j]._attendees > events[j + 1]._attendees)
                        {
                            Event tmp = events[j];
                            events[j] = events[j + 1];
                            events[j + 1] = tmp;
                        }
                    }
                }
            }
            // Здесь сортируем speakers по количеству спикеров.
            else if (option == "7")
            {
                for (int i = 0; i < events.Length; i++)
                {
                    for (int j = 0; j < events.Length - 1 - i; j++)
                    {
                        if (events[j]._speakers!.Length > events[j + 1]._speakers!.Length)
                        {
                            Event tmp = events[j];
                            events[j] = events[j + 1];
                            events[j + 1] = tmp;
                        }
                    }
                }
            }
            // Сортируем поля типа string.
            else
            {
                for (int i = 0; i < events.Length; i++)
                {
                    for (int j = 0; j < events.Length - 1 - i; j++)
                    {
                        if (option == "2")
                        {
                            if (String.Compare(events[j]._event_name, events[j + 1]._event_name) > 0)
                            {
                                Event tmp = events[j];
                                events[j] = events[j + 1];
                                events[j + 1] = tmp;
                            }
                        }
                        if (option == "3")
                        {
                            if (String.Compare(events[j]._location, events[j + 1]._location) > 0)
                            {
                                Event tmp = events[j];
                                events[j] = events[j + 1];
                                events[j + 1] = tmp;
                            }
                        }
                        if (option == "4")
                        {
                            if (String.Compare(events[j]._date, events[j + 1]._date) > 0)
                            {
                                Event tmp = events[j];
                                events[j] = events[j + 1];
                                events[j + 1] = tmp;
                            }
                        }
                        if (option == "6")
                        {
                            if (String.Compare(events[j]._organizer, events[j + 1]._organizer) > 0)
                            {
                                Event tmp = events[j];
                                events[j] = events[j + 1];
                                events[j + 1] = tmp;
                            }
                        }
                    }
                }
            }
            // Вывод данных.
            for (int i = 0; i < events.Length; i++)
            {
                Console.WriteLine(events[i].ToString());
                Console.WriteLine("-----------------------------------");
            }
            return events;
        }
    }
}
