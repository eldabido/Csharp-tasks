namespace WorkWithCsvFile
{
    public class ReadingAndWriting
    {
        // Строка для названий колонок.
        const string s_Fields = "ROWNUM;name;adress;okrug;rayon;form_of_incorporation;" +
            "submission;tip_uchrezhdeniya;vid_uchrezhdeniya;" +
            "telephone;web_site;e_mail;X;Y;global_id;";

        /// <summary>
        /// Метод Read - считывает данные с файла.
        /// </summary>
        public static string[] Read(string fPath)
        {
            // rowData хранит те самые данные.
            string[]? rowData = null;
            try
            {
                // Проверка на существование и формат файла.
                if (File.Exists(fPath) && Path.GetExtension(fPath) == ".csv" && CheckFormat(fPath))
                {
                    rowData = File.ReadAllLines(fPath);
                }
                else
                {
                    // Если файл отсутствует или его структура не соответствует варианту, то выбрасываем исключение.
                    throw new ArgumentException("fPath");
                }
            }
            catch (ArgumentException)
            {
                Console.WriteLine("Message: Исключение ArgumentException (Файл отсутствует" +
                    " или его структура не соответствует варианту)");
            }
            return rowData!;
        }

        /// <summary>
        /// Метод CheckFormat - проверяет формат данных в файле.
        /// </summary>
        public static bool CheckFormat(string fPath)
        {
            using (StreamReader sr = new StreamReader(fPath))
            {
                // Проверяем первую строку - названия колонок.
                if (sr.ReadLine() != s_Fields)
                {
                    return false;
                }
                // Теперь считываем данные и проверяем, чтобы количество характеристик соответствовало колонкам.
                // Если это так у всех строк, то возвращаем true, иначе - false.
                string? line;
                while ((line = sr.ReadLine()) != null)
                {
                    int count = 1;
                    int i = 0;
                    while (i < line.Length - 1)
                    {
                        while (line[i] != '\"' || line[i + 1] != ';')
                        {
                            i++;
                        }
                        i++;
                        count++;
                        
                    }
                    if (count != s_Fields.Split(";").Length - 1)
                    {
                        return false;
                    }
                }
                return true;
            }
        }

        /// <summary>
        /// Метод Write - записывает данные в файл.
        /// </summary>
        public static void Write(School[] Data, string fPath, string choice)
        {
            // Когда нужно создать новый файл.
            try
            {
                if (choice == "1")
                {
                    using (StreamWriter sw = new StreamWriter(fPath))
                    {
                        // Запись названий колонок.
                        sw.WriteLine(s_Fields);
                        // Запись данных.
                        for (int i = 0; i < Data.Length; i++)
                        {
                            sw.WriteLine(Data[i].GetStringForFile());
                        }
                        Console.WriteLine("Данные успешно добавлены в файл.");
                    }
                }
                // Когда перезаписать в существующий.
                else if (choice == "2")
                {
                    if (File.Exists(fPath))
                    {
                        using (StreamWriter sw = new StreamWriter(fPath))
                        {
                            // Запись названий колонок.
                            sw.WriteLine(s_Fields);
                            // Запись данных.
                            for (int i = 0; i < Data.Length; i++)
                            {
                                sw.WriteLine(Data[i].GetStringForFile());
                            }
                            Console.WriteLine("Данные успешно добавлены в файл.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Файла не существует!");
                    }
                }
                // Когда добавить данные в другой файл.
                else if (choice == "3")
                {
                    // Проверка на формат и существование.
                    if (File.Exists(fPath) && CheckFormat(fPath))
                    {
                        using (StreamWriter sw = new StreamWriter(fPath, true))
                        {
                            // Запись данных.
                            for (int i = 0; i < Data.Length; i++)
                            {
                                sw.WriteLine(Data[i].GetStringForFile());
                            }
                            Console.WriteLine("Данные успешно добавлены в файл.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Файла не существует или у него плохой формат!");
                    }
                }
            }
            catch
            {
                Console.WriteLine("Ошибка! Плохое название или содержимое файла!");
            }
        }

        /// <summary>
        /// Метод Print - выводит все данные об объектах массива с элементами типа School.
        /// </summary>
        public static void Print(School[] Data)
        {
            foreach (School s in Data)
            {
                Console.WriteLine(s.ToString());
                Console.WriteLine("-----------------------------------");
            }
        }
    }
}