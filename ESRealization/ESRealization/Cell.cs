using ESContract;


public class Cell : ICell
{
    public event Func<State, Task>? StateUpdateNotification;

    public void UpdateState(State state)
    {
        StateUpdateNotification?.Invoke(state);
    }
}
