using System.ComponentModel;

namespace ESContract;

public class Cell : INotifyPropertyChanged
{
    public State State { get; set; }
    /// <summary>
    /// Событие - обновление состояния клетки
    /// </summary>
    public event PropertyChangedEventHandler? PropertyChanged;

    /// <summary>
    /// Изменяет состояние клетки и оповещает подписчиков об этом изменении.
    /// </summary>
    /// <param name="state">
    /// Новое состояние клетки.
    /// </param>
    public void UpdateState(State state)
    {
        State = state;
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(""));
    }
}

