using QueryBuilder;
using Microsoft.Data.Sqlite;
using QueryBuilder.Models;

public class Program
{
    static string connection = "Data Source = " + ProjectRoot.Root + Path.DirectorySeparatorChar + "Data" + Path.DirectorySeparatorChar + "Lab 5.db";
    
    static void Main(String[] args)
    {
        Menu();
    }

    private static void Menu() 
    {
        bool menu = true;
        using (var builder = new QueryBuilder.QueryBuilder(connection))
        {
            var users = builder.ReadAll<Users>();
            var books = builder.ReadAll<Books>();
            var authors = builder.ReadAll<Author>();
            var categories = builder.ReadAll<Categories>();
            var loans = builder.ReadAll<BooksOutOnLoan>();

            Users u = new Users(6, "myusername", "my address", "my details", 3, "my email", "my phone number");
            Books b = new Books(6, "Book title", "book isbn", "book date");
            Author a = new Author(6, "Author first name", "author surname");
            Categories c = new Categories(6, "category name");
            BooksOutOnLoan booksOut = new BooksOutOnLoan(6, 6, "date issued", "due date", "date returned");

            while (menu)
            {
                Console.WriteLine($"1. Create\n" +
                                  $"2. Read\n" +
                                  $"3. Update\n" +
                                  $"4. Delete\n"+
                                  $"5. Display everything\n" +
                                  $"6. Exit");
                var choice = Convert.ToInt32(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        Console.Clear();
                        Console.WriteLine("Which class?\n1. Users\n2. Books\n3. Author\n4. Categories\n5. Books on loan\n6. Exit");
                        choice = Convert.ToInt32(Console.ReadLine());
                        
                        switch (choice)
                        {
                            case 1:
                                Console.Clear();
                                builder.Create<Users>(u);
                                MoveOn();
                                break;

                            case 2:
                                Console.Clear();
                                builder.Create<Books>(b);
                                MoveOn();
                                break;
                            case 3:
                                Console.Clear();
                                builder.Create<Author>(a);
                                MoveOn();
                                break;
                            case 4:
                                Console.Clear();
                                builder.Create<Categories>(c);
                                MoveOn();
                                break;
                            case 5:
                                Console.Clear();
                                builder.Create<BooksOutOnLoan>(booksOut);
                                MoveOn();
                                break;
                            case 6:
                                break;

                        }
                        break;
                    case 2:
                        Console.Clear();
                        Console.WriteLine("Which class?\n1. Users\n2. Books\n3. Author\n4. Categories\n5. Books on loan\n6. Exit");
                        choice = Convert.ToInt32(Console.ReadLine());

                        switch (choice)
                        {
                            case 1:
                                Console.Clear();
                                Console.WriteLine(builder.Read<Users>(3));
                                MoveOn();
                                break;

                            case 2:
                                Console.Clear();
                                Console.WriteLine(builder.Read<Books>(3));
                                MoveOn();
                                break;
                            case 3:
                                Console.Clear();
                                Console.WriteLine(builder.Read<Author>(5));
                                MoveOn();
                                break;
                            case 4:
                                Console.Clear();
                                Console.WriteLine(builder.Read<Categories>(2));
                                MoveOn();
                                break;
                            case 5:
                                Console.Clear();
                                Console.WriteLine(builder.Read<BooksOutOnLoan>(1));
                                MoveOn();
                                break;
                            case 6:
                                break;
                        }
                        break;
                    case 3:
                        Console.Clear();
                        Console.WriteLine("Which class?\n1. Users\n2. Books\n3. Author\n4. Categories\n5. Books on loan\n6. Exit");
                        choice = Convert.ToInt32(Console.ReadLine());
                        switch (choice)
                        {
                            case 1:
                                Console.Clear();
                                u = new Users(5, "updated user", "updated address", "updated details", 3, "updated email", "updated phone");
                                builder.Update<Users>(u);
                                MoveOn();
                                break;

                            case 2:
                                Console.Clear();
                                b = new Books(5, "updated title", "updated isbn", "updated publication");
                                builder.Update<Books>(b);
                                MoveOn();
                                break;
                            case 3:
                                Console.Clear();
                                a = new Author(5, "updated author", "updated surname");
                                builder.Update<Author>(a);
                                MoveOn();
                                break;
                            case 4:
                                Console.Clear();
                                c = new Categories(5, "updated category");
                                builder.Update<Categories>(c);
                                MoveOn();
                                break;
                            case 5:
                                Console.Clear();
                                booksOut = new BooksOutOnLoan(5, 5, "updated book", "updated due date", "updated return date");
                                builder.Update<BooksOutOnLoan>(booksOut);
                                MoveOn();
                                break;
                            case 6:
                                break;
                        }
                        //a = new Author(5, "updated author", "updated surname");
                        //builder.Update<Author>(a);
                        break;
                    case 4:
                        Console.Clear();
                        Console.WriteLine("Which class?\n1. Users\n2. Books\n3. Author\n4. Categories\n5. Books on loan\n6. Exit");
                        choice = Convert.ToInt32(Console.ReadLine());

                        switch (choice)
                        {
                            case 1:
                                Console.Clear();
                                builder.Delete<Users>(u);
                                MoveOn();
                                break;

                            case 2:
                                Console.Clear();
                                builder.Delete<Books>(b);
                                MoveOn();
                                break;
                            case 3:
                                Console.Clear();
                                builder.Delete<Author>(a);
                                MoveOn();
                                break;
                            case 4:
                                Console.Clear();
                                builder.Delete<Categories>(c);
                                MoveOn();
                                break;
                            case 5:
                                Console.Clear();
                                builder.Delete<BooksOutOnLoan>(booksOut);
                                MoveOn();
                                break;
                            case 6:
                                break;
                        }
                        break;
                    case 5:
                        GetAllFromDB(users, books, authors, categories, loans);
                        MoveOn();
                        break;
                    case 6:
                        menu = false;
                        break;
                    default:
                        Console.WriteLine("Out of range");
                        break;
                }
            }
        }
    }

