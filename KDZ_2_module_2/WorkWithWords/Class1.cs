namespace WorkWithWords;

/// <summary>
/// Класс CharArr2D - основной класс для работы с предложениями.
/// </summary>
public class CharArr2D
{
    char[][] _charArr;

    /// <summary>
    /// Конструктор по умолчанию.
    /// </summary>
    public CharArr2D() { }

    /// <summary>
    /// Конструктор с параметром-строкой.
    /// </summary>
    public CharArr2D(string Sentence)
    {
        // Проверка на подходящий формат предложения.
        int res = 0;
        // Проверка на строку из одной точки. Это отдельный случай предложения нулевой длины.
        if (Sentence == ".")
        {
            _charArr = new char[0][];
        }
        else
        {
            res = CheckFormat(Sentence);
            if (res == 1)
            {
                // Если всё хорошо, то разбиваем предложение на слова, но исключаем точку, чтобы не мешалась.
                string[] arr = Sentence[..(Sentence.Length - 1)].Split(' ');
                // Инициализируем поле.
                _charArr = new char[arr.Length][];
                // Присваиваем значения.
                for (int i = 0; i < _charArr.Length; i++)
                {
                    _charArr[i] = new char[arr[i].Length];
                    for (int j = 0; j < arr[i].Length; j++)
                    {
                        _charArr[i][j] = arr[i][j];
                    }
                }
            }
        }
    }

    /// <summary>
    /// Конструктор с параметром char[][].
    /// </summary>
    public CharArr2D(char[][] Arr)
    {
        if (Arr != null)
        {
            // Проверка на то что в Arr только латинские символы.
            for (int i = 0; i < Arr.Length; i++)
            {
                for (int j = 0; j < Arr[i].Length; j++)
                {
                    if (('A' > Arr[i][j] || Arr[i][j] > 'Z') && ('a' > Arr[i][j] || Arr[i][j] > 'z'))
                    {
                        Console.WriteLine("В слове присутствуют нелатинские символы!");
                        // Передаётся именно ArgumentException, так как переданное значение недопустимо.
                        throw new ArgumentException();
                    }
                }
            }
            // Инициализация.
            _charArr = new char[Arr.Length][];
            // Присваивание значений.
            for (int i = 0; i < Arr.Length; i++)
            {
                _charArr[i] = new char[Arr[i].Length];
                for (int j = 0; j < Arr[i].Length; j++)
                {
                    _charArr[i][j] = Arr[i][j];
                }
            }
        }
    }

    /// <summary>
    /// Свойство, возвращающее строки только с гласными буквами.
    /// </summary>
    public char[][] OnlyVowels
    {
        get
        {
            if (_charArr != null)
            {
                // Vowels будет итоговым массивом. Берем размер с запасом, вернем только его часть.
                char[][] Vowels = new char[_charArr.Length][];
                // Индекс массива Vowels.
                int indVow = 0;
                // Цикл для присваивания значений.
                for (int i = 0; i < _charArr.Length; i++)
                {
                    int check = 1;
                    for (int j = 0; j < _charArr[i].Length; j++)
                    {
                        // Если есть гласная, то строка не подходит.
                        if (!CheckVow(_charArr[i][j]))
                        {
                            check = 0;
                            break;
                        }
                    }
                    // Если все буквы гласные, то присваиваем.
                    if (check == 1)
                    {
                        Vowels[indVow] = _charArr[i];
                        indVow++;
                    }
                }
                // Возвращаем срез с нужными строками.
                return Vowels[..indVow];
            }
            else
            {
                return null;
            }
        }
    }

    /// <summary>
    /// Метод,  проверяющий символ на то, является ли он гласной буквой.
    /// </summary>
    static bool CheckVow(char let)
    {
        if (let == 'a' || let == 'e' || let == 'i' || let == 'o' || let == 'u' || let == 'y')
        {
            return true;
        }
        else if (let == 'A' || let == 'E' || let == 'I' || let == 'O' || let == 'U' || let == 'Y')
        {
            return true;
        }
        return false;
    }

    /// <summary>
    /// Метод, проверяющий корректность формата переданной строки.
    /// </summary>
    static int CheckFormat(string str)
    {
        
        if (str[^1] != '.')
        {
            Console.WriteLine("В конце предложения не точка! Некорректно.");
            // Передаётся именно ArgumentException, так как переданное значение недопустимо.
            throw new ArgumentException(str);
        }
        // Делаем массив из слов, исключаем точку, чтобы не мешалась.
        string[] words = str[..(str.Length - 1)].Split(' ');
        for (int i = 0; i < words.Length; i++)
        {
            // Проверка на то, что символы латинские.
            if (words[i].Length != 0)
            {
                for (int j = 0; j < words[i].Length; j++)
                {
                    if (('A' > words[i][j] || words[i][j] > 'Z') && ('a' > words[i][j] || words[i][j] > 'z'))
                    {
                        Console.WriteLine("В слове присутствуют нелатинские символы!");
                        // Передаётся именно ArgumentException, так как переданное значение недопустимо.
                        throw new ArgumentException(str);
                    }
                }
            }
            else
            {
                Console.WriteLine("Возможно, в строке лишние пробелы.");
                throw new ArgumentException(str);
            }
        }
        return 1;
    }

    /// <summary>
    /// Метод, печатающий поле _charArr.
    /// </summary>
    public void Print()
    {
        // Если поле непустое, то просто поэлементно печатаем.
        if (_charArr != null && _charArr.Length != 0)
        {
            for (int i = 0; i < _charArr.Length; i++)
            {
                if (_charArr[i] != null && _charArr[i].Length != 0)
                {
                    foreach (char c in _charArr[i])
                    {
                        Console.Write(c);
                    }
                    Console.Write(" ");
                }
            }
            Console.WriteLine();
        }
    }

