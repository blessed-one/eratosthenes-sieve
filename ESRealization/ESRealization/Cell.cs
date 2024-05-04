using ESContract;


public class Cell : ICell
{
    public event Action<State>? StateUpdateNotification;

    public void UpdateState(State state)
    {
        StateUpdateNotification?.Invoke(state);
    }
}
