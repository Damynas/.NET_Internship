namespace Library.App_Code
{
    /// <summary>
    /// Main class to lunch library app
    /// </summary>
    public class Program
    {
        private const string BookData = "../../../App_Data/bookData.json"; // Data file to store information about books
        private const string PeopleData = "../../../App_Data/peopleData.json"; // Data file to store information about current people who taken at least 1 book

        static void Main(string[] args)
        {
            MainLibrary library = new();
            library.Start(BookData, PeopleData); // Lunch library
        }
    }
}
