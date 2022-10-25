using QueryBuilder;
using Microsoft.Data.Sqlite;

public class Program
{
    private static string connection =
        "Data Source = " + ProjectRoot.Root + Path.DirectorySeparatorChar +
        "Data" + Path.DirectorySeparatorChar + "Lab 5.db";

    static void Main(String[] args)
    {
        Console.WriteLine(connection);
        Console.ReadKey();
    }
}


