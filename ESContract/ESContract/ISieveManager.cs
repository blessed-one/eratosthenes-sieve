namespace ESContract;

public interface ISieveManager<T, V> where T : IField<V>
                                     where V : ICell
{
    public T Field { get; set; }
    public int NumbersCount { get; set; }

    /// <summary>
    /// Использует алгоритм "Решето Эратосфена"
    /// </summary>
    /// <returns>Массив простых чисел</returns>
    public int[] FindPrimes();

    /// <summary>
    /// Подписывает делегат на обновление ячейки (i, j)
    /// </summary>
    /// <param name="i">Индекс ячейки</param>
    /// <param name="j">Индекс ячейки</param>
    /// <param name="action">Реакция на обновление состояния ячейки</param>
    public void LinkMatrices(int i, int j, Action<State> action);
}

