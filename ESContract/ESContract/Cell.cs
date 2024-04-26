namespace ESContract
{
    public class Cell
    {
        public int Number { get; init; }
        public State State { get; private set; } = State.Unknown;
        public delegate void NotifyUpdate(int change);
        public event NotifyUpdate? StateUpdateNotification;

        /// <summary>
        /// Изменяет состояние клетки и оповещает подписчиков об этом изменении.
        /// </summary>
        /// <param name="state">
        /// Новое состояние клетки.
        /// </param>
        public void UpdateState(State state)
        {
            State = state;
            if (StateUpdateNotification != null) { StateUpdateNotification((int)state); }
        }
    }
}

