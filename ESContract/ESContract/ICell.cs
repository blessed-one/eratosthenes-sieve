namespace ESContract;

public interface ICell
{
    /// <summary>
    /// Изменяет состояние клетки и оповещает подписчиков об этом изменении.
    /// </summary>
    /// <param name="state">
    /// Новое состояние клетки.
    /// </param>
    public void UpdateState(State state);
}

