using System.Text.RegularExpressions;


namespace WorkWithJson
{
    /// <summary>
    /// Класс OtherMethods - статический вспомогательный класс, содержащий методы для взаимодействия с пользователем и проверки формата.
    /// </summary>
    public static class OtherMethods
    {
        /// <summary>
        /// Метод ChooseOption1 - метод, выводящий возможности главного меню.
        /// </summary>
        public static void ChooseOption1()
        {
            Console.WriteLine("Выберите желаемое действие:");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("1. Передать новый путь к файлу. (Выход из меню)");
            Console.WriteLine("2. Отсортировать массив данных по одному из полей" +
                "(не включая вложенные объекты).");
            Console.WriteLine("3. Выбрать объект и отредактировать в нем любое поле, " +
                "кроме идентификаторов (поля, в названии которых есть «Id») и полей, " +
                "которые изменяются в результате активации и обработки событий.");
            Console.ResetColor();
        }
        /// <summary>
        /// Метод ChooseOption2 - метод, выводящий список полей для сортировки.
        /// </summary>
        public static void ChooseOption2()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("1. hero_id");
            Console.WriteLine("2. hero_name");
            Console.WriteLine("3. faction");
            Console.WriteLine("4. level");
            Console.WriteLine("5. castle_location");
            Console.WriteLine("6. troops");
            Console.ResetColor();
        }
        /// <summary>
        /// Метод ChooseOption2 - метод, выводящий список полей для изменения.
        /// </summary>
        public static void ChooseOption3()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("1. hero_name");
            Console.WriteLine("2. faction");
            Console.WriteLine("3. level");
            Console.WriteLine("4. castle_location");
            Console.WriteLine("5. units");
            Console.ResetColor();
        }
        /// <summary>
        /// Метод ChooseOption2 - метод, выводящий список полей Units для изменения..
        /// </summary>
        public static void ChooseOption4()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("1. unit_name");
            Console.WriteLine("2. quantity");
            Console.WriteLine("3. experience");
            Console.WriteLine("4. current_location");
            Console.ResetColor();
        }

        public static bool CheckFormat(string fPath)
        {
            // Открываем файл.
            using (FileStream fs = new FileStream(fPath, FileMode.Open))
            {
                using (StreamReader sr = new StreamReader(fs))
                {
                    // Получаем первую строку, которая должна быть "["
                    string? line = sr.ReadLine();
                    // С помощью регулярных выражений будем проверять строки на соответствие.
                    var hero_id = new Regex(@"    ""hero_id"": "".+""+,$");
                    var hero_name = new Regex(@"    ""hero_name"": "".+"",$");
                    var faction = new Regex(@"    ""faction"": "".+"",$");
                    var level = new Regex(@"    ""level"": \d+,$");
                    var castle_location = new Regex(@"    ""castle_location"": "".+"",$");
                    var troops = new Regex(@"    ""troops"": \d+,$");
                    var unit_name = new Regex(@"        ""unit_name"": "".+"",$");
                    var quantity = new Regex(@"        ""quantity"": \d+,$");
                    var experience = new Regex(@"        ""experience"": \d+,$");
                    var current_location = new Regex(@"        ""current_location"": "".+""$");
                    // Проверка на первую строку.
                    if (line != "[")
                    {
                        return false;
                    }
                    else
                    {
                        // Цикл, проходящий по файлу.
                        while (line != "]")
                        {
                            // Проверка открывающей скобки.
                            if ((line = sr.ReadLine()) == "  {")
                            {
                                line = sr.ReadLine()!;
                            }
                            else
                            {
                                return false;
                            }
                            // Проверяем следующие строки на соответствие с помощью регулярок.
                            if (hero_id.IsMatch(line) && hero_name.IsMatch(sr.ReadLine()!)
                                && faction.IsMatch(sr.ReadLine()!) && level.IsMatch(sr.ReadLine()!)
                                && castle_location.IsMatch(sr.ReadLine()!) && troops.IsMatch(sr.ReadLine()!)
                                && (line = sr.ReadLine()) == "    \"units\": [")
                            {
                                // Отдельно проверяем speakers'ов, так как там массив.
                                line = sr.ReadLine()!;
                                while (line != "    ]")
                                {
                                    if (line == "      {")
                                    {
                                        line = sr.ReadLine();
                                    }
                                    else
                                    {
                                        return false;
                                    }
                                    if (unit_name.IsMatch(line!) && quantity.IsMatch(sr.ReadLine()!)
                                    && experience.IsMatch(sr.ReadLine()!) && current_location.IsMatch(sr.ReadLine()!))
                                    {
                                        line = sr.ReadLine();
                                        if (line == "      }" || line == "      },") 
                                        {
                                            line = sr.ReadLine();
                                        }
                                        else
                                        {
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        return false;
                                    }


                                }
                            }
                            else
                            {
                                return false;
                            }
                            // Проверка на конец блока данных и переход к следующему.
                            line = sr.ReadLine();
                            // Если блок последний, то запятой в конце не будет.
                            if (line == "  }")
                            {
                                line = sr.ReadLine();
                            }
                        }
                        return true;
                    }
                }
            }
        }
    }
}
