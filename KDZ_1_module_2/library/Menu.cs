namespace WorkWithCsvLibrary
{
    public static class Menu
    {
        /// <summary>
        /// Метод MenuOfProg - главное меню программы.
        /// </summary>
        public static int MenuOfProg(string[] DataFromCSV)
        {
            OtherMethods.ChooseOption();
            string? option = Console.ReadLine();
            if (option == "1")
            {
                Console.WriteLine("Введите конкретное значение AdmArea, по которому будет выборка");
                Console.WriteLine("(Вводить внимательно, без кавычек)");
                // Так как в файле всё в кавычках, то добавляем их в строку-значение.
                //  Аналогично и в других if'ах.
                string? Area = "\"" + Console.ReadLine() + "\"";
                DataProcessing.AdmArea(DataFromCSV, Area);
                return 1;
            }
            else if (option == "2")
            {
                Console.WriteLine("Введите конкретное значение geoarea, по которому будет выборка");
                Console.WriteLine("(Вводить внимательно, без кавычек)");
                string? Area = "\"" + Console.ReadLine() + "\"";
                DataProcessing.geoarea(DataFromCSV, Area);
                return 1;
            }
            else if (option == "3")
            {
                Console.WriteLine("Введите конкретные два значения Distict и geoarea, по которым будет выборка (через enter");
                Console.WriteLine("(Вводить внимательно, без кавычек)");
                string? District = "\"" + Console.ReadLine() + "\"";
                string? Area = "\"" + Console.ReadLine() + "\"";
                DataProcessing.DistrAndGeoAr(DataFromCSV, Area, District);
                return 1;
            }
            else if (option == "4")
            {
                Console.WriteLine("Вывод отсортированных данных файла в прямом порядке");
                DataProcessing.NameDirect(DataFromCSV);
                return 1;
            }
            else if (option == "5")
            {
                Console.WriteLine("Вывод отсортированных данных файла в обратном порядке");
                DataProcessing.NameReverse(DataFromCSV);
                return 1;
            }
            else if (option == "6")
            {
                // Выход из программы.
                return 0;
            }
            else
            {
                Console.WriteLine("Похоже, вы ввели что-то не то.");
                return 1;
            }
        }
    }
}