    private static void GetAllFromDB(List<Users> users, List<Books> books,List<Author> authors, List<Categories> categories, List<BooksOutOnLoan> loans)
    {

        GetAllUsers(users);
        Console.WriteLine("------------------------------");
        GetAllBooks(books);
        Console.WriteLine("------------------------------");
        GetAllAuthors(authors);
        Console.WriteLine("------------------------------");
        GetAllCategories(categories);
        Console.WriteLine("------------------------------");
        GetBooksOnLoan(loans);
    }

    private static void GetSpecificEntry<T>(QueryBuilder.QueryBuilder builder, int Id) where T : IClassModels, new()
    {
        
        Console.WriteLine(builder.Read<T>(Id));
    }

    private static void GetBooksOnLoan(List<BooksOutOnLoan> loans)
    {
        foreach (var loan in loans)
        {
            Console.WriteLine(loan);
            Console.WriteLine("-----------------------\n");
        }
    }

    private static void GetAllCategories(List<Categories> categories)
    {
        foreach (var category in categories)
        {
            Console.WriteLine(category);
            Console.WriteLine("-----------------------\n");
        }
    }

    private static void GetAllAuthors(List<Author> authors)
    {
        foreach (var author in authors)
        {
            Console.WriteLine(author);
            Console.WriteLine("-----------------------\n");
        }
    }

    private static void GetAllBooks(List<Books> books)
    {
        foreach (var book in books)
        {
            Console.WriteLine(book);
            Console.WriteLine("-----------------------\n");
        }
    }

    private static void GetAllUsers(List<Users> users)
    {
        Console.WriteLine("-----------------------\n");
        foreach (var user in users)
        {
            Console.WriteLine(user);
            Console.WriteLine("-----------------------\n");
        }
    }

    private static void MoveOn()
    {
        Console.WriteLine("Press any key to continue...");
        Console.ReadLine();
        Console.Clear();
    }
}


