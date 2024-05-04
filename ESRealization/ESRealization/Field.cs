using ESContract;


public class Field : IField
{
    public int Size { get; init; }
    public int CellsCount { get; init; }
    public Cell[,] CellField { get; init; }
    public Field(int cellsCount)
    {
        CellsCount = cellsCount;
        Size = CalculateSize();
        CellField = new Cell[Size, Size];
        for (int i = 0; i < Size; i++)
        {
            for (int j = 0; j < Size; j++)
            {
                CellField[i, j] = new Cell();
            }
        }
    }

    /// <summary>
    /// Вычисляет оптимальную размерность матрицы по заданному числу клеток
    /// </summary>
    /// <returns>Размерность матрицы: кол-во строк (столбцов)</returns>
    public int CalculateSize()
    {
        int i = 0;
        while (i * i < CellsCount)
        {
            i++;
        }

        return i;
    }

    public Cell GetCell(int number)
    {
        int n = number - 1;
        int x, y;

        x = n / Size;
        y = n % Size;

        return CellField[x, y];
    }
}
