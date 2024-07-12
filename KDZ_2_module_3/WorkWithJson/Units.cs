using System.Text.Json;
using System.Text.Json.Serialization;

namespace WorkWithJson
{
    /// <summary>
    /// Класс  Units - класс, содержащий данные о поле Units героя Hero.
    /// </summary>
    [Serializable]
    public class Units
    {
        // Поля.
        string? _unit_name;
        int _quantity;
        int _experience;
        string? _current_location;

        // Свойства.
        [JsonPropertyName("unit_name")]
        public string? Unit_name { get { return _unit_name; } set { _unit_name = value; } }
        [JsonPropertyName("quantity")]
        public int Quantity { get { return _quantity; } set { _quantity = value; } }
        [JsonPropertyName("experience")]
        public int Experience { get { return _experience; } set { _experience = value; } }
        [JsonPropertyName("current_location")]
        public string? Current_location { get { return _current_location; } set { _current_location = value; } }

        /// <summary>
        /// Конструктор, принимающий значения для всех полей.
        /// </summary>
        public Units(string unit_name, int quantity, int experience, string current_location)
        {
            _unit_name = unit_name;
            _quantity = quantity;
            _experience = experience;
            _current_location = current_location;
        }

        /// <summary>
        /// Конструктор по умолчанию.
        /// </summary>
        public Units()
        {
        }

        /// <summary>
        /// Метод ToJSON - метод, предоставляющий строковое представление текущего объекта в JSON формате
        /// </summary>
        public string ToJSON()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
