public class CellCounter
{
    private Field _field;
    private int _startNumber;
    private int _currentNumber;
    public Cell StartCell { get; private set; }
    public Cell CurrentCell { get; private set; }

    

    public CellCounter(int startingNum, Field field)
    {
        _field = field;
        _startNumber = startingNum;
        _currentNumber = startingNum;

        StartCell = _field.GetCell(startingNum);
        CurrentCell = StartCell;

        StartCell.IsStardedWith = true;
        if (!StartCell.IsVisited)
        {
            StartCell.State = State.Good;
        }
    }

    public void Step()
    {
        _currentNumber += _startNumber;
        CurrentCell = _field.GetCell(_currentNumber);

    }
}
