using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using Figgle;

namespace Library.App_Code
{
    /// <summary>
    /// Class that creates the library application and stores all the methods to use it
    /// </summary>
    public class MainLibrary
    {
        /// <summary>
        /// Method that handles all the library app work
        /// </summary>
        /// <param name="bookDataFileName">Data file to store book information</param>
        /// <param name="peopleDataFileName">Data file to store information about people who taken books</param>
        public void Start(string bookDataFileName, string peopleDataFileName)
        {
            Console.Title = "Visma's book library!";

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                Console.SetWindowSize(130, 30);
            }

            BooksRegister allBooks = File.Exists(bookDataFileName) ? InOut.ReadBookData(bookDataFileName) : new BooksRegister();
            Dictionary<string, int> peopleData = File.Exists(peopleDataFileName) ? InOut.ReadPeopleData(peopleDataFileName) : new Dictionary<string, int>();

            string[] mainMenuOptions = { "Add book", "List books", "Take book", "Return book", "Remove book", "Exit" };
            string mainMenuTitle = FiggleFonts.Larry3d.Render("Visma's library");
            string mainMenuInstructions = "Use arrow keys to navigate the menu and press enter to select...";

            Menu mainMenu = new(mainMenuOptions, mainMenuTitle, mainMenuInstructions); // Main menu for main controlls of the app

            string[] listingOptions = { "List all books", "Filter by author", "Filter by category", "Filter by language", "Filter by ISBN", "Filter by name", "Filter by taken", "Filter by available", "Exit" };
            string listingInstructions = "Chose your prefered filtering...";

            Menu listingMenu = new(listingOptions, listingInstructions); // Side menu for choosing prefered listing filtering

            int selectedOption;
            bool menuOn = true;

            while (menuOn) // The main loop for the application to run
            {
                selectedOption = mainMenu.TheMenu(true);

                switch (selectedOption)
                {
                    case 0: // Add
                        AddNewBook(allBooks);
                        break;
                    case 1: // List
                        selectedOption = listingMenu.TheMenu(false);
                        string filteringCase;
                        switch (selectedOption)
                        {
                            case 0: // List all
                                ListBooks(allBooks, "allBooks", "");
                                break;
                            case 1: // Filter by author
                                Console.Clear();
                                Console.WriteLine("Type 'exit' any time to return to the main menu...");
                                filteringCase = Inputing.InputNonEmptyString("Enter author: ", "author");
                                if (filteringCase == "exit") { break; }
                                ListBooks(allBooks, "author", filteringCase);
                                break;
                            case 2: // filter by category
                                Console.Clear();
                                Console.WriteLine("Type 'exit' any time to return to the main menu...");
                                filteringCase = Inputing.InputNonEmptyString("Enter category: ", "category");
                                if (filteringCase == "exit") { break; }
                                ListBooks(allBooks, "category", filteringCase);
                                break;
                            case 3: // filter by language
                                Console.Clear();
                                Console.WriteLine("Type 'exit' any time to return to the main menu...");
                                filteringCase = Inputing.InputNonEmptyString("Enter language: ", "language");
                                if (filteringCase == "exit") { break; }
                                ListBooks(allBooks, "language", filteringCase);
                                break;
                            case 4: // Filter by ISBN
                                Console.Clear();
                                Console.WriteLine("Type 'exit' any time to return to the main menu...");
                                filteringCase = Inputing.InputNonEmptyString("Enter ISBN: ", "ISBN");
                                if (filteringCase == "exit") { break; }
                                ListBooks(allBooks, "iSBN", filteringCase);
                                break;
                            case 5: // Filter by name
                                Console.Clear();
                                Console.WriteLine("Type 'exit' any time to return to the main menu...");
                                filteringCase = Inputing.InputNonEmptyString("Enter name: ", "name");
                                if (filteringCase == "exit") { break; }
                                ListBooks(allBooks, "name", filteringCase);
                                break;
                            case 6: // Filter by taken
                                ListBooks(allBooks, "taken", "");
                                break;
                            case 7: // Filter by available
                                ListBooks(allBooks, "available", "");
                                break;
                            default: // Exit
                                break;
                        }
                        break;
                    case 2: // Take
                        TakeBook(allBooks, peopleData);
                        break;
                    case 3: // Return
                        ReturnBook(allBooks, peopleData);
                        break;
                    case 4: // Delete
                        DeleteBook(allBooks);
                        break;
                    case 5: // Exit and save data
                        InOut.SaveBookData(bookDataFileName, allBooks);
                        InOut.SavePeopleData(peopleDataFileName, peopleData);
                        return;
                    default:
                        break;

                }
            }
        }

