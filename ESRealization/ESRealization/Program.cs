public static class Program
{
    public static void Main(string[] args)
    {
        var sieveManager = new SieveManager(1000);
        var primes = sieveManager.FindPrimes();
        primes.Print();
    }
}
