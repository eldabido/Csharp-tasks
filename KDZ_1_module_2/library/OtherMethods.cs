namespace WorkWithCsvLibrary
{
    public static class OtherMethods
    {
        // Колонки на английском.
        const string EngFields = "\"ID\";\"Name\";\"Photo\";\"global_id\";\"AdmArea\";\"District\";\"Location\"" +
            ";\"RegistrationNumber\";\"State\";\"LocationType\";\"geodata_center\";\"geoarea\";";
        // Колонки на русском.
        const string RusFields = "\"Локальный идентификатор\";\"Название объекта\";\"Фотография\";\"global_id\";" +
            "\"Административный округ по адресу\";\"Район\";\"Месторасположение\";" +
            "\"Государственный регистрационный знак\";\"Состояние регистрации\";" +
            "\"Тип места расположения\";\"geodata_center\";\"geoarea\";";

        /// <summary>
        /// Метод ChooseOption - выводит меню для пользователя.
        /// </summary>
        public static void ChooseOption()
        {
            Console.WriteLine("Укажите номер пункта меню для запуска действия:");
            Console.WriteLine("(Если хотите, например, 1-ый пункт, то нужно ввести 1, и т. д.)");
            Console.WriteLine("1. Произвести выборку по значению AdmArea");
            Console.WriteLine("2. Произвести выборку по значению geoarea");
            Console.WriteLine("3. Произвести выборку по значению District и Geoarea");
            Console.WriteLine("4. Отсортировать по алфавиту Name в прямом порядке");
            Console.WriteLine("5. Отсортировать по алфавиту Name в обратном порядке");
            Console.WriteLine("6. Выйти из программы");
        }

        /// <summary>
        /// Метод WhatYouDesire - спрашивает пользователся о желании продолжить работу.
        /// </summary>
        public static string WhatYouDesire()
        {
            Console.WriteLine("Если хотите закончить работу, то введите STOP");
            Console.WriteLine("Если хотите продолжить и заново ввести значение, то что-то другое");
            return Console.ReadLine();
        }

        /// <summary>
        /// Метод SaveForSelections - сохраняет полученные выборки в csv файл.
        /// </summary>
        public static void SaveForSelections(string[] data)
        {
            Console.WriteLine("Введите имя CSV файла, в которое необходимо сохранить данные " +
                "(без использования запрещенных символов):");
            string? fPath = Console.ReadLine() + ".csv";

            //  Добавление колонок.
            // Сначала запишем названия колонок.
            Console.WriteLine("Хотите ли вы добавить названия колонок в файл? Тогда введите YES");
            Console.WriteLine("Если нет, то потом нельзя будет использовать этот файл в программе.");
            Console.WriteLine("Так как он не пройдет проверку формата.");
            string? ans = Console.ReadLine();
            if (ans == "YES") 
            {
                data = ChangeArray(data);
            }
            // Если одна строка, то вызываем Write для одной строки.
            if (data.Length == 1)
            {
                CsvProcessing.Write(data[0], fPath);
            }
            else
            {
                // Иначе вызываем Write для массива строк.
                CsvProcessing.Write(data, fPath);
            }
        }

        /// <summary>
        /// Метод SaveForSorted - сохраняет отсортированные данные в csv файл.
        /// </summary>
        public static void SaveForSorted(string[][] data)
        {
            Console.WriteLine("Введите имя CSV файла, в которое необходимо сохранить данные");
            string? fPath = Console.ReadLine() + ".csv";
            // Так как мы передали массив массивов данных, то переделаем его в массив строк данных.
            // Сначала запишем названия колонок.
            Console.WriteLine("Хотите ли вы добавить названия колонок в файл? Тогда введите YES");
            Console.WriteLine("Если нет, то потом нельзя будет использовать этот файл в программе.");
            Console.WriteLine("Так как он не пройдет проверку формата.");
            string? ans = Console.ReadLine();
            string[] StringData;
            if (ans == "YES")
            {
                // Если добавлять колонки, то длина массива увеличивается, поэтому делаем два случая.
                StringData = new string[data.Length + 2];
                StringData[0] = EngFields;
                StringData[1] = RusFields;
                // Запись в массив.
                for (int i = 2; i < data.Length + 2; i++)
                {
                    StringData[i] = "";
                    for (int j = 0; j < data[i-2].Length; j++)
                    {
                        // В конце лишний элемент в связи с особенностями csv файла.
                        if (j != data[i - 2].Length - 1)
                        {
                            StringData[i] += data[i - 2][j] + ";";
                        }
                    }
                }
            }
            else
            {
                // Случай, когда без колонок.
                StringData = new string[data.Length];
                for (int i = 0; i < data.Length; i++)
                {
                    StringData[i] = "";
                    for (int j = 0; j < data[i].Length; j++)
                    {
                        if (j != data[i].Length - 1)
                        {
                            StringData[i] += data[i][j] + ";";
                        }
                    }
                }
            }
            // И теперь вызываем Write отдельно для случая, когда одна строка, и когда много строк.
            if (StringData.Length == 1)
            {
                CsvProcessing.Write(StringData[0], fPath);
            }
            else
            {
                CsvProcessing.Write(StringData, fPath);
            }
        }

        /// <summary>
        /// метод CheckFormat - проверяет, тот ли формат, заданный варинтом, у файла(как у файла attraction).
        /// </summary>
        public static bool CheckFormat(string fPath)
        {
            using (StreamReader sr = File.OpenText(fPath))
            {
                // Проверяем первые две строки - это должны быть названия колонок на русском и английском.
                if (sr.ReadLine() != EngFields)
                {
                    return false;
                }
                if (sr.ReadLine() != RusFields)
                {
                    return false;
                }
                // Теперь считываем данные и проверяем, чтобы количество характеристик соответствовало колонкам.
                // Если это так у всех строк, то возвращаем true, иначе - false.
                string? line;
                while ((line  = sr.ReadLine()) != null) 
                {   // Разбиваем на массив и сравниваем кол-во элементов.
                    if (line.Split(";").Length != EngFields.Split(";").Length) 
                    {
                        return false;
                    }
                }
                return true;
            }
        }
        /// <summary>
        /// Метод ChangeArray - добавляет названия колонок в начало массива.
        /// </summary>
        public static string[] ChangeArray(string[] data)
        {
            string[] NewData = new string[data.Length + 2];
            NewData[0] = EngFields;
            NewData[1] = RusFields;
            for (int i = 2; i < data.Length+2; i++)
            {
                NewData[i] = data[i-2];
            }
            return NewData;
        }

        /// <summary>
        ///  Метод Spaces - для грамотного вывода колонок, передаваемых из data.
        /// </summary>
        public static void Spaces(string[] data)
        {
            // Нужен определённой длины отступ для красивого вывода. 
            Console.Write(data[0] + "             ");
            Console.Write(data[1] + "          ");
            Console.Write(data[2]);

            Console.WriteLine();
        }

        /// <summary>
        /// Метод OutputSorted - выводит отсортированные данные, передаваемые через SortedData.
        /// </summary>
        public static void OutputSorted(string[][] SortedData)
        {
            // Вывод данных.
            // Данные широковаты, сделаем три колонки, а остальные данные укажем горизонтально.
            string[] Eng = EngFields.Split(";");
            // Расстояние для указания первой колонки.
            Console.Write("                                            ");
            // Вывод названий колонок.
            for (int i = Eng.Length - 6; i < Eng.Length - 3; i++)
            {
                Console.Write(Eng[i] + "              ");
            }
            Console.WriteLine();
            // Вывод самих данных.
            foreach (var subArray in SortedData)
            {
                // Вывод тех самых трех колонок. Передаем в Spaces ту часть, где указаны эти данные.
                Console.Write("                                             ");
                OtherMethods.Spaces(subArray[(subArray.Length - 6)..(subArray.Length - 3)]);
                // Вывод горизонтальных данных.
                for (int i = 0; i <= subArray.Length - 7; i++)
                {
                    Console.WriteLine("{0}:{1}", Eng[i], subArray[i]);
                }
                // Geodata-данные тоже хочется вывести горизонтально.
                for (int i = subArray.Length - 3; i < subArray.Length - 1; i++)
                {
                    Console.WriteLine("{0}:{1}", Eng[i], subArray[i]);
                }
                Console.WriteLine("-------------------------------------------------------------------------");
            }
        }

        // Метод OutputColumn - вывод трех выбранных колонок.
        public static void OutputColumn()
        {
            // Вывод данных.
            // Данные широковаты, сделаем три колонки, а остальные данные укажем горизонтально.
            string[] Eng = EngFields.Split(";");
            // Расстояние для указания первой колонки.
            Console.Write("                                            ");
            // Вывод названий колонок.
            for (int i = Eng.Length - 6; i < Eng.Length - 3; i++)
            {
                Console.Write(Eng[i] + "              ");
            }
            Console.WriteLine();
        }

        /// <summary>
        ///  Метод OutputSelections - вывод выборок, передаваемых через data.
        /// </summary>
        public static void OutputSelections(string[] data)
        {
            string[] Eng = EngFields.Split(";");
            // Вывод самих данных.
            // Вывод тех самых трех колонок. Передаем в Spaces ту часть, где указаны эти данные.
            Console.Write("                                             ");
            OtherMethods.Spaces(data[(data.Length - 6)..(data.Length - 3)]);
            // Вывод горизонтальных данных.
            for (int i = 0; i <= data.Length - 7; i++)
            {
                Console.WriteLine("{0}:{1}", Eng[i], data[i]);
            }
            // Geodata-данные тоже хочется вывести горизонтально.
            for (int i = data.Length - 3; i < data.Length - 1; i++)
            {
                Console.WriteLine("{0}:{1}", Eng[i], data[i]);
            }
            Console.WriteLine("-----------------------------------------------------------------------------");
        }
    }
}
