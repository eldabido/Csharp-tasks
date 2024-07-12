namespace WorkWithJSON;
class Program
{
    // Название файла для перенаправления потока.
    static string s_fName = "data_10V.json";
    static void Main()
    {
        Console.WriteLine("Здравствуйте!");
        while (true)
        {
            Console.WriteLine("Нужно подать данные программе.");
            Console.WriteLine("Сделать это можно:");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("1. Указав полный путь к файлу");
            Console.WriteLine("2. Через System.Console.");
            Console.WriteLine("3. Закончить работу программы (ввести 3)");
            Console.ResetColor();
            Console.WriteLine("Если хотите указать путь, то просто введите его, а если через " +
                "System.Console, то нажмите Enter");
            string? fPath = Console.ReadLine();
            // Массив для хранения данных.
            Event[] events;
            // Считывание данных. Если нажали Enter, то строка пуста, и ввод через Console.
            if (fPath == "3") 
            {
                break;
            }
            else if (fPath != "")
            {
                events = JsonParser.ReadJson(fPath!, "1");
            }
            else
            {
                Console.WriteLine("Введите файл, в который хотите перенаправить поток");
                Console.WriteLine("(Можно нажать Enter, тогда введётся файл из задания).");
                string? ans = Console.ReadLine();
                if (ans == "") 
                    events = JsonParser.ReadJson(s_fName!, "2");
                else
                    events = JsonParser.ReadJson(ans!, "3");
            }
            // Если файл считался, то переходим в меню.
            Console.Clear();
            if (events != null)
            {
                Menu.MenuOfProg(events);
            }

        }
    }
}