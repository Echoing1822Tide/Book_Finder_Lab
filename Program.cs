using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookFinder;

public class Book
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public int CopyrightYear { get; set; }
    public string Description { get; set; }
    public string ItemType { get; set; }
    public int Rating { get; set; }
    public string Review { get; set; }
}

public class Movie
{
    public int Id { get; set; }
    public string Title { get; set; }
    public int CopyrightYear { get; set; }
    public string Description { get; set; }
    public string ItemType { get; set; }
    public int Rating { get; set; }
    public string Review { get; set; }
    public int RuntimeMinutes { get; set; }
}

public class Program
{
    private const int MAX_BOOKS = 1000;

    //track the actual number of books
    //and the next index to insert (the tail of the array)
    private static int _bookCount = 0;

    //length of the array is 1000 ==> _bookData.Length = 1000;
    //but we don't have 1000 books at inception or probably ever;
    //_bookCount tracks the next available index
    //_bookCount is the number of books we currently have in inventory/on the shelf
    private static string[] _bookData = new string[MAX_BOOKS];

    public static void Main(string[] args)
    {
        // var books = new Book[2] {b, b2};
        // var movies = new Movie[2] {m, m2};
        Book[] myBooks = new Book[10]; //there will be nulls!!! (what is one way I could prevent iterating to a null?)
        Movie[] myMovies = new Movie[10]; //there will be nulls!!! (what is one way I could prevent iterating to a null?)
                                          //int bookCount = 0;
                                          //int movieCount = 0;

        //Movie/Book
        var b = new Book();
        b.Title = "Harry Potter and the Prisoner of Azkaban";
        b.Author = "J. K. Rowling";
        b.CopyrightYear = 1994;
        b.Description = "One of the potter books, probably fights with his friends against someone";
        b.ItemType = "Book";

        var b2 = new Book();
        b2.Title = "Harry Potter and the Sorcerer's Stone";
        b2.Author = "J. K. Rowling";
        b2.CopyrightYear = 1998;
        b2.Description = "One of the potter books, probably fights with his friends against someone again";
        b2.ItemType = "Book";

        var lotr = new Book();
        lotr.Author = "J. R. R. Tolkein";
        lotr.CopyrightYear = 1954;
        lotr.Description = "In the first book in the LOTR trilogy, Frodo finds the ring.";
        lotr.Rating = 4;
        lotr.Review = "A little slow at first, but great overall story";
        lotr.Title = "The Lord of the Rings: The Fellowship of the Ring";
        lotr.ItemType = "Book";

        var topGun = new Movie();
        topGun.CopyrightYear = 1986;
        topGun.Description = "Navy pilots learn to be the best of the best of the best.";
        topGun.Rating = 5;
        topGun.Review = "Highly enjoyable, must see movie!";
        topGun.RuntimeMinutes = 120;
        topGun.Title = "Top Gun";
        topGun.ItemType = "Movie";

        var m2 = new Movie();
        m2.Title = "Harry Potter and the Chamber of Secrets";
        m2.CopyrightYear = 2008;
        m2.Description = "More potter lore";
        m2.Rating = 3;
        m2.Review = "Has good music, that's about it";
        m2.RuntimeMinutes = 120;
        m2.ItemType = "Movie";

        //add items to collections
        myBooks[0] = lotr;
        myBooks[1] = b;
        myBooks[2] = b2;
        //myBooks[3] = topGun;
        myMovies[0] = topGun;
        myMovies[1] = m2;
        //myMovies[2] = lotr;

        foreach (var item in myMovies)
        {
            if (item is null)
            {
                break; //or continue if you still want to keep iterating to the end of the array
            }

            Console.WriteLine($"New Movie: {item.Title}");
        }

        foreach (var item in myBooks)
        {
            if (item is not null)
            {
                Console.WriteLine($"New Book: {item.Title}");
            }
        }

        //you can put different objects into an array at the OBJECT base level:
        object[] myObjects = new object[10];
        myObjects[0] = lotr;
        myObjects[1] = topGun;

        foreach (var o in myObjects)
        {
            if (o != null)
            {
                Console.WriteLine($"Item: {o.ToString()}");

                //Console.WriteLine($"My object has the title {o.Title}");
                if (o is Movie)
                {
                    var title = ((Movie)o).Title;
                    var rtMinutes = ((Movie)o).RuntimeMinutes;
                    Console.WriteLine($"My movie has the title {title} | with {rtMinutes} runtime minutes");
                }
                else if (o is Book)
                {
                    var book = (Book)o;
                    Console.WriteLine($"My book has the title {book.Title} | by {book.Author}");
                }
            }
        }

        Console.WriteLine("Welcome to the Book Inventory Manager!");
        bool runAgain = false;

        do
        {
            var option = PrintMenu();
            switch (option)
            {
                case 1:
                    //List Books
                    PrintBooksList();
                    break;
                case 2:
                    //Get Book
                    //User gives you an ID to find in the book array:
                    //that value from the user is passed as a parameter to the GetBook method:
                    GetBook(GetValueFromUser("Which Book Id would you like to get?"));
                    break;
                case 3:
                    //Add Book
                    AddBook();
                    break;
                case 4:
                    //Delete Book
                    DeleteBook(GetValueFromUser("Which Book Id would you like to delete?"));
                    break;
                case 20:
                    Console.WriteLine("Operation Cancelled");
                    break;
                default:
                    Console.WriteLine("You have entered an invalid choice");
                    break;
            }
            Console.WriteLine("Would you like to continue [y/n]?");
            runAgain = (Console.ReadLine()).StartsWith("Y", StringComparison.OrdinalIgnoreCase);
        } while (runAgain == true);

        Console.WriteLine("Thank you for using the Book Inventory Manager!");
    }

