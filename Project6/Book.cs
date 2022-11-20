///////////////////////////////////////////////////////////////////////////////
//
// Author: Caden Lafollette
// Course: CSCI-2210-001 - Data Structures
// Assignment: Project 6
// Description: Class for creating book objects and printing them out.
//
///////////////////////////////////////////////////////////////////////////////

namespace Project6
{
    internal class Book : IComparable<Book>
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public int Pages { get; set; }
        public string Publisher { get; set; }
        public string CompareType { get; set; }

        /// <summary>
        /// dispatch table that maps a string to one of the 3 compares types. takes in 2 book objects to compare, and
        /// returns an int, either -1, 1, or 0 to line up with the IComparable CompareTo() method
        /// </summary>
        public static Dictionary<string, Func<Book, Book, int>> SortMethod = new()
        {
            ["author"] = (book, bookTwo) => book.CompareAuthor(bookTwo),
            ["title"] = (book, bookTwo) => book.CompareTitle(bookTwo),
            ["publisher"] = (book, bookTwo) => book.ComparePublisher(bookTwo)
        };

        /// <summary>
        /// constructor for book object
        /// </summary>
        /// <param name="compareType">user given method for book comparison</param>
        public Book(string compareType)
        {
            CompareType = compareType.ToLower();
        }

        /// <summary>
        /// implementation of IComparable CompareTo() method that utilizes dispatch table defined above
        /// </summary>
        /// <param name="b">book to compare to</param>
        /// <returns></returns>
        public int CompareTo(Book b)
        {
            return SortMethod[b.CompareType](this, b);
        }

        /// <summary>
        /// compares books by author
        /// </summary>
        /// <param name="b">book to be compared to</param>
        /// <returns></returns>
        public int CompareAuthor(Book b)
        {
            if (b.Author == this.Author)
                return 0;

            string shorterAuthor = (b.Author.Length < this.Author.Length) ? b.Author : this.Author;

            for (int i = 0; i < shorterAuthor.Length; i++)
            {
                if (b.Author[i] > this.Author[i])
                    return -1;
                else if (b.Author[i] < this.Author[i])
                    return 1;
            }

            return -1;
        }

        /// <summary>
        /// compares books by title
        /// </summary>
        /// <param name="b">book to be compared to</param>
        /// <returns></returns>
        public int CompareTitle(Book b)
        {
            if (b.Title == this.Title)
                return 0;

            string shorterTitle = (b.Title.Length < this.Title.Length) ? b.Title : this.Title;

            for (int i = 0; i < shorterTitle.Length; i++)
            {
                if (b.Title[i] > this.Title[i])
                    return -1;
                else if (b.Title[i] < this.Title[i])
                    return 1;
            }

            return -1;
        }

        /// <summary>
        /// compares books by publisher
        /// </summary>
        /// <param name="b">book to be compared to</param>
        /// <returns></returns>
        public int ComparePublisher(Book b)
        {
            if (b.Publisher == this.Publisher)
                return 0;

            string shorterPublisher = (b.Publisher.Length < this.Publisher.Length) ? b.Publisher : this.Publisher;

            for (int i = 0; i < shorterPublisher.Length; i++)
            {
                if (b.Publisher[i] > this.Publisher[i])
                    return -1;
                else if (b.Publisher[i] < this.Publisher[i])
                    return 1;
            }

            return -1;
        }

        /// <summary>
        /// print method that displays book's attributes
        /// </summary>
        /// <returns>a string formatted to display book</returns>
        public string Print()
        {
            string bookInfo = string.Empty;

            bookInfo += $"-----------------------------------\n";
            bookInfo += $"Title: {this.Title}\n";
            bookInfo += $"Author: {this.Author}\n";
            bookInfo += $"Page count: {this.Pages}\n";
            bookInfo += $"Publisher: {this.Publisher}\n";

            return bookInfo;
        }
    }
}
