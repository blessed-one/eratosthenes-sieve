namespace ESContract;

public interface ISieveManager
{
    /// <summary>
    /// Использует алгоритм "Решето Эратосфена"
    /// </summary>
    /// <returns>Массив простых чисел</returns>
    public int[] FindPrimes(int n);

    /// <summary>
    /// Возвращает очерёдность изменений состояний State чисел 
    /// </summary>
    /// <returns>Массив пар (число, состояние)</returns>
    public (int Number, State State)[] GetSteps();
}

