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
            var books = builder.ReadAll<Books>();
            var authors = builder.ReadAll<Author>();
            var categories = builder.ReadAll<Categories>();
            var loans = builder.ReadAll<BooksOutOnLoan>();

            Console.WriteLine("All users in the database: \n");
            foreach(var user in users)
            {
                Console.WriteLine(user);
                Console.WriteLine("-----------------------\n");
            }

            Console.WriteLine("All books in the database: \n");
            foreach(var book in books)
            {
                Console.WriteLine(book);
                Console.WriteLine("-----------------------\n");
            }

            Console.WriteLine("All authors in the database: \n");
            foreach (var author in authors)
            {
                Console.WriteLine(author);
                Console.WriteLine("-----------------------\n");
            }

            Console.WriteLine("All categories in the database: \n");
            foreach(var category in categories)
            {
                Console.WriteLine(category);
                Console.WriteLine("-----------------------\n");
            }

            Console.WriteLine("All books on loan in the database: \n");
            foreach(var loan in loans)
            {
                Console.WriteLine(loan);
                Console.WriteLine("-----------------------\n");
            }
        }

        Console.ReadKey();
        
    }
    
}


