namespace WorkWithCsvFile
{
    /// <summary>
    /// Класс School - класс, содержащий информацию о школах.
    /// </summary>
    public class School
    {
        // Создаем поля, названные, как колонки в файле.
        string? _Rownum;
        string? _name;
        string? _okrug;
        string? _rayon;
        string? _form_of_incorporation;
        string? _submission;
        string? _tip_uchrezhdeniya;
        string? _vid_uchrezhdeniya;
        string? _X;
        string? _Y;
        string? _global_id;
        Contacts _contact;

        // Конструктор по умолчанию.
        public School() { }

        // Конструктор.
        public School(string Data)
        {
            // Так как порядковый номер без кавычек, идём до первой ; и записываем в поле номер.
            int i = 0;
            string? str = "";
            while (Data[i] != ';')
            {
                str += Data[i++];
            }
            i++;
            _Rownum = str;

            // Получаем остальные поля. Где-то приходится применять несколько раз, так как
            // некоторые поля нужно пропустить, они пойдут в _contact. 
            str = GetField(Data, ref i);
            _name = str;
            str = GetField(Data, ref i);
            str = GetField(Data, ref i);
            _okrug = str;
            str = GetField(Data, ref i);
            _rayon = str;
            str = GetField(Data, ref i);
            _form_of_incorporation = str;
            str = GetField(Data, ref i);
            _submission = str;
            str = GetField(Data, ref i);
            _tip_uchrezhdeniya = str;
            str = GetField(Data, ref i);
            _vid_uchrezhdeniya = str;
            str = GetField(Data, ref i);
            str = GetField(Data, ref i);
            str = GetField(Data, ref i);
            str = GetField(Data, ref i);
            _X = str;
            str = GetField(Data, ref i);
            _Y = str;
            str = GetField(Data, ref i);
            _global_id = str;

            // Инициализируем _contact.
            _contact = new Contacts(Data);
        }

        // Свойство-геттер для _form_of_incorporation.
        public string GetForm
        {
            get { return _form_of_incorporation!; }
        }

        // Свойство-геттер для _submission.
        public string GetSubmission
        {
            get { return _submission!; }
        }

        // Свойство-геттер для _okrug.
        public string GetOkrug
        {
            get { return _okrug!; }
        }
        /// <summary>
        /// Метод GetField - для считывания значения поля.
        /// </summary>
        static string GetField(string Field, ref int i)
        {
            string str = "";
            // Идем, пока не встретим комбинацию ";, это означает, что значение получено полностью.
            while (Field[i] != '\"' || Field[i + 1] != ';')
            {
                str += Field[i++];
            }
            // Записываем кавычку.
            str += Field[i++];
            // Для прохода дальше.
            i++;
            return str;
        }

        /// <summary>
        /// Метод ToString - переопределённый ToString для вывода данных.
        /// </summary>
        public override string ToString()
        {
            return $"Rownum = {_Rownum};\nname = {_name};\nokrug = {_okrug};\nrayon = {_rayon};\n" +
                $"_form_of_incorporation = {_form_of_incorporation};\n" +
                $"_submission = {_submission};\n_tip_uchrezhdeniya" +
                $"= {_tip_uchrezhdeniya};\n_vid_uchrezhdeniya = {_vid_uchrezhdeniya};\n" +
                $"_X = {_X};\n_Y = {_Y};\nglobal_id = {_global_id};\ncontacts: {_contact.ToString()}";
        }

        /// <summary>
        /// Метод GetStringForFile - для получения строки формата, как в файле.
        /// </summary>
        public string GetStringForFile()
        {
            return $"{_Rownum};{_name};{_contact.GetAddress};{_okrug};{_rayon};" +
                $"{_form_of_incorporation};{_submission};{_tip_uchrezhdeniya};{_vid_uchrezhdeniya};" +
                $";{_X};{_Y};{_global_id};{_contact.GetTelephone};{_contact.GetWebsite};" +
                $"{_contact.GetEmail};";
        }
    }

    /// <summary>
    /// Структура Contacts - содержит адрес, телефон, веб-сайт, почту.
    /// </summary>
    public struct Contacts
    {
        // Поля.
        string? _adress;
        string? _telephone;
        string? _web_site;
        string? _e_mail;

        // Свойства-геттеры для всех полей.
        public string GetAddress
        {
            get { return _adress!; }
        }
        public string GetTelephone
        {
            get { return _telephone!; }
        }
        public string GetWebsite
        {
            get { return _web_site!; }
        }
        public string GetEmail
        {
            get { return _e_mail!; }
        }

        // Конструктор.
        public Contacts(string Data) 
        {
            int i = 0;
            string? str = "";
            // Пропускаем Rownum.
            while (Data[i] != ';')
            {
                str += Data[i++];
            }
            i++;
            // Пропускаем поля для получения нужного.
            str = GetField(Data, ref i);
            str = GetField(Data, ref i);
            _adress = str;

            // Пропускаем ещё поля для получения следующих.
            for (int j = 0; j < 13; j++)
            {
                str = GetField(Data, ref i);
                j++;
            }
            _telephone = str;
            str = GetField(Data, ref i);
            _web_site = str;
            str = GetField(Data, ref i);
            _e_mail = str;
        }

        /// <summary>
        /// Метод GetField - для считывания значения поля.
        /// </summary>
        static string GetField(string Field, ref int i)
        {
            // Работает аналогично методу из School.
            string str = "";
            while (Field[i] != '\"' || Field[i + 1] != ';')
            {
                str += Field[i++];
            }
            str += Field[i++];
            i++;
            return str;
        }

        /// <summary>
        /// Метод ToString - переопределённый ToString для вывода данных.
        /// </summary>
        public override string ToString()
        {
            return $"address = {_adress};\nTelephone = {_telephone};\nWeb_site = {_web_site};\n" +
                $"e_mail = {_e_mail};\n";
        }

    }

    
}
