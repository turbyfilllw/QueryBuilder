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
        int entry;
        
        using (var builder = new QueryBuilder.QueryBuilder(connection))
        {
            var users = builder.ReadAll<Users>();
            var books = builder.ReadAll<Books>();
            var authors = builder.ReadAll<Author>();
            var categories = builder.ReadAll<Categories>();
            var loans = builder.ReadAll<BooksOutOnLoan>();
            

            while (menu)
            {
                Console.WriteLine($"1. Display everything in the database\n" +
                                  $"2. Display all users in the database\n" +
                                  $"3. Display all books in the database\n" +
                                  $"4. Display all authors in the database\n" +
                                  $"5. Display all categories in the database\n" +
                                  $"6. Display all books on loan in the database\n" +
                                  $"7. Get a specific entry in the database\n" +
                                  $"8. Delete a table from the database\n" +
                                  $"9. Exit");

                var choice = Convert.ToInt32(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        GetAllFromDB(users, books, authors, categories, loans);
                        MoveOn();
                        break;
                    case 2:
                        GetAllUsers(users);
                        MoveOn();
                        break;
                    case 3:
                        GetAllBooks(books);
                        MoveOn();
                        break;
                    case 4:
                        GetAllAuthors(authors);
                        MoveOn();
                        break;
                    case 5:
                        GetAllCategories(categories);
                        MoveOn();
                        break;
                    case 6:
                        GetBooksOnLoan(loans);
                        MoveOn();
                        break;
                    case 7:
                        Console.WriteLine($"Which class?\n" +
                                          $"1. Users\n" +
                                          $"2. Books\n" +
                                          $"3. Authors\n" +
                                          $"4. Categories\n" +
                                          $"5. Books on loan\n");

                        choice = Convert.ToInt32(Console.ReadLine());
                        switch (choice)
                        {
                            case 1:
                                Console.Clear();
                                Console.WriteLine("Which entry?: ");
                                entry = Convert.ToInt32(Console.ReadLine());
                                GetSpecificEntry<Users>(builder, entry);
                                break;
                            case 2:
                                Console.Clear();
                                Console.WriteLine("Which entry?: ");
                                entry = Convert.ToInt32(Console.ReadLine());
                                GetSpecificEntry<Books>(builder, entry);
                                break;
                            case 3:
                                Console.Clear();
                                Console.WriteLine("Which entry?: ");
                                entry = Convert.ToInt32(Console.ReadLine());
                                GetSpecificEntry<Author>(builder, entry);
                                break;
                            case 4:
                                Console.Clear();
                                Console.WriteLine("Which entry?: ");
                                entry = Convert.ToInt32(Console.ReadLine());
                                GetSpecificEntry<Categories>(builder, entry);
                                break;
                            case 5:
                                Console.Clear();
                                Console.WriteLine("Which entry?: ");
                                entry = Convert.ToInt32(Console.ReadLine());
                                GetSpecificEntry<BooksOutOnLoan>(builder, entry);
                                break;
                            default:
                                Console.Clear();
                                Console.WriteLine("Out of range");
                                break;
                        }
                        MoveOn();
                        break;
                    case 8:
                        Console.WriteLine($"Which class?\n" +
                                          $"1. Users\n" +
                                          $"2. Books\n" +
                                          $"3. Authors\n" +
                                          $"4. Categories\n" +
                                          $"5. Books on loan\n");

                        choice = Convert.ToInt32(Console.ReadLine());
                        switch (choice)
                        {
                            case 1:
                                Console.Clear();
                                DeleteFromDatabase<Users>(builder);
                                break;
                            case 2:
                                Console.Clear();
                                DeleteFromDatabase<Books>(builder);
                                break;
                            case 3:
                                Console.Clear();
                                DeleteFromDatabase<Author>(builder);
                                break;
                            case 4:
                                Console.Clear();
                                DeleteFromDatabase<Categories>(builder);
                                break;
                            case 5:
                                Console.Clear();
                                DeleteFromDatabase<BooksOutOnLoan>(builder);
                                break;
                            default:
                                Console.Clear();
                                Console.WriteLine("Out of range");
                                break;
                        }
                        MoveOn();
                        break;
                    case 9:
                        menu = false;
                        break;
                    default:
                        Console.WriteLine("Invalid Entry");
                        MoveOn();
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

    private static void DeleteFromDatabase<T>(QueryBuilder.QueryBuilder builder) where T : IClassModels, new()
    {
        builder.Delete<T>();
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


