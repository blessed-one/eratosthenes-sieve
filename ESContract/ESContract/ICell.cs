namespace ESContract;

public interface ICell
{
    public delegate void NotifyUpdate(int change);
    /// <summary>
    /// Событие - обновление состояния клетки
    /// </summary>
    public event NotifyUpdate? StateUpdateNotification;
    /// <summary>
    /// Изменяет состояние клетки и оповещает подписчиков об этом изменении.
    /// </summary>
    /// <param name="state">
    /// Новое состояние клетки.
    /// </param>
    public void UpdateState(State state);
}

