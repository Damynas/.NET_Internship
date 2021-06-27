using System;
using System.Collections.Generic;

namespace Library.App_Code
{
    /// <summary>
    /// Class that stores information about one book
    /// </summary>
    public class Book
    {
        public string Name { get; }
        public string Author { get; }
        public string Category { get; }
        public string Language { get; }
        public DateTime PublicationDate { get; }
        public string ISBN { get; }
        public bool IsTaken { get; set; }

        public List<TakingHistory> History { get; }

        public Book(string name, string author, string category, string language, DateTime publicationDate, string iSBN)
        {
            Name = name;
            Author = author;
            Category = category;
            Language = language;
            PublicationDate = publicationDate;
            ISBN = iSBN;
            IsTaken = false;
            History = new List<TakingHistory>();
        }

        public override string ToString() => string.Format("| {0, -20} | {1, -20} | {2, -10} | {3, -10} | {4, -16} | {5, -15} | {6, -8} |", Name, Author, Category, Language, PublicationDate.ToString("yyyy/MM/dd"), ISBN, IsTaken);


        /// <summary>
        /// Method that returns history event count
        /// </summary>
        /// <returns>Retuns history event quantity</returns>
        public int HistoryEventCount() => History.Count;

        /// <summary>
        /// Method that add new history event to the event list
        /// </summary>
        /// <param name="historyEvent">History event to add</param>
        public void AddHistoryEvent(TakingHistory historyEvent) => History.Add(historyEvent);

        /// <summary>
        /// Mehtod to get last history event
        /// </summary>
        /// <returns>Returns the last history event</returns>
        public TakingHistory GetLastHistoryEvent()
        {
            if(HistoryEventCount() > 0) { return History[HistoryEventCount() - 1]; }
            return null;
        }

        public override bool Equals(object obj) => obj is Book book && ISBN == book.ISBN;

        public override int GetHashCode() => HashCode.Combine(ISBN);
    }
}
