using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;

namespace BotLibrary
{
    /// <summary>
    /// Класс CsvProcessing - класс, содержащий методы для считывания и записи данных у csv файлов.
    /// </summary>
    public class CsvProcessing
    {
        /// <summary>
        /// Метод Read - метод, считывающий данные с csv файла.
        /// </summary>
        public List<Attraction> Read(Stream stream)
        {
            // Разделитель должен быть ";".
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                Delimiter = ";"
            };
            using (var reader = new StreamReader(stream))
            {
                using (var csv = new CsvReader(reader, config))
                {
                    // Считываем с помощью CsvHelper и преобразовываем в список.
                    var records = csv.GetRecords<Attraction>().ToList();
                    var rec = records.ToArray();
                    records = rec.ToList();
                    return records;
                }

            }
        }
        /// <summary>
        /// Метод Write - метод, записывающий данные с csv файла.
        /// </summary>
        public Stream Write(List<Attraction> records, string fName)
        {
            // Создаем файл и закрываем его.
            File.Create(fName).Close();
            // Создаем новый поток, и записываем данные.
            using (var writer = new FileStream(fName, FileMode.Open, FileAccess.Write))
            {
                using (var sr = new StreamWriter(writer))
                {
                    using (var csv = new CsvWriter(sr, new CsvConfiguration(CultureInfo.InvariantCulture)
                        {
                        Delimiter = ";",
                        Quote = '"'
                        }))
                    {
                        csv.WriteRecords(records);
                        return writer;
                    }
                }
            }
        }
    }
}
