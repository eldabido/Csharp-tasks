using System.Text.Json;
namespace WorkWithJson
{
    /// <summary>
    /// Класс ReadingAndWritingJson - статический класс, содержащий методы для чтения и записи данных в json файл.
    /// </summary>
    public static class ReadingAndWritingJson
    {
        /// <summary>
        /// Метод ReadJson - метод, считывающий данные с json файла с помощью десериализации.
        /// </summary>
        public static Hero[] ReadJson(string fPath)
        {
            // Создаем массив здесь, чтобы можно было сделать return в catch, а то пришлось бы писать return null.
            Hero[] heroes = new Hero[0];
            try
            {
                if (OtherMethods.CheckFormat(fPath))
                {
                    // Считывание данных.
                    string jsonString = File.ReadAllText(fPath);
                    // Десериализация.
                    heroes = JsonSerializer.Deserialize<Hero[]>(jsonString)!;
                    // Создание классов, которые содержат методы, выполняющиеся при определенных событиях.
                    AutoSaver autoSaver = new AutoSaver(fPath);
                    CounterOfTroops counterOfTroops = new CounterOfTroops();
                    // Подпись на события.
                    foreach (Hero hero in heroes)
                    {
                        hero.Updated += autoSaver.HandleUpdate!;
                        hero.LocationChanged += counterOfTroops.HandleCounter;
                    }
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Плохой формат файла");
                    Console.ResetColor();
                }
                return heroes;
            }
            catch
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Похоже, файл не соответствует формату. Проверьте его существование, формат (json) и внутренность");
                Console.ResetColor();
                return heroes;
            }
        }
        /// <summary>
        /// Метод WriteJson - метод, записывающий данные в json файл с помощью сериализации.
        /// </summary>
        public static void WriteJson(Hero[] data, string fName)
        {
            try
            {
                // Сериализация.
                var options = new JsonSerializerOptions { WriteIndented = true };
                string jsonString = JsonSerializer.Serialize(data, options);
                // Запись в файл.
                File.WriteAllText(fName, jsonString);

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Запись завершена");
                Console.ResetColor();
            }
            catch
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Что-то пошло не так. Возможно, плохое название файла.");
                Console.ResetColor();
            }
        }
    }
}
