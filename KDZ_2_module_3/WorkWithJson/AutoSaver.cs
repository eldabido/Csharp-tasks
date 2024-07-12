using System.Text.Json;
namespace WorkWithJson
{
    /// <summary>
    /// Класс AutoSaver - класс, подписанный на событие о смене поля.
    /// </summary>
    public class AutoSaver
    {
        // Поля.
        // Время последнего обновления.
        DateTime _lastUpdate;
        // Название файла.
        string? _json_File_Name;

        /// <summary>
        /// Конструктор по умолчанию.
        /// </summary>
        public AutoSaver()
        {
        }
        /// <summary>
        /// Конструктор с именем файла.
        /// </summary>
        public AutoSaver(string? _Json_File_Name) 
        {
           _json_File_Name = _Json_File_Name;
        }
        
        /// <summary>
        /// метод HandleUpdate - метод, срабатывабщий при смене значения поля в классе Hero (событии).
        /// </summary>
        public void HandleUpdate(object data, HeroEventArgs e)
        {
            // Если прошло не больше 15 секунд, то сохраняем данные.
            if ((e.TimeChanging.Subtract(_lastUpdate)).TotalSeconds <= 15)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Два изменения полей за 15 секунд. Данные сохраняются в файл с припиской _tmp.");
                Console.ResetColor();
                ToSaveFile((Hero[])data);
            }
            // Обновление времени последнего изменения.
            _lastUpdate = e.TimeChanging;
        }

        /// <summary>
        /// Метод ToSaveFile - занимается сохранением данных.
        /// </summary>
        public void ToSaveFile(Hero[] data)
        {
            string? new_file = "";
            // Формирование названия файла. (Нужно убрать .json в конце, чтоб добавить _tmp.
            for (int i = 0; i < _json_File_Name!.Length - 5; i++) 
            {
                new_file += _json_File_Name[i];
            }
            // Сериализация.
            var options = new JsonSerializerOptions { WriteIndented = true };
            string jsonString = JsonSerializer.Serialize(data, options);
            // Запись в файл.
            File.WriteAllText(new_file + "_tmp.json", jsonString);
        }
    }
}
