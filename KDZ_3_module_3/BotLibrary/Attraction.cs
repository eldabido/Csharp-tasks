using CsvHelper.Configuration.Attributes;
using System.Text.Json.Serialization;

namespace BotLibrary
{
    /// <summary>
    /// Класс Attraction - класс, хранящий данные о местах отдыха.
    /// </summary>
    [Serializable]
    public class Attraction
    {
        // Поля.
        string? _id;
        string? _name;
        string? _photo;
        string? _global_id;
        string? _admarea;
        string? _district;
        string? _location;
        string? _registration_number;
        string? _state;
        string? _locationtype;
        string? _geodata_center;
        string? _geoarea;

        // Свойства вместе с атрибутами, чтоб можно было считывать данные в них.
        [Name("ID")]
        [JsonPropertyName("ID")]
        public string? Id { get { return _id; } set { _id = value; } }
        [Name("Name")]
        [JsonPropertyName("Name")]
        public string? Name { get { return _name; } set { _name = value; } }
        [Name("Photo")]
        [JsonPropertyName("Photo")]
        public string? Photo { get { return _photo; } set { _photo = value; } }
        [Name("global_id")]
        [JsonPropertyName("global_id")]
        public string? Global_Id { get { return _global_id; } set { _global_id = value; } }
        [Name("AdmArea")]
        [JsonPropertyName("AdmArea")]
        public string? AdmArea { get { return _admarea; } set { _admarea = value; } }
        [Name("District")]
        [JsonPropertyName("District")]
        public string? District { get { return _district; } set { _district = value; } }
        [Name("Location")]
        [JsonPropertyName("Location")]
        public string? Location { get { return _location; } set { _location = value; } }
        [Name("RegistrationNumber")]
        [JsonPropertyName("RegistrationNumber")]
        public string? Registration_Number { get { return _registration_number; } set { _registration_number = value; } }
        [Name("State")]
        [JsonPropertyName("State")]
        public string? State { get { return _state; } set { _state = value; } }
        [Name("LocationType")]
        [JsonPropertyName("LocationType")]
        public string? Location_Type { get { return _locationtype; } set { _locationtype = value; } }
        [Name("geodata_center")]
        [JsonPropertyName("geodata_center")]
        public string? Geodata_Center { get { return _geodata_center; } set { _geodata_center = value; } }
        [Name("geoarea")]
        [JsonPropertyName("geoarea")]
        public string? Geoarea { get { return _geoarea; } set { _geoarea = value; } }

    }
}
