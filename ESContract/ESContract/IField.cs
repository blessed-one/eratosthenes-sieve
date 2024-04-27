namespace ESContract;

public interface IField
{
    /// <summary>
    /// Вычисляет оптимальный размер квадратного поля (X на X)
    /// </summary>
    /// <returns>
    /// Размер поля X
    /// </returns>
    public int CalculateSize();

    /// <summary>
    /// Возвращает клетку с номером number
    /// </summary>
    /// <param name="number">
    /// Номер искомой клетки
    /// </param>
    /// <returns>
    /// Искомая клетка
    /// </returns>
    public ICell GetCell(int number);
}

