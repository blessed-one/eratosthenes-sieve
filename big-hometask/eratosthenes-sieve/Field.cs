public class Field
{
    private int _cellsCount;
    private int[,] _matrix;
    private (int X, int Y) _size;

    public Cell[,] CellField { get; set; }
    public Field(int cellsCount)
    {
        _cellsCount = cellsCount;
        _size = CalculateSize();
        _matrix = new int[_size.X, _size.Y];
        CellField = new Cell[_size.X, _size.Y]; ;
    }

    private (int X, int Y) CalculateSize()
    {
        int x = _cellsCount, y = 1;

        for (int i = 1; i < (int)Math.Pow(_cellsCount, 0.5d) + 1; i++)
        {
            if (_cellsCount % i == 0)
            {
                x = _cellsCount % i;
                y = i;
            }
        }

        return (x, y);
    }

    public (int X, int Y) GetIndex(int number)
    {
        int n = number - 1;
        int x, y;

        x = n / _size.X;
        y = n % _size.X;

        return (x, y);
    }

    public Cell GetCell(int number)
    {
        var position = GetIndex(number);

        return CellField[position.X, position.Y];
    }

    public void Print()
    {
        for (int i = 0; i < _size.X; i++)
        {
            for (int j = 0; j < _size.Y; j++)
            {
                Console.Write(_matrix[i, j]);
            }
            Console.WriteLine();
        }
    }

}
