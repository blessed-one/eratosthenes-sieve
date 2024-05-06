using ESContract;
using System.Collections.Concurrent;
using System.Security.Principal;


public class SieveManager : ISieveManager
{
    public ConcurrentQueue<(int, State)> StepsQueue { get; set; }
    public int[] FindPrimes(int n)
    {
        StepsQueue = new ConcurrentQueue<(int, State)>();
        StepsQueue.Enqueue((1, State.Bad));


        if (n == 0 || n == 1)
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
        List<SieveFilter> filters = new() { new SieveFilter(this) };
        for (int i = 1; i < basisCount; i++)
        {
            var filter = new SieveFilter(this);
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
        Parallel.For(0, basisCount == 0 ? 1 : basisCount, (i) => filters[i].Operate());

        // Сбор чисел в массив - результат
        List<int> result = new();
        foreach (var filter in filters)
        {
            result.AddRange(filter.GetNumbers());
        }
        result.Remove(-1);
        result.Remove(0);
        result.Sort();

        return result.ToArray();
    }

    public (int Number, State State)[] GetSteps()
    {
        if (StepsQueue != null)
        {
            return StepsQueue.ToArray();
        }
        else throw new InvalidOperationException("Поиск ещё не был произведён");
    }
}