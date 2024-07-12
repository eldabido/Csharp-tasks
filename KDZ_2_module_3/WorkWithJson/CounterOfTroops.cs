namespace WorkWithJson
{
    /// <summary>
    /// Класс CounterOfTroops - класс, подписанный на событие о смене локации какого-то войска.
    /// </summary>
    public class CounterOfTroops
    {
        /// <summary>
        /// Конструктор по умолчанию.
        /// </summary>
        public CounterOfTroops()
        {
        }
        /// <summary>
        /// Метод HandleCounter - метод, срабатывающий при смене локации войска (событии).
        /// </summary>
        public Hero[] HandleCounter(Hero[] heroes, int num)
        {
            int count = 0;
            // Считаем параметр troops, num - порядковый номер героя в коллекции.
            // Если в одной локации, то прибавляем к troops quantity - количество солдат.
            for (int i = 0; i < heroes.Length; i++)
            {
                for (int j = 0; j < heroes[i].Units.Length; j++)
                if (heroes[i].Units[j].Current_location == heroes[num].Castle_location) 
                {
                    count += heroes[i].Units[j].Quantity;
                }
            }
            // Меняем параметр troops.
            heroes[num].Troops = count;
            Console.WriteLine("Произошло событие! Изменилась локация войска. Пересчитан параметр troops.");
            return heroes;
        }
    }
}
