public class Cell
{
    public bool IsVisited { get; set; }
    public bool IsStardedWith {  get; set; }
    public int Number {  get; set; }
    public State State { get; set; }

    public Cell(int number)
    {
        Number = number;
        State = State.Unknown;
    }
}
