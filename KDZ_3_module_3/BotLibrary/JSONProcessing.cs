using System.Text.Json;
using System.Text.Encodings.Web;

namespace BotLibrary
{
    /// <summary>
    /// Класс JSONProcessing - класс, содержащий методы для считывания и записи данных у csv файлов.
    /// </summary>
    public class JSONProcessing
    {
        /// <summary>
        /// Метод Read - метод, считывающий данные с json файла.
        /// </summary>
        public List<Attraction> Read(Stream stream)
        {
            // Считываем с помощью System.Text.Json.
            var attractions = JsonSerializer.DeserializeAsync<List<Attraction>>(stream).Result;
            return attractions!;
        }
        /// <summary>
        /// Метод Write - метод, записывающий данные с json файла.
        /// </summary>
        public Stream Write(List<Attraction> attractions, string fName)
        {
            using (FileStream fileStream = File.Create(fName))
            {
                using (Utf8JsonWriter writer = new Utf8JsonWriter(fileStream, new JsonWriterOptions
                    {
                    // Для правильной кодировки.
                        Indented = true,
                        Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
                    }))
                {
                    JsonSerializer.Serialize(writer, attractions);
                    return fileStream;
                }
            }
        }
    }
}