        /// <summary>
        /// Method that handel the adding of new book into the library
        /// </summary>
        /// <param name="allBooks">Books register that holds all the books</param>
        private void AddNewBook(BooksRegister allBooks)
        {
            string name, author, category, language, iSBN;
            int year, month, day;
            DateTime publicationDate;

            Console.Clear();
            Console.WriteLine("Type 'exit' any time to return to the main menu...");

            //Name
            name = Inputing.InputNonEmptyString("Enter a name of the book: ", "name");
            if (name == "exit") { return; }

            //Author
            author = Inputing.InputNonEmptyString("Enter an author of the book: ", "author");
            if (author == "exit") { return; }

            //Category
            category = Inputing.InputNonEmptyString("Enter book's category: ", "category");
            if (category == "exit") { return; }

            //Language
            language = Inputing.InputNonEmptyString("Enter book's language: ", "lanfuage");
            if (language == "exit") { return; }

            //Publication date
            year = Inputing.InputPublicationYear();
            if (year == -1) { return; }

            month = Inputing.InputPublicationMonth(year);
            if (month == -1) { return; }

            day = Inputing.InputPublicationDay(year, month);
            if (day == -1) { return; }

            publicationDate = new DateTime(year, month, day);

            //ISBN
            List<string> allISBN = allBooks.GetAllISBN();
            iSBN = Inputing.InputISBN(allISBN, false, "to add (every book in this library needs to have unique ISBN code)");
            if (iSBN == "exit") { return; }

            Book book = new(name, author, category, language, publicationDate, iSBN);

            allBooks.AddBook(book);

            Console.Clear();
            Console.WriteLine("New book added succesfuly. Press any key to continue...");
            Console.ReadKey();
        }

        /// <summary>
        /// Method that handles the listing of filtered or all books
        /// </summary>
        /// <param name="allBooks">Books register that holds all the books</param>
        /// <param name="filterBy">The book;s property to filter by</param>
        /// <param name="filteringCase">Filtering case if it is needed (Otherwise pass empty string)</param>
        private void ListBooks(BooksRegister allBooks, string filterBy, string filteringCase)
        {
            Console.Clear();
            List<Book> filteredBooks = allBooks.Filter(filterBy, filteringCase);
            if (filteredBooks.Count > 0)
            {
                Console.WriteLine("Filtered by: " + filterBy + " " + filteringCase);
                Console.WriteLine(new string('-', 121));
                Console.WriteLine(string.Format("| {0, -20} | {1, -20} | {2, -10} | {3, -10} | {4, -16} | {5, -15} | {6, -8} |", "Name", "Author", "Category", "Language", "Publication Date", "ISBN", "Is Taken"));
                Console.WriteLine(new string('-', 121));
                for (int i = 0; i < filteredBooks.Count; i++)
                {
                    Console.WriteLine(filteredBooks[i].ToString());
                }
                Console.WriteLine(new string('-', 121));
                Console.WriteLine("Press any key to continue...");
            }
            else
            {
                Console.WriteLine("There are no books to display. Press any key to continue...");
            }


            Console.ReadKey();
        }

