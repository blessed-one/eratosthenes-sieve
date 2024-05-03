public static class Program
{
    public static void Main(string[] args)
    {
        var sieveManager = new SieveManager(10);
        var primes = sieveManager.FindPrimes();
        primes.Print();
    }
}
