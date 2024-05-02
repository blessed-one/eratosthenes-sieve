namespace ESContract;

public interface IField<T> where T : ICell
{
    /// <summary>
    /// Размер поля
    /// </summary>
    public int Size { get; init; }
    /// <summary>
    /// Количество клеток
    /// </summary>
    public int CellsCount { get; init; }
    /// <summary>
    /// Матрица, хранящая клетки
    /// </summary>
    public T[,] CellField { get; init; }

    /// <summary>
    /// Возвращает клетку с данным номером
    /// </summary>
    /// <param name="cellNumber">Номер искомой клетки</param>
    /// <returns>Искомая клетка</returns>
    public T GetCell(int cellNumber);
}
