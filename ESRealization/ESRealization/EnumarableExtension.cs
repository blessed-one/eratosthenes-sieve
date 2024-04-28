public static class EnumerableExtension
{
    public static void Print<T>(this IEnumerable<T> items)
    {
        foreach (var item in items)
        {
            Console.Write($"{item} ");
        }
        Console.WriteLine();
    }
}
