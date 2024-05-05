using ESContract;
using System.Diagnostics;

public static class Program
{
    public static void Main(string[] args)
    {
        var sieveManager = new SieveManager();
        
        var watch = new Stopwatch();
        watch.Start();
        var primes = sieveManager.FindPrimes(10_000_000);
        watch.Stop();

        Console.WriteLine(watch.ElapsedMilliseconds);
    }
}