    private static int PrintMenu()
    {
        PrintStars(80);
        Console.WriteLine($"{"* What would you like to do today?",-79}*");
        Console.WriteLine($"{"* 1) List Books",-79}*");
        Console.WriteLine($"{"* 2) Get Book",-79}*");
        Console.WriteLine($"{"* 3) Add Book",-79}*");
        Console.WriteLine($"{"* 4) Delete Book",-79}*");
        Console.WriteLine($"{"* 20) Cancel",-79}*");
        PrintStars(80);
        bool goodChoice = int.TryParse(Console.ReadLine(), out int choice);
        if (!goodChoice)
        {
            Console.WriteLine("Please make sure to enter a valid number.");
            PrintMenu();
        }
        return choice;
    }

    private static void PrintBooksList()
    {
        PrintStars(80);
        Console.WriteLine($"{"* Books:",-79}*");

        if (_bookCount > 0)
        {
            for (int i = 0; i < _bookCount; i++)
            {
                var nextBook = _bookData[i];
                var bookParts = nextBook.Split('|');
                var id = bookParts[0];
                var name = bookParts[1];
                Console.WriteLine($"Book {id}] {name}");
            }
        }
        else
        {
            Console.WriteLine("There are no books in inventory");
        }

        PrintStars(80);
    }

    /// <summary>
    /// Get a book from inventory (off the shelf) by the passed in book id
    /// </summary>
    /// <param name="id">The id of the book to get</param>
    private static void GetBook(int id)
    {
        PrintStars(80);
        Console.WriteLine($"{"* Book Details:",-79}*");

        //no books, so no possible match: get out
        if (_bookCount <= 0)
        {
            //no books, no reason to look:
            Console.WriteLine($"No book found matching id {id}, make sure books are in inventory");
            //exit...
            return;  //no more need for an else because this was the else
        }

        //only get here if there are books...
        //if there are books, look for the match
        //does the book exist in the array? If so, give me the index where it sits
        //if we don't have the book, this will return -1
        //if we have the book, this will return the index in the array where the book with the matching id lives:
        var index = FindBookByIdSplitString(id);

        //no matching book when index < 0 (-1)
        if (index < 0)
        {
            //we didn't find it
            Console.WriteLine($"No book found matching id {id}, make sure books are in inventory");
            return;
        }

        //if we found it at index, go get the specific book FROM that index
        //id should match
        //title will be expected title for that book id
        var theBook = GetBookFromArrayByIndex(index);

        //found a book
        Console.WriteLine("Book Found: ");
        Console.WriteLine($"{theBook.Id}] {theBook.Title}");

        PrintStars(80);
    }

    /// <summary>
    /// Gives the index where the matching book lives by book id
    /// </summary>
    /// <param name="id">the book to find</param>
    /// <returns>index of book in the array if found, otherwise -1</returns>
    private static int FindBookByIdSplitString(int id)
    {
        for (int i = 0; i < _bookCount; i++)
        {
            var nextBook = _bookData[i];
            var bookParts = nextBook.Split('|');
            var success = int.TryParse(bookParts[0], out int nextId);
            if (success && nextId == id)
            {
                return i;
            }
        }
        return -1;
    }

    /// <summary>
    /// Takes the index and returns the book that lives in that index
    /// </summary>
    /// <param name="index">the index in the array to get the book from directly</param>
    /// <returns>the book that lives in the index passed in</returns>
    private static Book GetBookFromArrayByIndex(int index)
    {
        var nextBook = _bookData[index];
        var bookParts = nextBook.Split('|');
        var success = int.TryParse(bookParts[0], out int nextId);
        if (!success)
        {
            throw new Exception("Invalid index or book data at index");
        }
        var theBook = new Book();
        theBook.Id = nextId;
        theBook.Title = bookParts[1];
        return theBook;
    }

    private static void AddBook()
    {
        PrintStars(80);
        Console.WriteLine("What is the book id?");
        var id = Console.ReadLine();
        Console.WriteLine("What is the book name?");
        var name = Console.ReadLine();

        //use id and name to compose entry:
        var theBook = $"{id}|{name}";

        //add to inventory
        if (_bookCount < MAX_BOOKS)
        {
            _bookData[_bookCount] = theBook;
            _bookCount++;
            Console.WriteLine($"Book {id}|{name} added to inventory!");
        }
        else
        {
            Console.WriteLine($"Could not add book {theBook}, because inventory is full");
        }

        PrintStars(80);
    }

    private static void DeleteBook(int id)
    {
        PrintStars(80);
        var deleteIndex = -1;
        //Delete the book from inventory
        if (_bookCount > 0)
        {
            for (int i = 0; i < _bookCount; i++)
            {
                var nextBook = _bookData[i];
                var bookParts = nextBook.Split('|');
                var success = int.TryParse(bookParts[0], out int nextId);
                if (success && nextId == id)
                {
                    deleteIndex = i;
                    break;
                }
            }
        }

        if (deleteIndex >= 0)
        {
            if (deleteIndex == _bookCount - 1)
            {
                //this is the only book or it is the last valid book
                _bookData[deleteIndex] = null;
            }
            else
            {
                //swap last book overwriting deleted book:
                _bookData[deleteIndex] = _bookData[_bookCount - 1];
                //clear out the last book
                _bookData[_bookCount - 1] = null;
            }
            _bookCount--;
            Console.WriteLine($"Book with id: {id} has been removed successfully");
        }

        PrintStars(80);
    }

    private static void PrintStars(int num)
    {
        Console.WriteLine(new String('*', num));
    }

    private static int GetValueFromUser(string message)
    {
        Console.WriteLine(message);
        var success = int.TryParse(Console.ReadLine(), out int result);
        while (!success)
        {
            //iterate until the user enters a number value:
            Console.WriteLine("Please make sure to enter a valid number!");
            Console.WriteLine(message);
            var entry = Console.ReadLine();
            success = int.TryParse(entry, out int result2);
            if (success)
            {
                result = result2;
            }
        }

        //confirm number with user
        Console.WriteLine($"You entered {result}.  Is this the correct number [y/n]?");
        success = Console.ReadLine().StartsWith("Y", StringComparison.OrdinalIgnoreCase);
        if (!success)
        {
            //user does not confirm, start over
            result = GetValueFromUser(message);
        }
        return result;
    }
}


