using ESContract;
using System.Diagnostics;

public static class Program
{
    public static void Main(string[] args)
    {
        new SieveManager().FindPrimes(10_000_000).Print();
    }
}
