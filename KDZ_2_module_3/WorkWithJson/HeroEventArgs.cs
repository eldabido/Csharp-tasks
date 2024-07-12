namespace WorkWithJson
{
    /// <summary>
    /// Класс HeroEventArgs - класс, содержащий время изменения поля.
    /// </summary>
    public class HeroEventArgs: EventArgs
    {
        // Авто-свойство с временем изменения.
        public DateTime TimeChanging { get; }
        
        /// <summary>
        /// Конструктор по умолчанию.
        /// </summary>
        public HeroEventArgs()
        {
        }
        /// <summary>
        /// Конструктор, принимающий время изменения.
        /// </summary>
        public HeroEventArgs(DateTime time)
        {
            TimeChanging = time;
        }

    }
}
