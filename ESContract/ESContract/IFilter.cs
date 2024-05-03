namespace ESContract;

public interface IFilter
{
    /// <summary>
    /// Обработка чисел из NumbersQueue
    /// </summary>
    public void Operate();

    /// <summary>
    /// Передача следующему фильтру одно значение
    /// </summary>
    /// <param name="num">Число на передачу</param>
    public void GiveNextOneNum(int num);

    /// <summary>
    /// Возвращает основное число фильтра и числа из очереди на проверку
    /// </summary>
    /// <returns>
    /// Список чисел
    /// </returns>
    public List<int> GetNumbers();
}