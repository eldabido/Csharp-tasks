using System.Text.Json;
using System.Text.Json.Serialization;
namespace WorkWithJson
{
    // Событийный делегат.
    public delegate Hero[] LocationChangedHandler(Hero[] heroes, int num);
    
    /// <summary>
    /// Класс Hero - класс, содержащий информацию о герое.
    /// </summary>
    public class Hero
    {
        // Поля.
        string? _hero_id;
        string? _hero_name;
        string? _faction;
        int _level;
        string? _castle_location;
        int _troops;
        Units[]? _units;

        // Свойства.
        [JsonPropertyName("hero_id")]
        public string? Hero_id { get { return _hero_id; } set { _hero_id = value; } }
        [JsonPropertyName("hero_name")]
        public string? Hero_name { get { return _hero_name; } set { _hero_name = value; } }
        [JsonPropertyName("faction")]
        public string? Faction { get { return _faction; } set { _faction = value; } }
        [JsonPropertyName("level")]
        public int Level { get { return _level; } set { _level = value; } }
        [JsonPropertyName("castle_location")]
        public string? Castle_location { get { return _castle_location; } set { _castle_location = value; } }
        [JsonPropertyName("troops")]
        public int Troops { get { return _troops; } set { _troops = value; } }
        [JsonPropertyName("units")]
        public Units[] Units { get {  return _units!; } set { _units = value; } }

        /// <summary>
        /// Конструктор, принимающий значения для всех полей.
        /// </summary>
        public Hero(string hero_id, string hero_name, string faction, int level, string castle_location, int troops, Units[] units)
        {
            _hero_id = hero_id;
            _hero_name = hero_name;
            _faction = faction;
            _level = level;
            _castle_location = castle_location;
            _troops = troops;
            _units = units;
        }

        /// <summary>
        /// Конструктор по умолчанию.
        /// </summary>
        public Hero()
        {

        }

        // События.
        public event EventHandler<HeroEventArgs>? Updated;
        public event LocationChangedHandler? LocationChanged;

        /// <summary>
        /// Метод ToRecountTroops - метод, вызывающий событие LocationChanged.
        /// </summary>
        public Hero[] ToRecountTroops(Hero[] heroes, int num)
        {
            heroes = LocationChanged?.Invoke(heroes, num)!;
            return heroes;
        }

        /// <summary>
        /// Метод ToUpdate - метод, вызывающий событие Updated. 
        /// </summary>
        /// <param name="heroes"></param>
        public void ToUpdate(Hero[] heroes)
        {
            Updated?.Invoke(heroes, new HeroEventArgs(DateTime.Now));
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