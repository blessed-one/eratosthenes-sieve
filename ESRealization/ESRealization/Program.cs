using ESContract;
using System.Diagnostics;

public static class Program
{
    public static void Main(string[] args)
    {
<<<<<<< HEAD
        var manager = new SieveManager();
        manager.FindPrimes(100);
        manager.GetSteps().Print();
=======
        new SieveManager().FindPrimes(10_000_000).Print();
>>>>>>> 459011fa578c672d7133c0860a08b37e7abe0282
    }
}
