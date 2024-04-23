using System.Drawing;

public static class Program
{
    public static void Main(string[] args)
    {
        int[,] matr = new int[10, 10];
        var cellIndex = TestClass.GetCell(99);
        matr[cellIndex.X, cellIndex.Y] = 1;
        Console.WriteLine(cellIndex);

        TestClass.Print(matr);
    }
}

public static class TestClass
{
    public static (int X, int Y) _size = (10, 10);
    public static (int X, int Y) GetCell(int number)
    {
        int n = number - 1;
        int x = 0, y = 0;

        x = n / _size.X;
        y = n % _size.X;

        return (x, y);
    }

    public static void Print(int[,] data)
    {
        for (int i = 0; i < _size.X; i++)
        {
            for (int j = 0; j < _size.Y; j++)
            {
                Console.Write(data[i,j]);
            }
            Console.WriteLine();
        }
    }
}
