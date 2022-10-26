using QueryBuilder;
using Microsoft.Data.Sqlite;

public class Program
{
    static string connection = "Data Source = " + ProjectRoot.Root + Path.DirectorySeparatorChar + "Data" + Path.DirectorySeparatorChar + "Lab 5.db";

    static void Main(String[] args)
    {
                //Console.WriteLine(connection);
        using (var builder = new QueryBuilder.QueryBuilder(connection))
        {
            //Console.WriteLine(builder.ReadAll<Users>());
            var users = builder.ReadAll<Users>();
            
            foreach(var user in users)
            {
                Console.WriteLine(user);
                Console.WriteLine("-----------------------");
            }
        }

        Console.ReadKey();
        
    }
    
}


