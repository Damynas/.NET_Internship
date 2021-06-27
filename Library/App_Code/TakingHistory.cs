using System;

namespace Library.App_Code
{
    /// <summary>
    /// Class that store information about one taking history event
    /// </summary>
    public class TakingHistory
    {
        public string FullName { get; }
        public int BookShouldBeTakenFor_Days { get; }
        public DateTime DateTaken { get; }
        public DateTime DateReturned { get; set; }

        public TakingHistory(string fullName, int bookShouldBeTakenFor_Days, DateTime dateTaken)
        {
            FullName = fullName;
            BookShouldBeTakenFor_Days = bookShouldBeTakenFor_Days;
            DateTaken = dateTaken;
            DateReturned = DateTime.MinValue;
        }
    }
}
