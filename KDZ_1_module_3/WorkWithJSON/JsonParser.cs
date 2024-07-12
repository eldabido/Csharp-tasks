namespace WorkWithJSON
{
    /// <summary>
    /// Статический класс JsonParser - класс для ввода и вывода данных формата json.
    /// </summary>
    public static class JsonParser
    {
        // Название файла для перенаправления потока.

        /// <summary>
        /// Метод AppendElem - метод для добавления элемента типа Event в массив.
        /// </summary>
        public static Event[] AppendElem(Event[] events, Event elem)
        {
            // Отдельно случай, когда пустой массив.
            if (events == null)
            {
                events = new Event[1];
                events[0] = elem;
                return events;
            }
            else
            {
                // Создаем новый массив с размером на один больше.
                Event[] New_events = new Event[events.Length + 1];
                // Инициализируем его и возвращаем.
                for (int i = 0; i < events.Length; i++)
                {
                    New_events[i] = events[i];
                }
                New_events[events.Length] = elem;
                return New_events;
            }
        }

        /// <summary>
        /// Метод ReadJson - метод для считывания данных типа json.
        /// </summary>
        public static Event[] ReadJson(string fPath, string ans)
        {
            string s_fName = "data_10V.json";
            try
            {
                Event[] events = null!;
                // Проверка на корректность файла.
                if (File.Exists(fPath) && Path.GetExtension(fPath) == ".json" && OtherMethods.CheckFormat(fPath))
                {
                    // Проверка на то, как пользователь захотел ввести данные.
                    if (ans == "1")
                    {
                        // Считывание через файловый потоковый ввод-вывод.
                        using (FileStream fs = new FileStream(fPath, FileMode.Open))
                        {
                            using (StreamReader sr = new StreamReader(fs))
                            {
                                string line = sr.ReadLine()!;
                                // Пропускаем строку [
                                line = sr.ReadLine()!;
                                while (line != "]")
                                {
                                    // Массив для хранения данных.
                                    string[] data = new string[7];
                                    // ind - индекс для прохода по data.
                                    int ind = 0;
                                    // Проход по блоку данных.
                                    while (line != "  }," && line != "  }")
                                    {
                                        // Пропуск строки {
                                        if (line == "  {")
                                        {
                                            line = sr.ReadLine()!;
                                        }
                                        int i = 0;
                                        // Считываем посимвольно. Идем до двоеточия.
                                        while (line[i++] != ':')
                                        {
                                        }
                                        // Считываем по разному, в зависимости от поля.
                                        if (ind == 0 || ind == 4 || ind == 6)
                                        {
                                            // Тут у нас считывание числовых полей и speakers.
                                            while (line[++i] != ',')
                                            {
                                                if (line[i] != '[')
                                                {
                                                    data[ind] += line[i];
                                                }
                                                else
                                                {
                                                    break;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            // Считывание строковых данных.
                                            i += 1;
                                            data[ind] += "\"";

                                            while (line[++i] != '\"')
                                            {
                                                if (line[i] != '[')
                                                {
                                                    data[ind] += line[i];
                                                }
                                                else
                                                {
                                                    break;
                                                }
                                            }
                                            data[ind] += "\"";
                                        }
                                        // Считывание speakers.
                                        if (line[i] == '[')
                                        {
                                            line = sr.ReadLine()!;
                                            while (line != "    ]")
                                            {
                                                data[ind] += line;
                                                line = sr.ReadLine()!;
                                            }
                                        }
                                        ind++;
                                        line = sr.ReadLine()!;
                                    }
                                    // Создаем экземпляр Event и добавляем его в массив.
                                    Event ev = new Event(data);
                                    events = AppendElem(events, ev);
                                    line = sr.ReadLine()!;
                                }
                            }
                        }
                    }
                    else
                    {
                        if (ans == "3")
                        {
                            s_fName = fPath;
                        }
                        // Перенаправление потока в файл.
                        TextReader consoleInput = Console.In;
                        using (StreamReader sr = new StreamReader(fPath))
                        {
                            Console.SetIn(sr);
                            string line = Console.ReadLine()!;
                            // Пропускаем строку [
                            line = Console.ReadLine()!;
                            while (line != "]")
                            {
                                // Здесь все аналогично, как в блоке if.
                                string[] data = new string[7];
                                int ind = 0;
                                while (line != "  }," && line != "  }")
                                {
                                    // Пропуск строки {
                                    if (line == "  {")
                                    {
                                        line = Console.ReadLine()!;
                                    }
                                    int i = 0;
                                    while (line[i++] != ':')
                                    {
                                    }
                                    if (ind == 0 || ind == 4 || ind == 6)
                                    {
                                        while (line[++i] != ',')
                                        {
                                            if (line[i] != '[')
                                            {
                                                data[ind] += line[i];
                                            }
                                            else
                                            {
                                                break;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        i += 1;
                                        data[ind] += "\"";
                                        while (line[++i] != '\"')
                                        {
                                            if (line[i] != '[')
                                            {
                                                data[ind] += line[i];
                                            }
                                            else
                                            {
                                                break;
                                            }
                                        }
                                        data[ind] += "\"";
                                    }
                                    if (line[i] == '[')
                                    {
                                        line = Console.ReadLine()!;
                                        while (line != "    ]")
                                        {
                                            data[ind] += line;
                                            line = Console.ReadLine()!;
                                        }
                                    }
                                    ind++;
                                    line = Console.ReadLine()!;
                                }
                                Event ev = new Event(data);
                                events = AppendElem(events, ev);
                                line = Console.ReadLine()!;
                            }
                            // Перенаправляем поток обратно.
                            Console.SetIn(consoleInput);
                        }
                    }
                }
                else
                {
                    // Если у файла что-то не так, то бросаем исключение.
                    throw new FileNotFoundException();
                }
                return events;
            }
            catch (FileNotFoundException)
            {
                // Обработка исключения.
                Console.WriteLine("Исключение: FileNotFoundException");
                Console.WriteLine("Нет такого файла или он имеет недопустимый формат!");
                return null!;
            }
        }

        /// <summary>
        /// Метод WriteJson - метод для вывода данных формата json.
        /// </summary>
        public static void WriteJson(Event[] events, string choice, string fPath) 
        {
            try
            {
                if (choice == "1")
                {
                    TextWriter consoleOutput = Console.Out;
                    using (StreamWriter sr = new StreamWriter(fPath))
                    {
                        Console.SetOut(sr);
                        Console.WriteLine("[");
                        // Записываем все данные по очереди.
                        for (int i = 0; i < events.Length; i++)
                        {
                            Console.WriteLine("  {");
                            Console.WriteLine($"    \"event_id\": {events[i]._event_id},");
                            Console.WriteLine($"    \"event_name\": {events[i]._event_name},");
                            Console.WriteLine($"    \"location\": {events[i]._location},");
                            Console.WriteLine($"    \"date\": {events[i]._date},");
                            Console.WriteLine($"    \"attendees\": {events[i]._attendees},");
                            Console.WriteLine($"    \"organizer\": {events[i]._organizer},");
                            Console.WriteLine($"    \"speakers\": [");
                            for (int j = 0; j < events[i]._speakers!.Length; j++)
                            {
                                if (j != events[i]._speakers!.Length - 1)
                                    Console.WriteLine($"      \"{events[i]._speakers![j]}\",");
                                else
                                {
                                    Console.WriteLine($"      \"{events[i]._speakers![j]}\"");
                                }
                            }
                            // Закрытие массива.
                            Console.WriteLine($"    ]");
                            // Если последний блок, то надо без запятой.
                            if (i != events.Length - 1)
                            {
                                Console.WriteLine("  },");
                            }
                            else
                            {
                                Console.WriteLine("  }");
                            }
                        }
                        // "Закрытие" записи.
                        Console.WriteLine("]");
                        Console.SetOut(consoleOutput);
                    }
                }
                // Если выбрано создать новый файл или перезаписать в существующий.
                else if (choice == "3")
                {
                    using (StreamWriter sw = new StreamWriter(fPath + ".json"))
                    {
                        // Записываем квадратную скобку в начало (так требует формат).
                        sw.WriteLine("[");
                        // Записываем все данные по очереди.
                        for (int i = 0; i < events.Length; i++)
                        {
                            sw.WriteLine("  {");
                            sw.WriteLine($"    \"event_id\": {events[i]._event_id},");
                            sw.WriteLine($"    \"event_name\": {events[i]._event_name},");
                            sw.WriteLine($"    \"location\": {events[i]._location},");
                            sw.WriteLine($"    \"date\": {events[i]._date},");
                            sw.WriteLine($"    \"attendees\": {events[i]._attendees},");
                            sw.WriteLine($"    \"organizer\": {events[i]._organizer},");
                            sw.WriteLine($"    \"speakers\": [");
                            for (int j = 0; j < events[i]._speakers!.Length; j++)
                            {
                                if (j != events[i]._speakers!.Length - 1)
                                    sw.WriteLine($"      \"{events[i]._speakers![j]}\",");
                                else
                                {
                                    sw.WriteLine($"      \"{events[i]._speakers![j]}\"");
                                }
                            }
                            // Закрытие массива.
                            sw.WriteLine($"    ]");
                            // Если последний блок, то надо без запятой.
                            if (i != events.Length - 1)
                            {
                                sw.WriteLine("  },");
                            }
                            else
                            {
                                sw.WriteLine("  }");
                            }
                        }
                        // "Закрытие" записи.
                        sw.WriteLine("]");

                    }
                }
                // Если выбрано добавить новые данные.
                else if (choice == "4")
                {
                    // Считываем данные с файла.
                    if (!File.Exists(fPath + ".json"))
                    {
                        throw new FileNotFoundException("Файл не найден");
                    }
                    string[] lines = File.ReadAllLines(fPath + ".json");
                    // Меняем последние две строки на нужные.
                    lines[lines.Length - 2] = "  },";
                    lines[lines.Length - 1] = "  {";
                    File.WriteAllLines(fPath + ".json", lines);
                    // Далее всё аналогично прошлому if'у.
                    using (StreamWriter sw = new StreamWriter(fPath + ".json", true))
                    {
                        for (int i = 0; i < events.Length; i++)
                        {
                            if (i != 0)
                            {
                                sw.WriteLine("  {");
                            }
                            sw.WriteLine($"    \"event_id\": {events[i]._event_id},");
                            sw.WriteLine($"    \"event_name\": {events[i]._event_name},");
                            sw.WriteLine($"    \"location\": {events[i]._location},");
                            sw.WriteLine($"    \"date\": {events[i]._date},");
                            sw.WriteLine($"    \"attendees\": {events[i]._attendees},");
                            sw.WriteLine($"    \"organizer\": {events[i]._organizer},");
                            sw.WriteLine($"    \"speakers\": [");
                            for (int j = 0; j < events[i]._speakers!.Length; j++)
                            {
                                if (j != events[i]._speakers!.Length - 1)
                                    sw.WriteLine($"      \"{events[i]._speakers![j]}\",");
                                else
                                {
                                    sw.WriteLine($"      \"{events[i]._speakers![j]}\"");
                                }
                            }
                            sw.WriteLine($"    ]");
                            if (i != events.Length - 1)
                            {
                                sw.WriteLine("  },");
                            }
                            else
                            {
                                sw.WriteLine("  }");
                            }
                        }
                        sw.WriteLine("]");
                    }
                }
                Console.WriteLine("Запись завершена");
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Возможно, вы не так ввели название");
            }
            catch
            {
                Console.WriteLine("Что-то пошло не так. Наверное, плохое название файла");
            }
        }

    }
}
