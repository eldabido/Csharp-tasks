namespace BotLibrary
{
    using System.IO;
    using System.IO.Pipes;
    using Telegram.Bot;
    using Telegram.Bot.Exceptions;
    using Telegram.Bot.Polling;
    using Telegram.Bot.Types;
    using Telegram.Bot.Types.Enums;
    using Telegram.Bot.Types.ReplyMarkups;

    /// <summary>
    /// класс BotWorking - основной класс, содержащий меню работы с ботом.
    /// </summary>
    public class BotWorking
    {
        /// <summary>
        /// Метод Start - метод, запускающий работу с ботом.
        /// </summary>
        public static Task Start()
        {
            var botClient = new TelegramBotClient("<Your token>");

            using CancellationTokenSource cts = new();

            // StartReceiving does not block the caller thread. Receiving is done on the ThreadPool.
            ReceiverOptions receiverOptions = new()
            {
                AllowedUpdates = Array.Empty<UpdateType>() // receive all update types except ChatMember related updates
            };

            CsvProcessing csvProcessing = new CsvProcessing();
            //  Словарь, содержащий состояние пользователя (на каком он этапе).
            Dictionary<long, string> ConditionsOfUsers = new Dictionary<long, string>();
            // Содержит пути файлов.
            Dictionary<long, string> FilesOfUsers = new Dictionary<long, string>();
            // Содержит данные из файлов.
            Dictionary<long, List<Attraction>> DataOfUsers = new Dictionary<long, List<Attraction>>();
            int count = 0;

            // Начинаем получать сообщения.
            botClient.StartReceiving(
                updateHandler: HandleUpdateAsync,
            pollingErrorHandler: HandlePollingErrorAsync,
            receiverOptions: receiverOptions,
            cancellationToken: cts.Token
        );


            Console.WriteLine($"Start listening");
            Console.ReadLine();


            // Send cancellation request to stop bot
            cts.Cancel();

            // Метод, реализующий основную работу бота.
            async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
            {
                // Проверяем на сообщение.
                if (update.Message is not { } message)
                    return;
                // Получаем ID чата.
                var chatId = message.Chat.Id;

                // Если это первое обращение к боту, то выводим данный текст.
                if (count == 0)
                {
                    Message sentMessage = await botClient.SendTextMessageAsync(
                            chatId: chatId,
                            text: "Отправьте json или csv файл",
                            cancellationToken: cancellationToken);
                    count++;
                }
                Console.WriteLine($"Received a message {message.Text} in chat {chatId}.");
                // Эта переменная будет содержать путь файла.
                string destinationFilePath;
                try
                {
                    // Если был прислан файл.
                    if (message.Type == MessageType.Document)
                    {
                        // Получаем информацию о файле.
                        var fileId = update.Message.Document!.FileId;
                        var fileInfo = await botClient.GetFileAsync(fileId);
                        var filePath = fileInfo.FilePath;
                        string fName = update.Message.Document.FileName!;
                        destinationFilePath = $"../Files/{fName}";
                        if (Path.GetExtension(destinationFilePath) != ".csv" && Path.GetExtension(destinationFilePath) != ".json")
                        {
                            throw new ArgumentException("error");
                        }
                        // Создание файла и добавление в словарь его пути.
                        await using Stream fileStream = System.IO.File.Create(destinationFilePath);
                        FilesOfUsers.Add(chatId, destinationFilePath);
                        // Загрузка в папку.
                        var file = await botClient.GetInfoAndDownloadFileAsync(
                            fileId: fileId,
                            destination: fileStream,
                            cancellationToken: cancellationToken);
                        // Изменение состояния и предложение меню.
                        ConditionsOfUsers.Remove(chatId);
                        ConditionsOfUsers[chatId] = "Отправил файл";
                        ReplyKeyboardMarkup replyKeyboardMarkup = new(new[] {
                        new KeyboardButton[] { "1. Выборка по полю AdmArea" },
                        new KeyboardButton[] { "2. Выборка по полю Geoarea" },
                        new KeyboardButton[] { "3. Выборка по полям District и geoarea" },
                        new KeyboardButton[] { "4. Отсортировать по полю Name по возрастанию" },
                        new KeyboardButton[] { "5. Отсортировать по полю Name по убыванию" },
                        new KeyboardButton[] { "6. Скачать файл в формате CSV" },
                        new KeyboardButton[] { "7. Скачать файл в формате JSON" },
                        new KeyboardButton[] { "8. Закончить работу с файлом" },
                    })
                        {
                            ResizeKeyboard = true
                        };
                        Message sentMessage = await botClient.SendTextMessageAsync(
                            chatId: chatId,
                            text: "Выберите желаемое действие",
                            replyMarkup: replyKeyboardMarkup,
                            cancellationToken: cancellationToken);
                    }


                    // Здесь if'ы по первому выбору
                    else if (ConditionsOfUsers.ContainsKey(chatId) && message.Text == "1. Выборка по полю AdmArea" && ConditionsOfUsers[chatId] == "Отправил файл")
                    {
                        // Меняем состояние.
                        ConditionsOfUsers.Remove(chatId);
                        ConditionsOfUsers[chatId] = "Отправил значение AdmArea";
                        Message sentMessage = await botClient.SendTextMessageAsync(
                            chatId: chatId,
                            text: "Введите значение, по которому будет выборка",
                            cancellationToken: cancellationToken);

                    }
                    else if (ConditionsOfUsers.ContainsKey(chatId) && message.Text == "2. Выборка по полю Geoarea" && ConditionsOfUsers[chatId] == "Отправил файл")
                    {
                        // Меняем состояние.
                        ConditionsOfUsers.Remove(chatId);
                        ConditionsOfUsers[chatId] = "Отправил значение Geoarea";
                        Message sentMessage = await botClient.SendTextMessageAsync(
                            chatId: chatId,
                            text: "Введите значение, по которому будет выборка",
                            cancellationToken: cancellationToken);

                    }
                    else if (ConditionsOfUsers.ContainsKey(chatId) && message.Text == "3. Выборка по полям District и geoarea" && ConditionsOfUsers[chatId] == "Отправил файл")
                    {
                        // Меняем состояние.
                        ConditionsOfUsers.Remove(chatId);
                        ConditionsOfUsers[chatId] = "Отправил значения District и geoarea";
                        Message sentMessage = await botClient.SendTextMessageAsync(
                            chatId: chatId,
                            text: "Введите значение, по которому будет выборка (Вводить их надо через \";\":\n" +
                            "Например: район Арбат;geoarea (в исходном файле она везде пустая почему-то) ",
                            cancellationToken: cancellationToken);

                    }
                    else if (ConditionsOfUsers.ContainsKey(chatId) && message.Text == "4. Отсортировать по полю Name по возрастанию" && ConditionsOfUsers[chatId] == "Отправил файл")
                    {
                        // Меняем состояние.
                        ConditionsOfUsers.Remove(chatId);
                        ConditionsOfUsers[chatId] = "Выбрал сортировку по возрастанию";
                        // Начинаем сортировку, считываем данные.
                        using (FileStream fileStream = new FileStream(FilesOfUsers[chatId], FileMode.Open, FileAccess.Read))
                        {
                            List<Attraction> data = new List<Attraction>();
                            if (Path.GetExtension(FilesOfUsers[chatId]) == ".csv")
                            {
                                data = csvProcessing.Read(fileStream);
                            }
                            else
                            {
                                JSONProcessing jsonProcessing = new JSONProcessing();
                                data = jsonProcessing.Read(fileStream);
                            }
                            // Считали, теперь сортируем и добавляем итог в словарь.
                            SortingAndFiltering sortingAndFiltering = new SortingAndFiltering();
                            List<Attraction> new_data = sortingAndFiltering.ToSort(data, "Прямой");
                            DataOfUsers[chatId] = new_data;
                        }

                        // Предлагаем меню.
                        ReplyKeyboardMarkup replyKeyboardMarkup = new(new[] {
                        new KeyboardButton[] { "1. Выборка по полю AdmArea" },
                        new KeyboardButton[] { "2. Выборка по полю Geoarea" },
                        new KeyboardButton[] { "3. Выборка по полям District и geoarea" },
                        new KeyboardButton[] { "4. Отсортировать по полю Name по возрастанию" },
                        new KeyboardButton[] { "5. Отсортировать по полю Name по убыванию" },
                        new KeyboardButton[] { "6. Скачать файл в формате CSV" },
                        new KeyboardButton[] { "7. Скачать файл в формате JSON" },
                        new KeyboardButton[] { "8. Закончить работу с файлом" },
                    })
                        {
                            ResizeKeyboard = true
                        };
                        ConditionsOfUsers.Remove(chatId);
                        ConditionsOfUsers[chatId] = "Выбрал следующее действие";
                        Message sentMessage = await botClient.SendTextMessageAsync(
                            chatId: chatId,
                            text: "Готово! Выберите следующее желаемое действие",
                            replyMarkup: replyKeyboardMarkup,
                            cancellationToken: cancellationToken);

                    }
                    else if (ConditionsOfUsers.ContainsKey(chatId) && message.Text == "5. Отсортировать по полю Name по убыванию" && ConditionsOfUsers[chatId] == "Отправил файл")
                    {
                        // Здесь аналогично возрастанию всё, только теперь сортировка по убыванию.
                        ConditionsOfUsers.Remove(chatId);
                        ConditionsOfUsers[chatId] = "Выбрал сортировку по убыванию";
                        using (FileStream fileStream = new FileStream(FilesOfUsers[chatId], FileMode.Open, FileAccess.Read))
                        {

                            List<Attraction> data = new List<Attraction>();
                            if (Path.GetExtension(FilesOfUsers[chatId]) == ".csv")
                            {
                                data = csvProcessing.Read(fileStream);
                            }
                            else
                            {
                                JSONProcessing jsonProcessing = new JSONProcessing();
                                data = jsonProcessing.Read(fileStream);
                            }
                            SortingAndFiltering sortingAndFiltering = new SortingAndFiltering();
                            List<Attraction> new_data = sortingAndFiltering.ToSort(data, "Обратный");
                            DataOfUsers[chatId] = new_data;
                            ReplyKeyboardMarkup replyKeyboardMarkup = new(new[] {
                        new KeyboardButton[] { "1. Выборка по полю AdmArea" },
                        new KeyboardButton[] { "2. Выборка по полю Geoarea" },
                        new KeyboardButton[] { "3. Выборка по полям District и geoarea" },
                        new KeyboardButton[] { "4. Отсортировать по полю Name по возрастанию" },
                        new KeyboardButton[] { "5. Отсортировать по полю Name по убыванию" },
                        new KeyboardButton[] { "6. Скачать файл в формате CSV" },
                        new KeyboardButton[] { "7. Скачать файл в формате JSON" },
                        new KeyboardButton[] { "8. Закончить работу с файлом" },
                    })
                            {
                                ResizeKeyboard = true
                            };
                            ConditionsOfUsers.Remove(chatId);
                            ConditionsOfUsers[chatId] = "Выбрал следующее действие";
                            Message sentMessage = await botClient.SendTextMessageAsync(
                                chatId: chatId,
                                text: "Готово! Выберите следующее желаемое действие",
                                replyMarkup: replyKeyboardMarkup,
                                cancellationToken: cancellationToken);
                        }
                    }

                    else if (ConditionsOfUsers.ContainsKey(chatId) && message.Text == "6. Скачать файл в формате CSV" && ConditionsOfUsers[chatId] == "Отправил файл")
                    {
                        // Если человек скинул файл, и захотел его обратно сразу в формате csv, то сначала считываем его, а потом записываем.
                        JSONProcessing jsonProcessing = new JSONProcessing();
                        List<Attraction> data = new List<Attraction>();
                        if (Path.GetExtension(FilesOfUsers[chatId]) == ".csv")
                        {

                            using (FileStream fileStream = new FileStream(FilesOfUsers[chatId], FileMode.Open, FileAccess.Read))
                            {
                                data = csvProcessing.Read(fileStream);
                            }
                        }
                        else
                        {
                            using (FileStream fileStream = new FileStream(FilesOfUsers[chatId], FileMode.Open, FileAccess.Read))
                            {
                                data = jsonProcessing.Read(fileStream);
                            }
                        }
                        // Новый файл будет с приставкой _new.
                        DataOfUsers[chatId] = data;
                        string new_name = Path.GetFileNameWithoutExtension(FilesOfUsers[chatId]) + "_new.csv";
                        Stream str = csvProcessing.Write(data, new_name);
                        // Запись, отправка и предложение меню.
                        using (FileStream fileStream = new FileStream(new_name, FileMode.Open, FileAccess.Read))
                        {
                            Message message1 = await botClient.SendDocumentAsync(
                                chatId: chatId,
                                document: InputFile.FromStream(stream: fileStream, fileName: new_name),
                                caption: "Вот ваш CSV файл. Выберите следующее желаемое действие");
                        }
                        ReplyKeyboardMarkup replyKeyboardMarkup = new(new[] {
                        new KeyboardButton[] { "1. Выборка по полю AdmArea" },
                        new KeyboardButton[] { "2. Выборка по полю Geoarea" },
                        new KeyboardButton[] { "3. Выборка по полям District и geoarea" },
                        new KeyboardButton[] { "4. Отсортировать по полю Name по возрастанию" },
                        new KeyboardButton[] { "5. Отсортировать по полю Name по убыванию" },
                        new KeyboardButton[] { "6. Скачать файл в формате CSV" },
                        new KeyboardButton[] { "7. Скачать файл в формате JSON" },
                        new KeyboardButton[] { "8. Закончить работу с файлом" },
                    })
                        {
                            ResizeKeyboard = true
                        };
                        ConditionsOfUsers.Remove(chatId);
                        ConditionsOfUsers[chatId] = "Выбрал следующее действие";
                    }
                    else if (ConditionsOfUsers.ContainsKey(chatId) && message.Text == "7. Скачать файл в формате JSON" && ConditionsOfUsers[chatId] == "Отправил файл")
                    {
                        // всё аналогично прошлому пункту, но с json теперь.
                        JSONProcessing jsonProcessing = new JSONProcessing();
                        List<Attraction> data = new List<Attraction>();
                        if (Path.GetExtension(FilesOfUsers[chatId]) == ".csv")
                        {

                            using (FileStream fileStream = new FileStream(FilesOfUsers[chatId], FileMode.Open, FileAccess.Read))
                            {
                                data = csvProcessing.Read(fileStream);
                            }
                        }
                        else
                        {
                            using (FileStream fileStream = new FileStream(FilesOfUsers[chatId], FileMode.Open, FileAccess.Read))
                            {
                                data = jsonProcessing.Read(fileStream);
                            }
                        }
                        DataOfUsers[chatId] = data;
                        string new_name = Path.GetFileNameWithoutExtension(FilesOfUsers[chatId]) + "_new.json";
                        Stream str = jsonProcessing.Write(data, new_name);
                        using (FileStream fileStream = new FileStream(new_name, FileMode.Open, FileAccess.Read))
                        {
                            Message message1 = await botClient.SendDocumentAsync(
                                chatId: chatId,
                                document: InputFile.FromStream(stream: fileStream, fileName: new_name),
                                caption: "Вот ваш Json файл. Выберите следующее желаемое действие");
                        }
                        ReplyKeyboardMarkup replyKeyboardMarkup = new(new[] {
                        new KeyboardButton[] { "1. Выборка по полю AdmArea" },
                        new KeyboardButton[] { "2. Выборка по полю Geoarea" },
                        new KeyboardButton[] { "3. Выборка по полям District и geoarea" },
                        new KeyboardButton[] { "4. Отсортировать по полю Name по возрастанию" },
                        new KeyboardButton[] { "5. Отсортировать по полю Name по убыванию" },
                        new KeyboardButton[] { "6. Скачать файл в формате CSV" },
                        new KeyboardButton[] { "7. Скачать файл в формате JSON" },
                        new KeyboardButton[] { "8. Закончить работу с файлом" },
                        })
                        {
                            ResizeKeyboard = true
                        };
                        ConditionsOfUsers.Remove(chatId);
                        ConditionsOfUsers[chatId] = "Выбрал следующее действие";
                    }

                    else if (ConditionsOfUsers.ContainsKey(chatId) && message.Text == "8. Закончить работу с файлом" && ConditionsOfUsers[chatId] == "Отправил файл")
                    {
                        // удаляем все данные, чтоб начать заново.
                        ConditionsOfUsers.Remove(chatId);
                        FilesOfUsers.Remove(chatId);
                        if (DataOfUsers.ContainsKey(chatId))
                        {
                            DataOfUsers.Remove(chatId);
                        }
                        Message sentMessage = await botClient.SendTextMessageAsync(
                            chatId: chatId,
                            text: "Готово! Скиньте следующий файл, если хотите",
                            cancellationToken: cancellationToken);
                    }

                    // Здесь обрабатываем случаи фильтрации после того, как нам скинули значение.
                    else if (ConditionsOfUsers.ContainsKey(chatId) && ConditionsOfUsers[chatId] == "Отправил значение AdmArea")
                    {
                        // В общем и целом, все аналогично, только теперь вызываем ToFilter.
                        using (FileStream fileStream = new FileStream(FilesOfUsers[chatId], FileMode.Open, FileAccess.Read))
                        {

                            List<Attraction> data = new List<Attraction>();
                            if (Path.GetExtension(FilesOfUsers[chatId]) == ".csv")
                            {
                                data = csvProcessing.Read(fileStream);
                            }
                            else
                            {
                                JSONProcessing jsonProcessing = new JSONProcessing();
                                data = jsonProcessing.Read(fileStream);
                            }
                            SortingAndFiltering sortingAndFiltering = new SortingAndFiltering();
                            List<Attraction> new_data = sortingAndFiltering.ToFilter(data, message.Text!, "AdmArea");
                            DataOfUsers[chatId] = new_data;
                        }
                        ReplyKeyboardMarkup replyKeyboardMarkup = new(new[] {
                        new KeyboardButton[] { "1. Выборка по полю AdmArea" },
                        new KeyboardButton[] { "2. Выборка по полю Geoarea" },
                        new KeyboardButton[] { "3. Выборка по полям District и geoarea" },
                        new KeyboardButton[] { "4. Отсортировать по полю Name по возрастанию" },
                        new KeyboardButton[] { "5. Отсортировать по полю Name по убыванию" },
                        new KeyboardButton[] { "6. Скачать файл в формате CSV" },
                        new KeyboardButton[] { "7. Скачать файл в формате JSON" },
                        new KeyboardButton[] { "8. Закончить работу с файлом" },
                    })
                        {
                            ResizeKeyboard = true
                        };
                        ConditionsOfUsers.Remove(chatId);
                        ConditionsOfUsers[chatId] = "Выбрал следующее действие";
                        Message sentMessage = await botClient.SendTextMessageAsync(
                            chatId: chatId,
                            text: "Готово! Выберите следующее желаемое действие",
                            replyMarkup: replyKeyboardMarkup,
                            cancellationToken: cancellationToken);
                    }
                    else if (ConditionsOfUsers.ContainsKey(chatId) && ConditionsOfUsers[chatId] == "Отправил значение Geoarea")
                    {
                        // В общем и целом, все аналогично, только теперь вызываем ToFilter.
                        using (FileStream fileStream = new FileStream(FilesOfUsers[chatId], FileMode.Open, FileAccess.Read))
                        {

                            List<Attraction> data = new List<Attraction>();
                            if (Path.GetExtension(FilesOfUsers[chatId]) == ".csv")
                            {
                                data = csvProcessing.Read(fileStream);
                            }
                            else
                            {
                                JSONProcessing jsonProcessing = new JSONProcessing();
                                data = jsonProcessing.Read(fileStream);
                            }
                            SortingAndFiltering sortingAndFiltering = new SortingAndFiltering();
                            List<Attraction> new_data = sortingAndFiltering.ToFilter(data, message.Text!, "Geoarea");
                            DataOfUsers[chatId] = new_data;
                        }
                        ReplyKeyboardMarkup replyKeyboardMarkup = new(new[] {
                        new KeyboardButton[] { "1. Выборка по полю AdmArea" },
                        new KeyboardButton[] { "2. Выборка по полю Geoarea" },
                        new KeyboardButton[] { "3. Выборка по полям District и geoarea" },
                        new KeyboardButton[] { "4. Отсортировать по полю Name по возрастанию" },
                        new KeyboardButton[] { "5. Отсортировать по полю Name по убыванию" },
                        new KeyboardButton[] { "6. Скачать файл в формате CSV" },
                        new KeyboardButton[] { "7. Скачать файл в формате JSON" },
                        new KeyboardButton[] { "8. Закончить работу с файлом" },
                    })
                        {
                            ResizeKeyboard = true
                        };
                        ConditionsOfUsers.Remove(chatId);
                        ConditionsOfUsers[chatId] = "Выбрал следующее действие";
                        Message sentMessage = await botClient.SendTextMessageAsync(
                            chatId: chatId,
                            text: "Готово! Выберите следующее желаемое действие",
                            replyMarkup: replyKeyboardMarkup,
                            cancellationToken: cancellationToken);
                    }
                    else if (ConditionsOfUsers.ContainsKey(chatId) && ConditionsOfUsers[chatId] == "Отправил значения District и geoarea")
                    {
                        // В общем и целом, все аналогично, только теперь вызываем ToFilter.
                        using (FileStream fileStream = new FileStream(FilesOfUsers[chatId], FileMode.Open, FileAccess.Read))
                        {
                            List<Attraction> data = new List<Attraction>();
                            if (Path.GetExtension(FilesOfUsers[chatId]) == ".csv")
                            {
                                data = csvProcessing.Read(fileStream);
                            }
                            else
                            {
                                JSONProcessing jsonProcessing = new JSONProcessing();
                                data = jsonProcessing.Read(fileStream);
                            }
                            SortingAndFiltering sortingAndFiltering = new SortingAndFiltering();
                            List<Attraction> new_data = sortingAndFiltering.ToFilter(data, message.Text!, "District и geoarea");
                            DataOfUsers[chatId] = new_data;
                        }
                        ReplyKeyboardMarkup replyKeyboardMarkup = new(new[] {
                        new KeyboardButton[] { "1. Выборка по полю AdmArea" },
                        new KeyboardButton[] { "2. Выборка по полю Geoarea" },
                        new KeyboardButton[] { "3. Выборка по полям District и geoarea" },
                        new KeyboardButton[] { "4. Отсортировать по полю Name по возрастанию" },
                        new KeyboardButton[] { "5. Отсортировать по полю Name по убыванию" },
                        new KeyboardButton[] { "6. Скачать файл в формате CSV" },
                        new KeyboardButton[] { "7. Скачать файл в формате JSON" },
                        new KeyboardButton[] { "8. Закончить работу с файлом" },
                    })
                        {
                            ResizeKeyboard = true
                        };
                        ConditionsOfUsers.Remove(chatId);
                        ConditionsOfUsers[chatId] = "Выбрал следующее действие";
                        Message sentMessage = await botClient.SendTextMessageAsync(
                            chatId: chatId,
                            text: "Готово! Выберите следующее желаемое действие",
                            replyMarkup: replyKeyboardMarkup,
                            cancellationToken: cancellationToken);
                    }


                    // Здесь уже if'ы по второму и дальнейшим выборам.
                    // Нужно это в отдельный случай, так как нужно сохранять прошлые изменения, то есть не надо там заново считывать данные с файла.
                    // Код здесь почти аналогичен прошлому блоку, да, это можно сделать лучше, чем просто скопировать, но времени было мало.

                    else if (ConditionsOfUsers.ContainsKey(chatId) && ConditionsOfUsers[chatId] == "Выбрал следующее действие" && message.Text == "1. Выборка по полю AdmArea")
                    {
                        ConditionsOfUsers.Remove(chatId);
                        ConditionsOfUsers[chatId] = "Отправил значение AdmArea";
                        Message sentMessage = await botClient.SendTextMessageAsync(
                            chatId: chatId,
                            text: "Введите значение, по которому будет выборка",
                            cancellationToken: cancellationToken);

                    }
                    else if (ConditionsOfUsers.ContainsKey(chatId) && ConditionsOfUsers[chatId] == "Выбрал следующее действие" && message.Text == "2. Выборка по полю Geoarea")
                    {
                        ConditionsOfUsers.Remove(chatId);
                        ConditionsOfUsers[chatId] = "Отправил значение Geoarea";
                        Message sentMessage = await botClient.SendTextMessageAsync(
                            chatId: chatId,
                            text: "Введите значение, по которому будет выборка",
                            cancellationToken: cancellationToken);
                    }
                    else if (ConditionsOfUsers.ContainsKey(chatId) && ConditionsOfUsers[chatId] == "Выбрал следующее действие" && message.Text == "3. Выборка по полям District и geoarea")
                    {
                        ConditionsOfUsers.Remove(chatId);
                        ConditionsOfUsers[chatId] = "Отправил значения District и geoarea";
                        Message sentMessage = await botClient.SendTextMessageAsync(
                            chatId: chatId,
                            text: "Введите значение, по которому будет выборка",
                            cancellationToken: cancellationToken);
                    }
                    else if (ConditionsOfUsers.ContainsKey(chatId) && ConditionsOfUsers[chatId] == "Выбрал следующее действие" && message.Text == "4. Отсортировать по полю Name по возрастанию")
                    {
                        SortingAndFiltering sortingAndFiltering = new SortingAndFiltering();
                        List<Attraction> new_data = sortingAndFiltering.ToSort(DataOfUsers[chatId], "Прямой");
                        DataOfUsers.Remove(chatId);
                        DataOfUsers[chatId] = new_data;
                        ReplyKeyboardMarkup replyKeyboardMarkup = new(new[] {
                        new KeyboardButton[] { "1. Выборка по полю AdmArea" },
                        new KeyboardButton[] { "2. Выборка по полю Geoarea" },
                        new KeyboardButton[] { "3. Выборка по полям District и geoarea" },
                        new KeyboardButton[] { "4. Отсортировать по полю Name по возрастанию" },
                        new KeyboardButton[] { "5. Отсортировать по полю Name по убыванию" },
                        new KeyboardButton[] { "6. Скачать файл в формате CSV" },
                        new KeyboardButton[] { "7. Скачать файл в формате JSON" },
                        new KeyboardButton[] { "8. Закончить работу с файлом" },
                    })
                        {
                            ResizeKeyboard = true
                        };
                        ConditionsOfUsers.Remove(chatId);
                        ConditionsOfUsers[chatId] = "Выбрал следующее действие";
                        Message sentMessage = await botClient.SendTextMessageAsync(
                            chatId: chatId,
                            text: "Готово! Выберите следующее желаемое действие",
                            replyMarkup: replyKeyboardMarkup,
                            cancellationToken: cancellationToken);
                    }
                    else if (ConditionsOfUsers.ContainsKey(chatId) && ConditionsOfUsers[chatId] == "Выбрал следующее действие" && message.Text == "5. Отсортировать по полю Name по убыванию")
                    {
                        SortingAndFiltering sortingAndFiltering = new SortingAndFiltering();
                        List<Attraction> new_data = sortingAndFiltering.ToSort(DataOfUsers[chatId], "Обратный");
                        DataOfUsers.Remove(chatId);
                        DataOfUsers[chatId] = new_data;
                        ReplyKeyboardMarkup replyKeyboardMarkup = new(new[] {
                        new KeyboardButton[] { "1. Выборка по полю AdmArea" },
                        new KeyboardButton[] { "2. Выборка по полю Geoarea" },
                        new KeyboardButton[] { "3. Выборка по полям District и geoarea" },
                        new KeyboardButton[] { "4. Отсортировать по полю Name по возрастанию" },
                        new KeyboardButton[] { "5. Отсортировать по полю Name по убыванию" },
                        new KeyboardButton[] { "6. Скачать файл в формате CSV" },
                        new KeyboardButton[] { "7. Скачать файл в формате JSON" },
                        new KeyboardButton[] { "8. Закончить работу с файлом" },
                    })
                        {
                            ResizeKeyboard = true
                        };
                        ConditionsOfUsers.Remove(chatId);
                        ConditionsOfUsers[chatId] = "Выбрал следующее действие";
                        Message sentMessage = await botClient.SendTextMessageAsync(
                            chatId: chatId,
                            text: "Готово! Выберите следующее желаемое действие",
                            replyMarkup: replyKeyboardMarkup,
                            cancellationToken: cancellationToken);
                    }
                    else if (ConditionsOfUsers.ContainsKey(chatId) && ConditionsOfUsers[chatId] == "Выбрал следующее действие" && message.Text == "6. Скачать файл в формате CSV")
                    {
                        CsvProcessing csvProcessing = new CsvProcessing();
                        string new_name = Path.GetFileNameWithoutExtension(FilesOfUsers[chatId]) + "_new.csv";
                        Stream str = csvProcessing.Write(DataOfUsers[chatId], new_name);
                        using (FileStream fileStream = new FileStream(new_name, FileMode.Open, FileAccess.Read))
                        {
                            Message message1 = await botClient.SendDocumentAsync(
                                chatId: chatId,
                                document: InputFile.FromStream(stream: fileStream, fileName: new_name),
                                caption: "Вот ваш CSV файл. Выберите следующее желаемое действие");
                        }
                    }
                    else if (ConditionsOfUsers.ContainsKey(chatId) && ConditionsOfUsers[chatId] == "Выбрал следующее действие" && message.Text == "7. Скачать файл в формате JSON")
                    {
                        JSONProcessing jsonProcessing = new JSONProcessing();
                        string new_name = Path.GetFileNameWithoutExtension(FilesOfUsers[chatId]) + "_new.json";
                        Stream str = jsonProcessing.Write(DataOfUsers[chatId], new_name);
                        using (FileStream fileStream = new FileStream(new_name, FileMode.Open, FileAccess.Read))
                        {
                            Message message1 = await botClient.SendDocumentAsync(
                                chatId: chatId,
                                document: InputFile.FromStream(stream: fileStream, fileName: new_name),
                                caption: "Вот ваш Json файл. Выберите следующее желаемое действие");
                        }
                    }
                    else if (ConditionsOfUsers.ContainsKey(chatId) && message.Text == "8. Закончить работу с файлом" && ConditionsOfUsers[chatId] == "Выбрал следующее действие")
                    {
                        ConditionsOfUsers.Remove(chatId);
                        FilesOfUsers.Remove(chatId);
                        if (DataOfUsers.ContainsKey(chatId))
                        {
                            DataOfUsers.Remove(chatId);
                        }
                        Message sentMessage = await botClient.SendTextMessageAsync(
                            chatId: chatId,
                            text: "Готово! Скиньте следующий файл, если хотите",
                            cancellationToken: cancellationToken);
                    }
                    else
                    {
                        Message sentMessage = await botClient.SendTextMessageAsync(
                            chatId: chatId,
                            text: "Вы отправили что-то не то. Посмотрите, что я попросил вас отправить",
                            cancellationToken: cancellationToken);
                    }
                }
                catch
                {
                    Message sentMessage = await botClient.SendTextMessageAsync(
                            chatId: chatId,
                            text: "Ошибка. Возможно, вы отправили что-то не то, либо проблемы с файлом.\n" +
                            "Перепроверьте. Можете скопировать файл с задания.",
                            cancellationToken: cancellationToken);
                }

                Console.WriteLine("ok");
            }

            // Обработка ошибок.
            Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
            {
                var ErrorMessage = exception switch
                {
                    ApiRequestException apiRequestException
                        => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
                    _ => exception.ToString()
                };

                Console.WriteLine(ErrorMessage);
                return Task.CompletedTask;
            }

            return Task.CompletedTask;
        }
    }
}
