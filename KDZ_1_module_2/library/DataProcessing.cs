using System;

namespace WorkWithCsvLibrary
{
    public static class DataProcessing
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
        /// Метод AdmArea - принимает массив данных data, и делает выборку по колонке AdmArea данных с ReqArea.
        /// </summary>
        public static void AdmArea(string[] data, string ReqArea)
        {
            // Массив названий колнок.
            string[] fields = data[0].Split(";");
            int IndexOfAdmArea = 0;
            // Ищем, какой по очереди является колонка AdmArea.
            while (fields[IndexOfAdmArea] != "\"AdmArea\"" && IndexOfAdmArea != fields.Length)
            {
                IndexOfAdmArea++;
            }
            // Получаем количество строк с ReqArea, чтобы потом пользователь мог сохранить их, если хочет.
            int sizeOfReqArea = 0;
            // Выводим колонки в консоль.
            OtherMethods.OutputColumn();
            // В цикле выводим все подходящие данные (начинаем с i = 2, так как первые две строки - названия колонок).
            for (int i = 2; i < data.Length; i++) 
            {
                // Split'ом делаем массив из данных строки, и по индексу AdmArea проверяем, подходит ли.
                if (data[i].Split(";")[IndexOfAdmArea] == ReqArea)
                {
                    OtherMethods.OutputSelections(data[i].Split(";"));
                    // Не забываем считать их количество.
                    sizeOfReqArea++;
                }
            }
            // Если результат пуст, то оповещаем.
            if (sizeOfReqArea == 0)
            {
                Console.WriteLine("Нет подходящих данных");
            }
            else {
                Console.WriteLine("Поздравляю! Данные вывелись успешно.");
                Console.WriteLine("Хотите ли вы сохранить полученные данные в файле? Если да, то введите YES");
                string? Save = Console.ReadLine();
                if (Save == "YES")
                {
                    // Создаем массив с нужными строками.
                    string[] ReqAreas = new string[sizeOfReqArea];
                    // Индекс для прохода по этому массиву.
                    int indOfReq = 0;
                    for (int i = 2; i < data.Length; i++)
                    {
                        // Сохраняем данные в массив.
                        if (data[i].Split(";")[IndexOfAdmArea] == ReqArea)
                        {
                            ReqAreas[indOfReq] = data[i];
                            indOfReq++;
                        }
                    }
                    // Вызов специального метода для сохранения в csv файл.
                    OtherMethods.SaveForSelections(ReqAreas);
                }
            }
        }

        /// <summary>
        /// Метод geoarea - принимает массив данных data, и делает выборку по колонке geoarea данных с ReqArea.
        /// </summary>
        public static void geoarea(string[] data, string ReqArea)
        {
            // В приницпе, метод работает аналогично AdmArea, только теперь с geoarea.

            // Массив названий колнок.
            string[] fields = data[0].Split(";");
            int IndexOfgeoarea = 0;
            // Ищем, какой по очереди является колонка geoarea.
            while (fields[IndexOfgeoarea] != "\"geoarea\"" && IndexOfgeoarea != fields.Length)
            {
                IndexOfgeoarea++;
            }
            int sizeOfReqArea = 0;
            // Выводим колонки в консоль.
            OtherMethods.OutputColumn();
            // В цикле выводим все подходящие данные (начинаем с i = 2, так как первые две строки - названия колонок).
            for (int i = 2; i < data.Length; i++)
            {
                // Split'ом делаем массив из данных строки, и по индексу AdmArea проверяем, подходит ли.
                if (data[i].Split(";")[IndexOfgeoarea] == ReqArea)
                {
                    OtherMethods.OutputSelections(data[i].Split(";"));
                    // Не забываем считать их количество.
                    sizeOfReqArea++;
                }
            }
            // Если результат пуст, то оповещаем.
            if (sizeOfReqArea == 0)
            {
                Console.WriteLine("Нет подходящих данных");
            }
            else
            {
                Console.WriteLine("Поздравляю! Данные вывелись успешно.");
                Console.WriteLine("Хотите ли вы сохранить полученные данные в файле? Если да, то введите YES");
                string? Save = Console.ReadLine();
                if (Save == "YES")
                {
                    // Массив с нужными строками.
                    string[] ReqAreas = new string[sizeOfReqArea];
                    // Индекс прохода по нему.
                    int indOfReq = 0;
                    for (int i = 2; i < data.Length; i++)
                    {
                        // Если подходит, то записываем.
                        if (data[i].Split(";")[IndexOfgeoarea] == ReqArea)
                        {
                            ReqAreas[indOfReq] = data[i];
                            indOfReq++;
                        }
                    }
                    // Сохранение.
                    OtherMethods.SaveForSelections(ReqAreas);
                }
            }
        }

