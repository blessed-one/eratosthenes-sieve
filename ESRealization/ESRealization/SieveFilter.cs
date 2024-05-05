using ESContract;
using System.Collections.Concurrent;


public class SieveFilter : IFilter
{
    public SieveManager Manager { get; private set; }
    public SieveFilter NextFilter { get; set; }
    public int MainNumber { get; set; }
    public bool IsDone { get; set; }
    public ConcurrentQueue<int> NumbersQueue { get; set; }

    public SieveFilter(SieveManager manager)
    {
        NumbersQueue = new ConcurrentQueue<int>();
        NextFilter = this;
        Manager = manager;
    }

    public void Operate()
    {
        while (!IsDone)
        {
            if (NumbersQueue.TryDequeue(out int number))
            {
                if (number == -1)
                {
                    GiveNextOneNum(number);
                    IsDone = true;
                    break;
                }

                if (MainNumber == 0)
                {
                    MainNumber = number;

                    Manager.StepsQueue.Enqueue((number, State.Good));
                }
                else if (number % MainNumber != 0)
                {
                    Manager.StepsQueue.Enqueue((number, State.Good));

                    GiveNextOneNum(number);
                }
                else
                {
                    Manager.StepsQueue.Enqueue((number, State.Bad));
                }
            }
        }
    }

    public void GiveNextOneNum(int num)
    {
        try
        {
            NextFilter.NumbersQueue.Enqueue(num);
        }
        catch
        {
            Console.WriteLine($"{MainNumber} -> {NextFilter.MainNumber}   {num}");
        }
    }

    public List<int> GetNumbers()
    {
        var result = NumbersQueue.ToList();
        result.Add(MainNumber);

        return result;
    }
}
