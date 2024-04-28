namespace ESContract;

public interface ISieveManager<T, V> where T : IField<V>
                                     where V : ICell
{
    /// <summary>
    /// Использует алгоритм "Решето Эратосфена"
    /// </summary>
    /// <returns>Массив простых чисел</returns>
    int[] FindPrimes(int n, T field);
}

