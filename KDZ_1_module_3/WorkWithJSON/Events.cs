namespace WorkWithJSON
{
    /// <summary>
    /// Класс Event - класс, содержащий данные о каждом мероприятии.
    /// </summary>
    public class Event
    {
        // Поля для хранения данных.
        public int _event_id { get; }
        public string? _event_name { get; }
        public string? _location { get; }
        public string? _date { get; }
        public int _attendees { get; }
        public string? _organizer { get; }
        public string[]? _speakers { get; }

        // Конструктор по умолчанию.
        public Event() 
        {
            _event_id = 0;
            _event_name = null;
            _location = null;
            _date = null;
            _attendees = 0;
            _organizer = null;
            _speakers = null;
        }
        // Конструктор для массива данных.
        public Event(string[] data)
        {
            _event_id = int.Parse(data[0]);
            _event_name = data[1];
            _location = data[2];
            _date = data[3];
            _attendees = int.Parse(data[4]);
            _organizer = data[5];
            // Отдельно нужно обработать строку-массив для speakers.
            // ind - индекс для прохода по data.
            int ind = 0;
            // speak - массив для передачи в speakers.
            string[] speak = new string[data.Length];
            // i - для прохода по speak.
            int i = 0;
            // count - для подсчета размера массива.
            int count = 0;
            // Цикл до конца строки.
            while (ind < data[6].Length)
            {
                // Считываем до кавычек, чтобы пропустить пробелы.
                while (data[6][ind++] != '\"')
                {
                }
                // Считываем элемент массива.
                while (data[6][ind] != '\"')
                {
                    speak[i] += data[6][ind++];
                }
                // Идем дальше.
                ind++;
                count++;
                i++;
            }
            // С помощью среза получаем speakers.
            _speakers = speak[..count];
        }

        /// <summary>
        /// Метод ToString - выводит все данные на экран.
        /// </summary>
        public override string ToString()
        {
            string ans =  $"event_id = {_event_id};\nevent_name = {_event_name};\nlocation = {_location};\n" +
                $"date = {_date};\nattendees = {_attendees};\norganizer = {_organizer}\nspeakers: ";
            // Вывод массива speakers на экран отдельно.
            if (_speakers != null)
            {
                for (int i = 0; i < _speakers.Length; i++)
                {
                    ans += $"{_speakers[i]} ";
                }
            }
            return ans + "\n";
        }
    }
}