namespace WorkWithCsvFile
{
    public class SortAndFilter
    {
        /// <summary>
        /// FilterForm - Метод, фильтрующий по form_of_corporation данные Data.
        /// </summary>
        public static School[] FilterForm(School[] Data, string field)
        {
            // Создаем массив для итога.
            School[] Filtered = new School[Data.Length];
            int count = 0;
            // Проходимся и получаем то, что нужно.
            foreach (School school in Data)
            {
                if (school.GetForm == field)
                {
                    Filtered[count++] = school;
                    Console.WriteLine(school.ToString());
                    Console.WriteLine("-----------------------------------");
                }
            }
            return Filtered[..count];
        }
        /// <summary>
        /// FilterSubmission - Метод, фильтрующий по submission данные Data.
        /// </summary>
        public static School[] FilterSubmission(School[] Data, string field)
        {
            // Создаем массив для итога.
            School[] Filtered = new School[Data.Length];
            int count = 0;
            // Проходимся и получаем то, что нужно.
            foreach (School school in Data)
            {
                if (school.GetSubmission == field)
                {
                    Filtered[count++] = school;
                    Console.WriteLine(school.ToString());
                    Console.WriteLine("-----------------------------------");
                }
            }
            return Filtered[..count];
        }

        /// <summary>
        /// Метод PrintFirstN - печать первых N элементов.
        /// </summary>
        public static School[] PrintFirstN(School[] Data, int N)
        {
            School[] Filtered = new School[N];
            for (int i = 0; i < N; i++)
            {
                Filtered[i] = Data[i];
                Console.WriteLine(Data[i].ToString());
                Console.WriteLine("-----------------------------------");
            }
            return Filtered;
        }

        /// <summary>
        /// Метод PrintLastN - печать последних N элементов.
        /// </summary>
        public static School[] PrintLastN(School[] Data, int N)
        {
            School[] Filtered = new School[N];
            int ind = 0;
            for (int i = Data.Length - N; i < Data.Length; i++)
            {
                Filtered[ind++] = Data[i];
                Console.WriteLine(Data[i].ToString());
                Console.WriteLine("-----------------------------------");
            }
            return Filtered;
        }

        /// <summary>
        /// Метод SortByOkrug - сортировка по полю okrug.
        /// </summary>
        public static School[] SortByOkrug(School[] Data, string ans)
        {
            // По возрастанию. Сортировка пузырьком.
            if (ans == "1")
            {
                for (int i = 0; i < Data.Length; i++)
                {
                    for (int j = 0; j < Data.Length - 1 - i; j++)
                    {
                        if (String.Compare(Data[j].GetOkrug, Data[j + 1].GetOkrug) > 0)
                        {
                            School tmp = Data[j];
                            Data[j] = Data[j + 1];
                            Data[j + 1] = tmp;
                        }
                    }
                }
            }
            // По убыванию. Сортировка пузырьком.
            else
            {
                for (int i = 0; i < Data.Length; i++)
                {
                    for (int j = 0; j < Data.Length - 1 - i; j++)
                    {
                        if (String.Compare(Data[j].GetOkrug, Data[j + 1].GetOkrug) < 0)
                        {
                            School tmp = Data[j];
                            Data[j] = Data[j + 1];
                            Data[j + 1] = tmp;
                        }
                    }
                }

            }

            // Вывод данных.
            for (int i = 0; i < Data.Length; i++)
            {
                Console.WriteLine(Data[i].ToString());
                Console.WriteLine("-----------------------------------");
            }
            return Data;
        }
    }
}