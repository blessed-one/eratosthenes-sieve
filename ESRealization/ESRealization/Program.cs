public static class Program
{
    public static void Main(string[] args)
    {
        var sieveManager = new SieveManager(100);
        var primes = sieveManager.FindPrimes();
        primes.Print();
    }
}
