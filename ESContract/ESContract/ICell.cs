namespace ESContract;

public interface ICell
{
    /// <summary>
    /// Событие - обновление состояния клетки
    /// </summary>
    public event Func<State, Task>? StateUpdateNotification;
    /// <summary>
    /// Изменяет состояние клетки и оповещает подписчиков об этом изменении.
    /// </summary>
    /// <param name="state">
    /// Новое состояние клетки.
    /// </param>
    public void UpdateState(State state);
}

