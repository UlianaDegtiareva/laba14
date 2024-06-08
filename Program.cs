using ClassLibraryLaba10;
using ClassLibraryHash_Table;
using System.Drawing;


namespace laba14
{
    public class Program
    {
        static void Main()
        {
            int answer;
            do
            {
                Console.WriteLine();
                Console.WriteLine("НЕОБХОДИМО ВЫБРАТЬ ЧАСТЬ РАБОТЫ---");
                Console.WriteLine("1. Часть №1");
                Console.WriteLine("2. Часть №2");
                Console.WriteLine("3. Выход");
                Console.WriteLine();
                answer = InputAnswer();
                Console.WriteLine();
                switch (answer)
                {
                    case 1:
                        Console.WriteLine("Перед продолжением необходимо создать поселок");
                        Dictionary<string, object> village = CreateVillage();
                        WorkWithPart1(village, "ЧАСТЬ №1");
                        break;
                    case 2:
                        Console.WriteLine("Перед продолжением необходимо создать хеш-таблицу");
                        MyCollection_HashTable<Plants> plants = CreateHashTable();
                        WorkWithPart2(plants, "ЧАСТЬ №2");
                        break;
                    case 3:
                        break;
                    default:
                        Console.WriteLine("Неправильно задан пункт меню");
                        break;
                }
            } while (answer != 3);
            Console.ReadLine();
            
        }

        public static MyCollection_HashTable<Plants> CreateHashTable()
        {
            Console.WriteLine("Введите размер хеш-таблицы:");
            int hashTableSize;
            bool ok;
            do
            {
                string buf = Console.ReadLine();
                ok = int.TryParse(buf, out hashTableSize);
                if (!ok || hashTableSize < 0)
                {
                    Console.WriteLine("Некорректный ввод. Повторите ввод");
                }
            } while (!ok || hashTableSize < 0);

            MyCollection_HashTable<Plants> plants = new MyCollection_HashTable<Plants>(hashTableSize, 0.72);

            for (int i = 0; i < hashTableSize; i++)
            {
                var plant = CreateRandomPlant();
                plants.Add(plant);
            }

            return plants;
        }


        public static Dictionary<string, object> CreateVillage()
        {
            Dictionary<string, object> village = new Dictionary<string, object>();

            Console.WriteLine("Введите количество садов в поселке:");
            int mainsize;
            bool Ok;
            do
            {
                string buf = Console.ReadLine();
                Ok = int.TryParse(buf, out mainsize);
                if (!Ok || (mainsize < 0))
                {
                    Console.WriteLine("Некорректный ввод. Повторите ввод");
                }
            } while (!Ok || (mainsize < 0));

            for (int i = 0; i < mainsize; i++)
            {
                Queue<Plants> garden = new Queue<Plants>();

                Console.Write($"Введите количество растений в саду №{i + 1}: ");
                int gardenSize;
                bool ok;
                do
                {
                    string buf = Console.ReadLine();
                    ok = int.TryParse(buf, out gardenSize);
                    if (!ok || (gardenSize <= 0))
                    {
                        Console.WriteLine("Некорректный ввод. Повторите ввод");
                    }
                } while (!ok || (gardenSize <= 0));

                for (int j = 0; j < gardenSize; j++)
                {
                    var plant = CreateRandomPlant();
                    garden.Enqueue(plant);
                }

                if (i == 0 || i == 1)
                {
                    Plants commonPlant = new Plants("Роза", "Фиолетовая");
                    garden.Enqueue(commonPlant);
                }

                village.Add($"Сад №{i + 1}", garden);
            }

            return village;
        }

