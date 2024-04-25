namespace ESContract
{
    public interface ISieveManager
    {
        /// <summary>
        /// Использует алгоритм "Решето Эратосфена"
        /// </summary>
        /// <returns>Массив простых чисел</returns>
        int[] FindPrimes(int n);
    }
}