        /// <summary>
        /// Метод DistrAndGeoAr - принимает данные data, и делает выборку по колонкам geoarea и Distict данных с ReqArea.
        /// </summary>

        public static void DistrAndGeoAr(string[] data, string ReqArea, string ReqDistr)
        {
            // В приницпе, метод работает аналогично AdmArea, только теперь учитывает два поля.
            string[] fields = data[0].Split(";");
            int IndexOfGeoarea = 0;
            // Ищем, какие по счету колонки.
            while (fields[IndexOfGeoarea] != "\"geoarea\"" && IndexOfGeoarea != fields.Length)
            {
                IndexOfGeoarea++;
            }
            int IndexOfDistr = 0;
            while (fields[IndexOfDistr] != "\"District\"" && IndexOfDistr != fields.Length)
            {
                IndexOfDistr++;
            }
            // Выводим подходящие данные.
            int SizeOfReqArea = 0;
            // Выводим колонки в консоль.
            OtherMethods.OutputColumn();
            // В цикле выводим все подходящие данные (начинаем с i = 2, так как первые две строки - названия колонок).
            for (int i = 2; i < data.Length; i++)
            {
                // Split'ом делаем массив из данных строки, и по индексу AdmArea проверяем, подходит ли.
                if (data[i].Split(";")[IndexOfGeoarea] == ReqArea && data[i].Split(";")[IndexOfDistr] == ReqDistr)
                {
                    OtherMethods.OutputSelections(data[i].Split(";"));
                    // Не забываем считать их количество.
                    SizeOfReqArea++;
                }
            }
            // Если результат пуст, то оповещаем.
            if (SizeOfReqArea == 0)
            {
                Console.WriteLine("Нет подходящих данных");
            }
            else 
            {
                Console.WriteLine("Поздравляю! Данные вывелись успешно.");
                Console.WriteLine("Хотите ли вы сохранить полученные данные в файле? Если да, то введите YES");
                string? Save = Console.ReadLine();
                if (Save == "YES")
                {
                    //Массив с нужными строками.
                    string[] ReqAreas = new string[SizeOfReqArea];
                    // Индекс прохода по нему.
                    int indOfReq = 0;
                    for (int i = 2; i < data.Length; i++)
                    {
                        // Если подходит, то записываем.
                        if (data[i].Split(";")[IndexOfGeoarea] == ReqArea && data[i].Split(";")[IndexOfDistr] == ReqDistr)
                        {
                            ReqAreas[indOfReq] = data[i];
                            indOfReq++;
                        }
                    }
                    // Сохранение.
                    OtherMethods.SaveForSelections(ReqAreas);
                }
            }
        }

        /// <summary>
        /// Метод NameDirect - проводит сортировку данных data по колонке Name в прямом порядке.
        /// </summary>
        public static void NameDirect(string[] data)
        {
            // Массив массивов строк для удобства сортировки данных. Две первые строки - колонки => длина меньше на 2.
            string[][] NewData = new string[data.Length - 2][];
            // Заполняем NewData.
            for (int i = 2; i < data.Length; i++)
            {
                NewData[i-2] = data[i].Split(";").ToArray();
            }
            // Сортировка методом OrderBy.
            string[][] SortedData = NewData.OrderBy(subArray => subArray[1]).ToArray();

            // Вывод данных.
            OtherMethods.OutputSorted(SortedData);

            Console.WriteLine("Поздравляю! Программа отработала успешно.");
            Console.WriteLine("Хотите ли вы сохранить полученные данные в файле? Если да, то введите YES");
            string? Save = Console.ReadLine();
            if (Save == "YES")
            {
                // Вызов метода для сохранения полученных данных.
                OtherMethods.SaveForSorted(SortedData);
            }
        }

        /// <summary>
        ///  Метод NameReverse - проводит сортировку данных data по колонке Name в обратном порядке.
        /// </summary>
        public static void NameReverse(string[] data)
        {
            // Аналогично, как в методе NameDirect.
            string[][] NewData = new string[data.Length - 2][];
            for (int i = 2; i < data.Length; i++)
            {
                NewData[i-2] = data[i].Split(";").ToArray();
            }
            // Сортировка методом OrderByDescending.
            string[][] SortedData = NewData.OrderByDescending(subArray => subArray[1]).ToArray();

            // Вывод данных.
            OtherMethods.OutputSorted(SortedData);

            Console.WriteLine("Поздравляю! Программа отработала успешно.");
            Console.WriteLine("Хотите ли вы сохранить полученные данные в файле? Если да, то введите YES");
            string? Save = Console.ReadLine();
            if (Save == "YES")
            {
                // Вызов метода для сохранения данных.
                OtherMethods.SaveForSorted(SortedData);
            }
        }
    }
}