        static void WorkWithPart1(Dictionary<string, object> village, string str)
        {
            int ans;
            do
            {
                Console.WriteLine();
                Console.WriteLine($"{str}-ЗАПРОСЫ:");
                Console.WriteLine("1. На выборку данных (Where)");
                Console.WriteLine("2. Использование операций над множествами (Intersect)");
                Console.WriteLine("3. Агрегирование данных (Average)");
                Console.WriteLine("4. Группировка данных (Group by)");
                Console.WriteLine("5. Соединение (Join)");
                Console.WriteLine("6. Печать");
                Console.WriteLine("7. Назад");
                ans = InputAnswer();
                Console.WriteLine();
                switch (ans)
                {
                    case 1:
                        if (village.Count == 0)
                            Console.WriteLine("В поселке нет садов, невозможно выполнить запрос");
                        else
                        {
                            var linqResult = WhereTreesLinq(village);
                            var extResult = WhereTreesExtensionMethods(village);
                            PrintQueryResults("Деревья в садах (LINQ):", linqResult);
                            PrintQueryResults("Деревья в садах (методы расширения):", extResult);
                        }
                        break;
                    case 2:
                        if (village.Count == 0)
                            Console.WriteLine("В поселке нет садов, невозможно выполнить запрос");
                        else
                        {
                            var linqResult = IntersectLINQ(village);
                            var extResult = IntersectExtensionMethods(village);
                            PrintQueryResults("Общие элементы между Садом №1 и Садом №2 (LINQ):", linqResult);
                            PrintQueryResults("Общие элементы между Садом №1 и Садом №2 (методы расширения):", extResult);
                        }
                        break;
                    case 3:
                        if (village.Count == 0)
                            Console.WriteLine("В поселке нет садов, невозможно выполнить запрос");
                        else
                        {
                            var linqResult = AggregateFunctionsLinq(village);
                            var extResult = AggregateFunctionsExtensionMethods(village);
                            Console.WriteLine($"Средний размер сада (LINQ): {linqResult}");
                            Console.WriteLine($"Средний размер сада (методы расширения): {extResult}");
                        }
                        break;
                    case 4:
                        if (village.Count == 0)
                            Console.WriteLine("В поселке нет садов, невозможно выполнить запрос");
                        else
                        {
                            var linqResult = GroupByLinq(village);
                            var extResult = GroupByExtensionMethods(village);
                            PrintGroupByResults("Группировка по размеру садов (LINQ):", linqResult);
                            PrintGroupByResults("Группировка по размеру садов (методы расширения):", extResult);
                        }
                        break;
                    case 5:
                        if (village.Count == 0)
                            Console.WriteLine("В поселке нет садов, невозможно выполнить запрос");
                        else
                        {
                            Queue<Thorn> thorns = new Queue<Thorn>();
                            Thorn th1 = new Thorn("Маленькие", 2, true);
                            thorns.Enqueue(th1);
                            var linqResult = JoinQuery1(village, thorns);
                            var extResult = JoinQuery2(village, thorns);
                            Console.WriteLine("Розы с видами шипов (LINQ):");
                            foreach (var item in linqResult)
                            {
                                Console.WriteLine(item);
                            }
                            Console.WriteLine("Розы с видами шипов (метод расширения):");
                            foreach (var item in extResult)
                            {
                                Console.WriteLine(item);
                            }
                        }
                        break;
                    case 6:
                        if (village.Count == 0)
                            Console.WriteLine("Поселок пуст");
                        else
                        {
                            Console.WriteLine("ПОСЕЛОК");
                            PrintCollection(village);
                        }
                        break;
                    case 7:
                        break;
                }
            } while (ans != 7);
        }