    /// <summary>
    /// Метод, записывающий полученные данные в файл.
    /// </summary>
    public void Write(string fPath, int NotEmpty)
    {
        // Проверка на существование файла.
        if (File.Exists(fPath) && NotEmpty != 0)
        {
            try
            {
                // Нужно true, чтобы именно добавить строку.
                using (StreamWriter sw = new StreamWriter(fPath, true))
                {
                    // Записываем поэлементно в массив.
                    for (int i = 0; i < _charArr.Length; i++)
                    {
                        for (int j = 0; j < _charArr[i].Length; j++)
                        {
                            sw.Write(_charArr[i][j]);
                        }
                        if (i != _charArr.Length - 1)
                        {
                            sw.Write(" ");
                        }
                    }
                    sw.WriteLine(".");
                }
            }
            catch
            {
                // Бросаем ArgumentException, так как проблема в передаче некорректных данных.
                Console.WriteLine("Что-то не так с файлом.");
                throw new ArgumentException(fPath);
            }
        }
        else
        {
            // Создание нового файла или начало записи в существующий.
            try
            {
                using (StreamWriter sw = new StreamWriter(fPath))
                {
                    // Записываем поэлементно в массив.
                    for (int i = 0; i < _charArr.Length; i++)
                    {
                        for (int j = 0; j < _charArr[i].Length; j++)
                        {
                            sw.Write(_charArr[i][j]);
                        }
                        if (i != _charArr.Length - 1)
                        {
                            sw.Write(" ");
                        }
                    }
                    // Записываем в конец точку, означающую конец предложения.
                    sw.WriteLine(".");
                }
            }
            catch
            {
                // Бросаем ArgumentException, так как проблема в передаче некорректных данных.
                Console.WriteLine("Что-то пошло не так. Возможно, плохое название файла.");
                throw new ArgumentException(fPath);
            }
        }
    }

    /// <summary>
    /// Свойство, проверяющее на null наше поле.
    /// </summary>
    public bool IsNull
    {
        get
        {
            if (_charArr == null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}


/// <summary>
/// Еще один класс для остальных методов.
/// </summary>
public class WorkWithFile
{
    /// <summary>
    /// Метод, считывающий данные из файла path в B.
    /// </summary>
    public static int Read(out CharArr2D[] B,  string path)
    {
        if (File.Exists(path))
        {
            // Считываем все строки.
            string[] data = File.ReadAllLines(path);
            // Инициализируем итоговый массив.
            B = new CharArr2D[data.Length];
            // Записываем строки в B.
            for (int i = 0; i < data.Length; i++)
            {
                try
                {
                    B[i] = new CharArr2D(data[i]);
                    // Если есть пустые строки, то они некорректны. Значит, завершаем работу.
                    if (B[i].IsNull)
                    {
                        Console.WriteLine("Какая-то строка не удовлетворяет формату.");
                        return 0;
                    }
                }
                catch (ArgumentException)
                {
                    Console.WriteLine("Message: Исключение ArgumentException (Строка не" +
                    " удовлетворяет формату)");
                    return 0;
                }
            }
            return 1;
        }
        else
        {
            Console.WriteLine("Файла не существует или в нем содержатся некорретные данные.");
            B = null;
            return 0;
        }
    }

    /// <summary>
    /// Из массива Sentences получаем новый, где предложения только с гласными.
    /// </summary>
    public static CharArr2D[] OnlyVow(CharArr2D[] Sentences)
    {
        try
        {
            // Инициализируем итоговый массив.
            CharArr2D[] Vow = new CharArr2D[Sentences.Length];
            int Ind = 0;
            for (int i = 0; i < Sentences.Length; i++)
            {
                if (Sentences[i].OnlyVowels != null && Sentences[i].OnlyVowels.Length != 0)
                {
                    // С помощью свойства OnlyVowels получаем нужные строки.
                    Vow[Ind] = new CharArr2D(Sentences[i].OnlyVowels);
                    Ind++;
                }
            }
            return Vow[..Ind];
        }
        catch (ArgumentException)
        {
            Console.WriteLine("Message: Исключение ArgumentException (Есть нелатинские символы).");
            return null;
        }
    }

    /// <summary>
    /// Метод, записывающий в файл массив объектов CharArr2D
    /// </summary>
    public static void Write(CharArr2D[] Sentences, string path)
    {
        try
        {
            // Так как запись идет поэлементно, то мы постоянно что-то добавляем в файл.
            // Чтобы не получилось так, что файл существует, а мы в него добавляем данные, а не
            // перезаписываем, то введем NotEmpty, чтобы показать, что запись уже идет.
            int NotEmpty = 0;
            for (int i = 0; i < Sentences.Length; i++)
            {
                // Поочередно записываем в файл.
                Sentences[i].Write(path, NotEmpty);
                NotEmpty++;
            }
            Console.WriteLine("Данные добавлены в файл.");
        }
        catch (ArgumentException)
        {
            Console.WriteLine("Message: Исключение ArgumentException (Что-то пошло не так. " +
                "Возможно, плохое название файла).");
        }
    }

    /// <summary>
    /// Метод для взаимодействия с пользователем.
    /// </summary>
    public static string WhatYouDesire()
    {
        Console.WriteLine("Если хотите остановиться, то введите STOP");
        Console.WriteLine("Если хотите начать заново, то введите что-то другое");
        return Console.ReadLine();

    }
}
