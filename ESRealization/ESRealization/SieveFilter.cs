using ESContract;
using System.Collections.Concurrent;


public class SieveFilter : IFilter
{
    private readonly Field _field;
    public SieveFilter NextFilter { get; set; }
    public int MainNumber { get; set; }
    public bool IsDone { get; set; }
    public ConcurrentQueue<int> NumbersQueue { get; set; }

    public SieveFilter(Field field)
    {
        _field = field;
        IsDone = false;
        NumbersQueue = new ConcurrentQueue<int>();
        NextFilter = this;
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

                var cell = _field.GetCell(number);

                if (MainNumber == 0)
                {
                    MainNumber = number;

                    cell.UpdateState(State.Good);
                }
                else if (number % MainNumber != 0)
                {
                    cell.UpdateState(State.Good);

                    GiveNextOneNum(number);
                }
                else
                {
                    cell.UpdateState(State.Bad);
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