        static void WorkWithPart2(MyCollection_HashTable<Plants> plants, string str)
        {
            int ans;
            do
            {
                Console.WriteLine();
                Console.WriteLine($"{str}-ЗАПРОСЫ:");
                Console.WriteLine("1. На выборку данных (Where)");
                Console.WriteLine("2. Получение счетчика (Count)");
                Console.WriteLine("3. Агрегирование данных (Average)");
                Console.WriteLine("4. Группировка данных (Group by)");
                Console.WriteLine("5. Печать");
                Console.WriteLine("6. Назад");
                ans = InputAnswer();
                Console.WriteLine();
                switch (ans)
                {
                    case 1:
                        if (plants.Count == 0)
                            Console.WriteLine("Коллекция пуста, невозможно выполнить запрос");
                        else
                        {
                            var linqResult = WhereQueryLINQ(plants);
                            var extResult = WhereQueryMethod(plants);
                            PrintQueryResults("Розы в коллекции (LINQ):", linqResult);
                            PrintQueryResults("Розы в коллекции (методы расширения):", extResult);
                        }
                        break;
                    case 2:
                        if (plants.Count == 0)
                            Console.WriteLine("Коллекция пуста, невозможно выполнить запрос");
                        else
                        {
                            var linqResult = CountQueryLINQ(plants);
                            var extResult = CountQueryMethod(plants);
                            Console.WriteLine($"Количество элементов в коллекции (LINQ): {linqResult}");
                            Console.WriteLine($"Количество элементов в коллекции (методы расширения): {extResult}");
                        }
                        break;
                    case 3:
                        if (plants.Count == 0)
                            Console.WriteLine("Коллекция пуста, невозможно выполнить запрос");
                        else
                        {
                            var linqResult = GroupByQueryLINQ(plants);
                            var extResult = GroupByQueryMethod(plants);
                            PrintGroupByResults("Группировка по типу растений (LINQ):", linqResult);
                            PrintGroupByResults("Группировка по типу растений (методы расширения):", extResult);
                        }
                        break;
                    case 4:
                        if (plants.Count == 0)
                            Console.WriteLine("Коллекция пуста, невозможно выполнить запрос");
                        else
                        {
                            var linqResult = AverageQueryLINQ(plants);
                            var extResult = AverageQueryExtension(plants);
                            Console.WriteLine($"Средняя высота деревьев (LINQ): {linqResult}");
                            Console.WriteLine($"Средняя высота деревьев (методы расширения): {extResult}");
                        }
                        break;
                    case 5:
                        if (plants.Count == 0)
                            Console.WriteLine("Таблица пуста");
                        else
                        {
                            plants.Print();
                        }
                        break;
                    case 6:
                        break;
                }
            } while (ans != 6);
        }

        static int InputAnswer()
        {
            int answer;
            bool Ok;
            do
            {
                string buf = Console.ReadLine();
                Ok = int.TryParse(buf, out answer);
                if (!Ok)
                {
                    Console.WriteLine("Неправильно выбран пункт меню. Повторите ввод");
                }
            } while (!Ok);
            return answer;
        }

        static Random rand = new Random();

        public static Plants CreateRandomPlant()
        {
            switch (rand.Next(4))
            {
                case 0:
                    var plant = new Plants();
                    plant.RandomInit();
                    return plant;
                case 1:
                    var tree = new Trees();
                    tree.RandomInit();
                    return tree;
                case 2:
                    var flower = new Flowers();
                    flower.RandomInit();
                    return flower;
                case 3:
                    var rose = new Rose();
                    rose.RandomInit();
                    return rose;
                default:
                    return new Plants();
            }
        }

        static void PrintCollectionItem(KeyValuePair<string, object> element)
        {
            Console.WriteLine($"Ключ: {element.Key}");

            Console.WriteLine("Значение: ");
            if (element.Value is Queue<Plants> gardenValue)
            {
                foreach (var plant in gardenValue)
                {
                    // Вывод элементов из очереди
                    Console.WriteLine(plant);
                }
            }
        }

        public static void PrintCollection(Dictionary<string, object> collection)
        {
            foreach (var element in collection)
            {
                PrintCollectionItem(element);
            }
        }

        // Метод выборки деревьев с использованием LINQ
        public static IEnumerable<Plants> WhereTreesLinq(Dictionary<string, object> village)
        {
            return from element in village
                   where element.Value is Queue<Plants>
                   from plant in (element.Value as Queue<Plants>)
                   where plant is Trees
                   select plant;
        }

        // Метод выборки деревьев с использованием методов расширения
        public static IEnumerable<Plants> WhereTreesExtensionMethods(Dictionary<string, object> village)
        {
            return village.Where(element => element.Value is Queue<Plants>)
                          .SelectMany(element => element.Value as Queue<Plants>)
                          .Where(plant => plant is Trees);
        }

