namespace BotLibrary
{
    /// <summary>
    /// класс SortingAndFiltering - класс, содержащий методы фильтрации и сортировки.
    /// </summary>
    public class SortingAndFiltering
    {
        /// <summary>
        /// Метод ToFilter - метод, фильтрующий данные по значению.
        /// </summary>

        public List<Attraction> ToFilter(List<Attraction> attractions, string value, string field)
        {
            // Фильтруем с помощтю LINQ запросов в зависимости от поля.
            if (field == "AdmArea")
            {
                var Filtered_attractions = attractions.Where(a => a.AdmArea == value).ToList();
                return Filtered_attractions;
            }
            else if (field == "Geoarea")
            {
                var Filtered_attractions = attractions.Where(a => a.Geoarea == value).ToList();
                return Filtered_attractions;
            }
            else if (field == "District и geoarea")
            {
                string[] values = value.Split(';');
                var Filtered_attractions = attractions.Where(a => a.District == values[0]).ToList();
                Filtered_attractions = Filtered_attractions.Where(a => a.Geoarea == values[1]).ToList();
                return Filtered_attractions;

            }
            else
            {
                return attractions;
            }
        }

        /// <summary>
        /// Метод ToSort - метод, сортирующий данные по полю Name по возрастанию или убыванию.
        /// </summary>
        public List<Attraction> ToSort(List<Attraction> attractions, string row)
        {
            // Сортируем с помощью LINQ запросов в зависимости от выбора порядка сортировки.
            if (row == "Прямой")
            {
                var attractionsSortedByName = attractions.OrderBy(a => a.Name).ToList();
                return attractionsSortedByName;
            }
            else if (row == "Обратный")
            {
                var attractionsSortedByName = attractions.OrderByDescending(a => a.Name).ToList();
                return attractionsSortedByName;
            }
            else
            {
                return attractions;
            }
        }

    }
}
