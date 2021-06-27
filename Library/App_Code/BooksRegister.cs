using System.Collections.Generic;
using System.Linq;

namespace Library.App_Code
{
    /// <summary>
    /// Class that stores all books and has methods to acces the book list
    /// </summary>
    public class BooksRegister
    {
        private readonly List<Book> allBooks;

        public BooksRegister()
        {
            allBooks = new List<Book>();
        }
        public BooksRegister(List<Book> books)
        {
            allBooks = books.ToList();
        }

        public int Count() => allBooks.Count;

        public bool Contains(Book book) => allBooks.Contains(book);

        public void AddBook(Book book) => allBooks.Add(book);

        public Book GetBookByISBN(string ISBN)
        {
            foreach (var book in allBooks.Where(book => book.ISBN == ISBN)) { return book; }
            return null;
        }

        /// <summary>
        /// Method to get all books ISBN codes list
        /// </summary>
        /// <returns>Returns the list of all ISBN codes</returns>
        public List<string> GetAllISBN()
        {
            List<string> allISBN = new();
            allISBN.AddRange(allBooks.Select(book => book.ISBN));
            return allISBN;
        }

        /// <summary>
        /// Method to get available books ISBN codes list
        /// </summary>
        /// <returns>Returns the list of avalable books ISBN codes</returns>
        public List<string> GetAvailableISBN()
        {
            List<string> availableISBN = new();
            availableISBN.AddRange(allBooks.Where(book => !book.IsTaken).Select(book => book.ISBN));
            return availableISBN;
        }

        /// <summary>
        /// Method to get taken books ISBN codes list
        /// </summary>
        /// <returns>Returns the list of taken books ISBN codes</returns>
        public List<string> GetTakenISBN()
        {
            List<string> takenISBN = new();
            takenISBN.AddRange(allBooks.Where(book => book.IsTaken).Select(book => book.ISBN));
            return takenISBN;
        }

        /// <summary>
        /// Method to delete specific book by its ISBN code
        /// </summary>
        /// <param name="iSBN">Book's to delete ISBN code</param>
        public void DeleteBookByISBN(string iSBN)
        {
            Book bookToRemove = null;
            foreach (var book in allBooks.Where(book => book.ISBN == iSBN)) { bookToRemove = book; }
            if (bookToRemove != null) { allBooks.Remove(bookToRemove); }
        }

        /// <summary>
        /// Method that filter the book list by special criterias
        /// </summary>
        /// <param name="filterBy">Parameter to filter by</param>
        /// <param name="filteringCase">Criteria to check if needed</param>
        /// <returns>Filtered list of books</returns>
        public List<Book> Filter(string filterBy, string filteringCase)
        {
            List<Book> filteredBooks = filterBy switch
            {
                "author" => allBooks.Where(nn => nn.Author.ToLower() == filteringCase.ToLower()).ToList(),
                "category" => allBooks.Where(nn => nn.Category.ToLower() == filteringCase.ToLower()).ToList(),
                "language" => allBooks.Where(nn => nn.Language.ToLower() == filteringCase.ToLower()).ToList(),
                "iSBN" => allBooks.Where(nn => nn.ISBN.ToLower() == filteringCase.ToLower()).ToList(),
                "name" => allBooks.Where(nn => nn.Name.ToLower() == filteringCase.ToLower()).ToList(),
                "taken" => allBooks.Where(nn => nn.IsTaken == true).ToList(),
                "available" => allBooks.Where(nn => nn.IsTaken == false).ToList(),
                "allBooks" => new List<Book>(allBooks),
                _ => new List<Book>(),
            };
            return filteredBooks;
        }
    }
}
