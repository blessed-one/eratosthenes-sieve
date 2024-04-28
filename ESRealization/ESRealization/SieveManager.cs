using ESContract;
using System.Diagnostics;


public class SieveManager //: ISieveManager<Field, Cell>
{
    public int[] FindPrimes(int n, Field field)
    {
        if (n == 0)
        {
            return new int[0];
        }

        // Поиск базиса простых чисел
        List<int> primeBasis = new();
        for (int i = 2; i <= (int)Math.Pow(n, 0.5); i++)
        {
            bool isPrime = true;

            foreach (int devisor in primeBasis)
            {
                if (i % devisor == 0)
                {
                    isPrime = false; 
                    break;
                }
            }

            if (isPrime) primeBasis.Add(i);
        }
        int basisCount = primeBasis.Count;

        // Создание фильтров под каждое число базиса и связь фильтров в цепочку
        List<SieveFilter> filters = new() { new SieveFilter(field) };
        for (int i = 1; i < basisCount; i++)
        {
            var filter = new SieveFilter(field);
            filters.Add(filter);
            filters[i - 1].NextFilter = filter;
        }
        filters[^1].NextFilter = filters[0];

        // Добавление n чисел и -1 в самый первый фильтр
        for (int i = 2; i <= n; i++)
        {
            filters[0].NumbersQueue.Enqueue(i);
        }
        filters[0].NumbersQueue.Enqueue(-1);

        // Параллель
        Parallel.For(0, basisCount, (n) => filters[n].Operate());

        // Сбор чисел в массив - результат
        List<int> result = new();
        foreach (var filter in filters)
        {
            result.AddRange(filter.GetNumbers());
        }
        result.Remove(-1);
        result.Sort();

        return result.ToArray();
    }
}