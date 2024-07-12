using WorkWithJson;

class Program
{
    static void Main()
    {
        Console.WriteLine("Здравствуйте!");
        Console.WriteLine("(Если у вас есть вопросы по программе, то пишите в telegram: @el_dabido)");
        while (true)
        {
            Console.WriteLine("Введите путь файла");
            string? fPath = Console.ReadLine();
            // Получение данных.
            Hero[] heroes = ReadingAndWritingJson.ReadJson(fPath!);
            // Переход в меню.
            if (heroes.Length != 0)
            {
                Menu.MenuOfProg(heroes);
            }
            Console.WriteLine("Если хотите продолжить, то нажмите Enter. Иначе - что-то другое");
            string? answer = Console.ReadLine();
            if (answer != "")
            {
                break;
            }
        }
    }
}