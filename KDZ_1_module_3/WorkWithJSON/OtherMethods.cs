using System.Text.RegularExpressions;
namespace WorkWithJSON
{
    /// <summary>
    /// Класс OtherMethods - класс, содержащий вспомогательные методы для более удобного взаимодействия с программой.
    /// </summary>
    public class OtherMethods
    {
        /// <summary>
        /// Метод ChooseOption1 - метод, выводящий опции меню на экран.
        /// </summary>
        public static void ChooseOption1()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Укажите номер действия, которое вас интересует:");
            Console.WriteLine("(Если хотите, например, 1-ый пункт, то нужно ввести 1, и т. д.)");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("1. Фильтрация по полю");
            Console.WriteLine("2. Сортировка по полю");
            Console.WriteLine("3. Выход из меню");
            Console.ResetColor();
        }

        /// <summary>
        /// Метод ChooseOption2 - метод, выводящий список полей на экран.
        /// </summary>
        public static void ChooseOption2()
        {
            Console.WriteLine("(Если хотите, например, по 1-ому полю, то нужно ввести 1, и т. д.)");
            // Устанавливаем цвет для красоты.
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("1. event_id");
            Console.WriteLine("2. event_name");
            Console.WriteLine("3. location");
            Console.WriteLine("4. date");
            Console.WriteLine("5. attendees");
            Console.WriteLine("6. organizer");
            Console.WriteLine("7. speakers");
            Console.WriteLine("8. Выход из меню");
            Console.ResetColor();
        }

        /// <summary>
        /// Метод CheckFormat - метод, проверяющий файл на правильный формат.
        /// </summary>
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
                    var event_id = new Regex(@"    ""event_id"": \d+,$");
                    var event_name = new Regex(@"    ""event_name"": "".+"",$");
                    var location = new Regex(@"    ""location"": "".+"",$");
                    var date = new Regex(@"    ""date"": "".+"",$");
                    var attendees = new Regex(@"    ""attendees"": \d+,$");
                    var organizer = new Regex(@"    ""organizer"": "".+"",$");
                    var speakers1 = new Regex(@"      "".+"",");
                    var speakers2 = new Regex(@"      "".+""");
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
                            if (event_id.IsMatch(line) && event_name.IsMatch(sr.ReadLine()!)
                                && location.IsMatch(sr.ReadLine()!) && date.IsMatch(sr.ReadLine()!)
                                && attendees.IsMatch(sr.ReadLine()!) && organizer.IsMatch(sr.ReadLine()!)
                                && (line = sr.ReadLine()) == "    \"speakers\": [")
                            {
                                // Отдельно проверяем speakers'ов, так как там массив.
                                line = sr.ReadLine()!;
                                while (line != "    ]")
                                {
                                    if (speakers1.IsMatch(line!))
                                    {
                                        line = sr.ReadLine();
                                    }
                                    else if (speakers2.IsMatch(line!))
                                    {
                                        line = sr.ReadLine();
                                        if (line != "    ]")
                                        {
                                            return false;
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
