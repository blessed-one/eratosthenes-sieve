using ESContract;


public class Cell : ICell
{
    public delegate void NotifyUpdate(int change);
    public event ICell.NotifyUpdate? StateUpdateNotification;

    public void UpdateState(State state)
    {
        StateUpdateNotification?.Invoke((int)state);
    }
}
