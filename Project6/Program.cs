using System.Text.RegularExpressions;
using System.DataStructures;

///////////////////////////////////////////////////////////////////////////////
//
// Author: Caden Lafollette
// Course: CSCI-2210-001 - Data Structures
// Assignment: Project 6
// Description: Contains main method and method generating list of book objects
//
///////////////////////////////////////////////////////////////////////////////

namespace Project6
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //creates a list of books using GetBooks() method
            List<Book> books = GetBooks("title");

            //creates a tree using the NuGet implementation, passing in the list of books created above,
            //and a Func<T,T,int> that is returned from the static SortMethod dispatch table in the book class
            AvlTree<Book> tree = new AvlTree<Book>(books, Book.SortMethod["title"]);

            bool checkingOutBooks = true;
            List<Book> checkedOutBooks = new();

            while(checkingOutBooks)
            {
                //prints all books in order by title
                foreach (Book book in tree.GetInorderEnumerator())
                {
                    Console.WriteLine(book.Print());
                }

                Console.WriteLine("To check out a book, input the title.");
                string bookTitle = Console.ReadLine();

                Book b = new Book("title")
                {
                    Title = bookTitle
                };

                //searches the tree for book, adds it to checked out books, and removes it from tree
                if (tree.Contains(b))
                {
                    checkedOutBooks.Add(tree.FindNode(b).Value);
                    tree.Remove(b);

                    Console.WriteLine("\n\nYour books: ");
                    foreach (Book book in checkedOutBooks)
                    {
                        Console.WriteLine(book.Print());
                    }

                    Console.WriteLine("Check out more books?");

                    if (!Console.ReadLine().ToLower().StartsWith("y"))
                        checkingOutBooks = false;
                }
                else
                {
                    Console.WriteLine("That is not a valid book title.");
                }
            }

            Console.WriteLine("\n\nYou checked out. Your books: ");
            foreach (Book book in checkedOutBooks)
            {
                Console.WriteLine(book.Print());
            }

            bool sortingBooks = true;
            string compareType;

            while (sortingBooks)
            {
                Console.WriteLine("How would you like to sort the library's books? Sort by Author, Title, or Publisher.");
                compareType = Console.ReadLine();

                if (Book.SortMethod.ContainsKey(compareType))
                {
                    books = GetBooks(compareType);
                    tree = new AvlTree<Book>(books, Book.SortMethod[compareType]);

                    //prints all books in order, sorted by given compare type
                    foreach (Book book in tree.GetInorderEnumerator())
                    {
                        Console.WriteLine(book.Print());
                    }

                    Console.WriteLine("Re-Order books?");

                    if (!Console.ReadLine().ToLower().StartsWith("y"))
                        sortingBooks = false;
                }
                else
                {
                    Console.WriteLine("Please input a valid sorting type.");
                }
            }
        }

        /// <summary>
        /// method that returns a list of book objects generated from the books.csv file
        /// </summary>
        /// <returns></returns>
        public static List<Book> GetBooks(string compareType)
        {
            List<Book> books = new List<Book>();

            try
            {
                using (StreamReader sr = new StreamReader("C:\\Users\\lafol\\source\\repos\\Project6\\Project6\\books.csv"))
                {
                    string line;
                    string[] bookInfo;

                    while ((line = sr.ReadLine()) is not null)
                    {
                        //regex expression shamelessly copied from https://gist.github.com/awwsmm/886ac0ce0cef517ad7092915f708175f
                        //(?:,|\n|^) makes sure each value starts at a new file, line, or comma
                        //(\"(?:(?:\"\")*[^\"]*)*\"|[^\",\\n]*|(?:\\n|$)) captures values within a double quoted string OR non-quoted values OR newline characters or end of file characters
                        bookInfo = Regex.Split(line, "(?:,|\n|^)(\"(?:(?:\"\")*[^\"]*)*\"|[^\",\\n]*|(?:\\n|$))");

                        //creates book object and sets attributes obtained from books.csv
                        //checks if given compare type is valid, or sets to a default sorting of title
                        Book b = new(Book.SortMethod.ContainsKey(compareType) ? compareType : "title")
                        {
                            Title = (bookInfo[1] != string.Empty) ? bookInfo[1].Replace("\"", string.Empty) : "*No title*",
                            Author = (bookInfo[3] != string.Empty) ? bookInfo[3].Replace("\"", string.Empty) : "*No author*",
                            Pages = (bookInfo[5] != string.Empty) ? int.Parse(bookInfo[5]) : 0,
                            Publisher = (bookInfo[7] != string.Empty) ? bookInfo[7].Replace("\"", string.Empty) : "*No publisher*"
                        };

                        //adds book to list
                        books.Add(b);
                    }
                }
            }
            catch {Console.WriteLine("File was not found.");}

            return books;
        }
    }
}