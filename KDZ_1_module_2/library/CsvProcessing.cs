using System.IO;

namespace WorkWithCsvLibrary
{
    public static class CsvProcessing
    {
        // fPath хранит путь к файлу.
        static string? fPath;
        // Для обращения к fPath.
        public static string fPathProp
        {
            get { return fPath; }
            set { fPath = value; }
        }
        /// <summary>
        /// Метод Read() - считывает данные из csv файла, если он корректен.
        /// </summary>
        public static string[] Read()
        {
            // rowData хранит те самые данные.
            string[]? rowData = null;
            try
            {
                // Проверка на существование и формат файла.
                if (File.Exists(fPath) && Path.GetExtension(fPath) == ".csv" && OtherMethods.CheckFormat(fPath))
                {
                    rowData = File.ReadAllLines(fPath);
                }
                else
                {
                    // Если файл отсутствует или его структура не соответствует варианту, то выбрасываем исключение.
                    throw new ArgumentNullException("fPath");
                }
            }
            catch (ArgumentNullException)
            {
                Console.WriteLine("Message: Исключение ArgumentNullException (Файл отсутствует" +
                    " или его структура не соответствует ваианту)");
            }
            return rowData;
        }

        /// <summary>
        /// Метод Write - принимает на вход строку data и путь к файлу nPath и добавляет её туда.
        /// </summary>
        public static void Write(string data, string nPath)
        {
            // Открытие файла для записи.
            if (File.Exists(nPath))
            {
                try
                {
                    // Нужно true, чтобы именно добавить строку.
                    using (StreamWriter sw = new StreamWriter(nPath, true))
                    {
                        sw.WriteLine(data);
                        Console.WriteLine("Данные успешно добавлены в файл.");
                    }
                }
                catch
                {
                    Console.WriteLine("Что-то не так с файлом. Попробуйте ввести еще раз (просто название).");
                    // Предложение ввести заново.
                    Console.WriteLine("Если не хотите, то введите NO");
                    string? FileName = Console.ReadLine();
                    if (FileName != "NO")
                        Write(data, FileName + ".csv");
                }
            }
            else
            {
                // Создание нового файла.
                try
                {
                    using (StreamWriter sw = new StreamWriter(nPath))
                    {
                        sw.WriteLine(data);
                    }
                    Console.WriteLine("Файл успешно создан и содержит полученные данные.");
                }
                catch
                {
                    Console.WriteLine("Либо путь до файла указан некорректно, либо плохое название файла!");
                    Console.WriteLine("Попробуйте ввести еще раз (только название, если не хотите, то введите NO)");
                    string? FileName = Console.ReadLine();
                    if (FileName != "NO")
                        Write(data, FileName + ".csv");
                }
            }
        }
        /// <summary>
        /// Перегрузка метода Write - теперь принимает массив строк data, которые нужно добавить в nPath.
        /// </summary>
        public static void Write(string[] data, string nPath)
        {
            try
            {
                // Записываем данные, стирая исходное содержимое.
                using (StreamWriter sw = new StreamWriter(nPath))
                {
                    for (int i = 0; i < data.Length; i++)
                    {
                        sw.WriteLine(data[i]);
                    }
                    Console.WriteLine("Данные успешно добавлены в файл.");
                }
            }
            catch
            {
                Console.WriteLine("Либо путь до файла указан некорректно, либо плохое название файла!");
                Console.WriteLine("Попробуйте ввести еще раз (если не хотите, то введите NO)");
                string? FileName = Console.ReadLine();
                if (FileName != "NO")
                    Write(data, FileName + ".csv");
            }
        }
    }  
}