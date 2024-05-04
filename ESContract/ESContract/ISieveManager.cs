using System.Reflection.Metadata.Ecma335;

namespace ESContract;

public interface ISieveManager<T> where T : IField
                                     
{
    public T Field { get; set; }
    public int NumbersCount { get; set; }

    /// <summary>
    /// Использует алгоритм "Решето Эратосфена"
    /// </summary>
    /// <returns>Массив простых чисел</returns>
    public int[] FindPrimes();

    public Cell GetCellByIndex(int i, int j) => Field.CellField[i, j];
}