        /// <summary>
        /// Method that handles the book taking
        /// </summary>
        /// <param name="allBooks">Books register that holds all the books</param>
        /// <param name="peopleData">Dictionary that holds data about how many books each person has taken</param>
        private void TakeBook(BooksRegister allBooks, Dictionary<string, int> peopleData)
        {
            string iSBN, fullName;
            int daysToTake;

            Console.Clear();
            ListBooks(allBooks, "available", "");
            List<string> availableISBN = allBooks.GetAvailableISBN();

            if (availableISBN.Count > 0) // Checks if there are available books to be taken
            {
                Console.CursorLeft = 0;

                Console.WriteLine("Type 'exit' any time to return to the main menu...");

                //ISBN
                iSBN = Inputing.InputISBN(availableISBN, true, "to take");
                if (iSBN == "exit") { return; }

                //Full name
                fullName = Inputing.InputNonEmptyString("Enter your full name: ", "name");
                if (fullName == "exit") { return; }

                if (peopleData.ContainsKey(fullName))
                {
                    if (peopleData[fullName] == 3) // Vallidation to see if a person is not trying to take more than 3 books
                    {
                        Console.WriteLine("You cannot take more than 3 books at the time. Please return already taken books and then try again.");
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadKey();
                        return;
                    }
                }

                //Taking length
                daysToTake = Inputing.InputDaysToTake();
                if(daysToTake == -1) { return; }

                TakingHistory newHistoryEvent = new(fullName, daysToTake, DateTime.Now);
                allBooks.GetBookByISBN(iSBN).AddHistoryEvent(newHistoryEvent);
                allBooks.GetBookByISBN(iSBN).IsTaken = true;

                if (!peopleData.ContainsKey(fullName))
                {
                    peopleData.Add(fullName, 1);
                }
                else
                {
                    peopleData[fullName]++;
                }

                Console.Clear();
                Console.WriteLine("Book taken succesfuly. Press any key to continue...");
                Console.ReadKey();
            }
        }

        /// <summary>
        /// Method that handles the returning of a book
        /// </summary>
        /// <param name="allBooks">Books register that holds all the books</param>
        /// <param name="peopleData">Dictionary that holds data about how many books each person taken</param>
        private void ReturnBook(BooksRegister allBooks, Dictionary<string, int> peopleData)
        {
            Console.Clear();
            ListBooks(allBooks, "taken", "");
            List<string> takenISBN = allBooks.GetTakenISBN();

            if (takenISBN.Count > 0)  // Checks if there are taken books to be returned
            {
                Console.CursorLeft = 0;

                Console.WriteLine("Type 'exit' any time to return to the main menu...");

                string iSBN = Inputing.InputISBN(takenISBN, true, "to return");
                if (iSBN == "exit") { return; }

                TakingHistory lastEvent = allBooks.GetBookByISBN(iSBN).GetLastHistoryEvent();
                if (lastEvent != null)
                {
                    lastEvent.DateReturned = DateTime.Now;
                    allBooks.GetBookByISBN(iSBN).IsTaken = false;

                    if (DateTime.Now > lastEvent.DateTaken.AddDays(lastEvent.BookShouldBeTakenFor_Days)) // Return time vallidation
                    {
                        Console.Clear();
                        Console.WriteLine("You are late to return this book. Better late than never, but never late is better;)");
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadKey();
                    }

                    peopleData[lastEvent.FullName]--;
                    if (peopleData[lastEvent.FullName] < 1) { peopleData.Remove(lastEvent.FullName); }

                    Console.Clear();
                    Console.WriteLine("Book returned succesfuly. Press any key to continue...");
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("This book was never taken. Press any key to continue...");
                }

                Console.ReadKey();
            }
        }

        /// <summary>
        /// Method that handles the removal of a book
        /// </summary>
        /// <param name="allBooks">Books register that holds all the books</param>
        private void DeleteBook(BooksRegister allBooks)
        {
            Console.Clear();
            ListBooks(allBooks, "allBooks", "");
            List<string> allISBN = allBooks.GetAllISBN();

            if (allISBN.Count > 0) // Checks if there are books to be removed
            {
                Console.CursorLeft = 0;

                Console.WriteLine("Type 'exit' any time to return to the main menu...");

                string iSBN = Inputing.InputISBN(allISBN, true, "to return");
                if (iSBN == "exit") { return; }

                allBooks.DeleteBookByISBN(iSBN);

                Console.Clear();
                Console.WriteLine("Book deleted succesfuly. Press any key to continue...");
                Console.ReadKey();
            }
        }
    }
}
