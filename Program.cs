using System;
using System.Buffers.Binary;
using System.Data.Common;
using System.Runtime.CompilerServices;
using System.Threading;

namespace BookFinder
{
    public struct Book
    {
        public int Id;
        public string Title;
    };

    class Program
    {
        private const int MAX_BOOKS = 1000;
        private static int _bookCount = 0;
        private static string[] _bookData = new string[MAX_BOOKS];

        // Prints a line of stars of the specified length
        private static void PrintStars(int count)
        {
            Console.WriteLine(new string('*', count));
        }

        static void Main(string[] args)
        {
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
                    case 5:
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

        private static void PrintBooksList()
        {
            throw new NotImplementedException();
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
        /// <summary>
        /// Prompts the user for an integer value and returns it.
        /// </summary>
        /// <param name="prompt">The prompt to display to the user.</param>
        /// <returns>The integer value entered by the user.</returns>
        private static int GetValueFromUser(string prompt)
        {
            int value;
            bool validInput = false;
            do
            {
                Console.WriteLine(prompt);
                string input = Console.ReadLine();
                validInput = int.TryParse(input, out value);
                if (!validInput)
                {
                    Console.WriteLine("Please enter a valid integer value.");
                }
            } while (!validInput);
            return value;
        }

        /// <summary>
        /// Adds a new book to the inventory.
        /// </summary>
        private static void AddBook()
        {
            if (_bookCount >= MAX_BOOKS)
            {
                Console.WriteLine("Cannot add more books. Inventory is full.");
                return;
            }

            int id = GetValueFromUser("Enter Book Id:");
            Console.WriteLine("Enter Book Title:");
            string title = Console.ReadLine();

            // Check for duplicate ID
            if (FindBookByIdSplitString(id) >= 0)
            {
                Console.WriteLine($"A book with Id {id} already exists.");
                return;
            }

            _bookData[_bookCount] = $"{id}|{title}";
            _bookCount++;
            Console.WriteLine("Book added successfully.");
        }

        /// <summary>
        /// Deletes a book from the inventory by its id.
        /// </summary>
        /// <param name="id">The id of the book to delete.</param>
        private static void DeleteBook(int id)
        {
            int index = FindBookByIdSplitString(id);
            if (index < 0)
            {
                Console.WriteLine($"No book found matching id {id}, nothing deleted.");
                return;
            }

            // Shift all books after the deleted one to the left
            for (int i = index; i < _bookCount - 1; i++)
            {
                _bookData[i] = _bookData[i + 1];
            }
            _bookData[_bookCount - 1] = null;
            _bookCount--;
            Console.WriteLine($"Book with id {id} deleted successfully.");
        }
    }
}