        // Метод пересечения садов с использованием методов расширения
        public static IEnumerable<Plants> IntersectExtensionMethods(Dictionary<string, object> village)
        {
            var garden1 = (Queue<Plants>)village["Сад №1"];
            var garden2 = (Queue<Plants>)village["Сад №2"];

            return garden1.Intersect(garden2);
        }

        // Метод пересечения садов с использованием LINQ
        public static IEnumerable<Plants> IntersectLINQ(Dictionary<string, object> village)
        {
            var garden1 = (Queue<Plants>)village["Сад №1"];
            var garden2 = (Queue<Plants>)village["Сад №2"];

            return (from plant in garden1 select plant).Intersect(garden2);
        }

        // Метод для получения среднего количества растений в садах с использованием LINQ
        public static double AggregateFunctionsLinq(Dictionary<string, object> village)
        {
            return (from element in village
                    let garden = element.Value as Queue<Plants>
                    where garden != null
                    select garden.Count).Average();
        }

        // Метод для получения среднего количества растений в садах с использованием методов расширения
        public static double AggregateFunctionsExtensionMethods(Dictionary<string, object> village)
        {
            return village.Where(element => element.Key.StartsWith("Сад"))
                          .Select(element => element.Value as Queue<Plants>)
                          .Select(queue => queue.Count)
                          .Average();
        }

        // Метод для группировки садов по количеству растений с использованием LINQ
        public static Dictionary<int, List<KeyValuePair<string, object>>> GroupByLinq(Dictionary<string, object> village)
        {
            return (from element in village
                    group element by ((Queue<Plants>)element.Value).Count into gardenGroup
                    select gardenGroup).ToDictionary(g => g.Key, g => g.ToList());
        }

        // Метод для группировки садов по количеству растений с использованием методов расширения
        public static Dictionary<int, List<KeyValuePair<string, object>>> GroupByExtensionMethods(Dictionary<string, object> village)
        {
            return village.Where(element => element.Key.StartsWith("Сад"))
                          .GroupBy(element => ((Queue<Plants>)element.Value).Count)
                          .ToDictionary(g => g.Key, g => g.ToList());
        }

        // Метод для соединения данных о розах и шипах с использованием методов расширения
        public static IEnumerable<dynamic> JoinQuery2(Dictionary<string, object> village, Queue<Thorn> thorns)
        {
            return village.SelectMany(kvp => kvp.Value as Queue<Plants>)
                          .OfType<Rose>()
                          .Select(plant =>
                          {
                              var thorn = thorns.FirstOrDefault(th => th.Thorns == plant.Thorns);
                              return new
                              {
                                  Name = plant.Name,
                                  Color = plant.Color,
                                  Flavor = plant.Flavor,
                                  Thorn = thorn != null ? plant.Thorns + "-" + thorn.Form : "No Thorn",
                                  Size = thorn != null ? thorn.Size : 0
                              };
                          });
        }

        // Метод для соединения данных о розах и шипах с использованием LINQ
        public static IEnumerable<dynamic> JoinQuery1(Dictionary<string, object> village, Queue<Thorn> thorns)
        {
            return from kvp in village
                   from plant in kvp.Value as Queue<Plants>
                   where plant is Rose
                   let rose = plant as Rose
                   let thorn = thorns.FirstOrDefault(th => th.Thorns == rose.Thorns)
                   select new
                   {
                       Name = rose.Name,
                       Color = rose.Color,
                       Flavor = rose.Flavor,
                       Thorn = thorn != null ? rose.Thorns + "-" + thorn.Form : "No Thorn",
                       Size = thorn != null ? thorn.Size : 0
                   };
        }

        // Функции для MyCollection_HashTable<Plants>

        // Метод для подсчета количества растений с использованием LINQ
        public static int CountQueryLINQ(MyCollection_HashTable<Plants> plants)
        {
            return (from plant in plants
                    where plant != null && !string.IsNullOrEmpty(plant.Name)
                    select plant).Count();
        }

        // Метод для группировки растений по типу с использованием LINQ
        public static int CountQueryMethod(MyCollection_HashTable<Plants> plants)
        {
            return plants.Count(plant => plant != null && !string.IsNullOrEmpty(plant.Name));
        }

        // Метод для группировки растений по типу с использованием методов расширения
        public static Dictionary<string, List<Plants>> GroupByQueryLINQ(MyCollection_HashTable<Plants> plants)
        {
            return (from plant in plants
                    where plant != null && !string.IsNullOrEmpty(plant.Name)
                    group plant by plant.GetType().Name)
                   .ToDictionary(g => g.Key, g => g.ToList());
        }

        // Метод для вычисления средней высоты деревьев с использованием LINQ
        public static Dictionary<string, List<Plants>> GroupByQueryMethod(MyCollection_HashTable<Plants> plants)
        {
            return plants.Where(plant => plant != null && !string.IsNullOrEmpty(plant.Name))
                         .GroupBy(plant => plant.GetType().Name)
                         .ToDictionary(g => g.Key, g => g.ToList());
        }

        // Метод для вычисления средней высоты деревьев с использованием методов расширения
        public static double AverageQueryLINQ(MyCollection_HashTable<Plants> plants)
        {
            var average = (from plant in plants
                           where plant is Trees
                           select ((Trees)plant).Height)
                          .DefaultIfEmpty()
                          .Average();

            return average;
        }

        public static double AverageQueryExtension(MyCollection_HashTable<Plants> plants)
        {
            var averageHeight = plants.Where(plant => plant is Trees)
                                      .Select(plant => ((Trees)plant).Height)
                                      .DefaultIfEmpty()
                                      .Average();

            return averageHeight;
        }

        // Метод для выборки растений, содержащих 'Роза' в имени, с использованием LINQ
        public static IEnumerable<Plants> WhereQueryLINQ(MyCollection_HashTable<Plants> plants)
        {
            var queryResult = from plant in plants
                              where plant != null && !string.IsNullOrEmpty(plant.Name) && plant.Name.Contains("Роза")
                              select plant;

            return queryResult.Any() ? queryResult : Enumerable.Empty<Plants>();
        }

        // Метод для выборки растений, содержащих 'Роза' в имени, с использованием методов расширения
        public static IEnumerable<Plants> WhereQueryMethod(MyCollection_HashTable<Plants> plants)
        {
            var methodResult = plants.Where(plant => plant != null && !string.IsNullOrEmpty(plant.Name) && plant.Name.Contains("Роза"));

            return methodResult.Any() ? methodResult : Enumerable.Empty<Plants>();
        }

        // Метод для печати результатов группировки по количеству растений в садах
        public static void PrintGroupByResults(string title, Dictionary<int, List<KeyValuePair<string, object>>> groups)
        {
            Console.WriteLine(title);
            foreach (var group in groups)
            {
                Console.WriteLine($"Размер сада: {group.Key}");
                foreach (var element in group.Value)
                {
                    PrintCollectionItem(element);
                }
                Console.WriteLine($"Количество садов в группе: {group.Value.Count}");
                Console.WriteLine();
            }
        }

        // Метод для печати результатов группировки по типу растений
        public static void PrintGroupByResults(string title, Dictionary<string, List<Plants>> groups)
        {
            Console.WriteLine(title);
            foreach (var group in groups)
            {
                Console.WriteLine($"Тип: {group.Key}");
                foreach (var plant in group.Value)
                {
                    Console.WriteLine(plant);
                }
            }
        }

        // Метод для печати результатов выборки растений
        public static void PrintQueryResults(string title, IEnumerable<Plants> results)
        {
            Console.WriteLine(title);
            if (results.Any())
            {
                foreach (var result in results)
                {
                    Console.WriteLine(result);
                }
            }
            else
            {
                Console.WriteLine("В коллекции нет растений, содержащих 'Роза' в имени.");
            }
        }
    }
}
