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
                                Console.Write("Enter author: ");
                                filteringCase = Console.ReadLine();
                                if (filteringCase == "exit") { break; }
                                ListBooks(allBooks, "author", filteringCase);
                                break;
                            case 2: // filter by category
                                Console.Clear();
                                Console.WriteLine("Type 'exit' any time to return to the main menu...");
                                Console.Write("Enter category: ");
                                filteringCase = Console.ReadLine();
                                if (filteringCase == "exit") { break; }
                                ListBooks(allBooks, "category", filteringCase);
                                break;
                            case 3: // filter by language
                                Console.Clear();
                                Console.WriteLine("Type 'exit' any time to return to the main menu...");
                                Console.Write("Enter language: ");
                                filteringCase = Console.ReadLine();
                                if (filteringCase == "exit") { break; }
                                ListBooks(allBooks, "language", filteringCase);
                                break;
                            case 4: // Filter by ISBN
                                Console.Clear();
                                Console.WriteLine("Type 'exit' any time to return to the main menu...");
                                Console.Write("Enter ISBN: ");
                                filteringCase = Console.ReadLine();
                                if (filteringCase == "exit") { break; }
                                ListBooks(allBooks, "iSBN", filteringCase);
                                break;
                            case 5: // Filter by name
                                Console.Clear();
                                Console.WriteLine("Type 'exit' any time to return to the main menu...");
                                Console.Write("Enter name: ");
                                filteringCase = Console.ReadLine();
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
            Console.Clear();
            string name, author, category, language, iSBN;
            DateTime publicationDate;
            bool parsed;

            Console.WriteLine("Type 'exit' any time to return to the main menu...");

            Console.Write("Enter a name of the book: ");
            name = Console.ReadLine();
            if (name == "exit") { return; }
            while (name.Length == 0) // Length vallidation
            {
                Console.Write("Entered name is not valid. Length cannot be zero. Please re-enter book's name: ");
                name = Console.ReadLine();
                if (name == "exit") { return; }
            }

            Console.Write("Enter an author of the book: ");
            author = Console.ReadLine();
            if (author == "exit") { return; }
            while (author.Length == 0) // Length vallidation
            {
                Console.Write("Entered author is not valid. Length cannot be zero. Please re-enter book's author: ");
                author = Console.ReadLine();
                if (author == "exit") { return; }
            }

            Console.Write("Enter book's category: ");
            category = Console.ReadLine();
            if (category == "exit") { return; }
            while (category.Length == 0) // Length vallidation
            {
                Console.Write("Entered category is not valid. Length cannot be zero. Please re-enter book's category: ");
                category = Console.ReadLine();
                if (category == "exit") { return; }
            }

            Console.Write("Enter book's language: ");
            language = Console.ReadLine();
            if (language == "exit") { return; }
            while (language.Length == 0) // Length vallidation
            {
                Console.Write("Entered language is not valid. Length cannot be zero. Please re-enter book's language: ");
                language = Console.ReadLine();
                if (language == "exit") { return; }
            }

            Console.Write("Enter book's publication year(number): ");
            string publicationYear = Console.ReadLine();
            if (publicationYear == "exit") { return; }
            parsed = int.TryParse(publicationYear, out int year);
            if (year > DateTime.Now.Year || year < 1) { parsed = false; }
            while (!parsed) // Correct year vallidation
            {
                Console.Write("Entered year is not valid. Please re-enter book's publication year: ");
                publicationYear = Console.ReadLine();
                if (publicationYear == "exit") { return; }
                parsed = int.TryParse(publicationYear, out year);
                if (year > DateTime.Now.Year || year < 1) { parsed = false; }
            };

            Console.Write("Enter book's publication month(number): ");
            string publicationMonth = Console.ReadLine();
            if (publicationMonth == "exit") { return; }
            parsed = int.TryParse(publicationMonth, out int month);
            if (month > 12 || month < 1) { parsed = false; }
            if (year == DateTime.Now.Year && month > DateTime.Now.Month) { parsed = false; }
            while (!parsed) // Correct month vallidation
            {
                Console.Write("Entered month is not valid. Please re-enter book's publication month: ");
                publicationMonth = Console.ReadLine();
                if (publicationMonth == "exit") { return; }
                parsed = int.TryParse(publicationMonth, out month);
                if (month > 12 || month < 1) { parsed = false; }
                if (year == DateTime.Now.Year && month > DateTime.Now.Month) { parsed = false; }
            };

            Console.Write("Enter book's publication day(number): ");
            string publicationDay = Console.ReadLine();
            if (publicationDay == "exit") { return; }
            parsed = int.TryParse(publicationDay, out int day);
            if (day > 31 || day < 1) { parsed = false; }
            if (year == DateTime.Now.Year && month == DateTime.Now.Month && day > DateTime.Now.Day) { parsed = false; }
            if ((month == 2 && day > 28) || ((month == 4 || month == 6 || month == 9 || month == 11) && day > 30)) { parsed = false; }
            while (!parsed)  // Correct day vallidation
            {
                Console.Write("Entered day is not valid. Please re-enter book's publication day: ");
                publicationDay = Console.ReadLine();
                if (publicationDay == "exit") { return; }
                parsed = int.TryParse(publicationDay, out day);
                if (day > 31 || day < 1) { parsed = false; }
                if (year == DateTime.Now.Year && month == DateTime.Now.Month && day > DateTime.Now.Day) { parsed = false; }
                if ((month == 2 && day > 28) || ((month == 4 || month == 6 || month == 9 || month == 11) && day > 30)) { parsed = false; }
            };
            publicationDate = new DateTime(year, month, day);

            Console.Write("Enter book's ISBN code: ");
            iSBN = Console.ReadLine();
            if (iSBN == "exit") { return; }
            List<string> allISBN = allBooks.GetAllISBN();
            while (allISBN.Contains(iSBN))  // Vallidation to see if this book already exists in the library (ISBN codes in my library version has to be unique)
            {
                Console.Write("Entered ISBN is not valid, becouse it already exists. Please re-enter book's ISBN code: ");
                iSBN = Console.ReadLine();
            };

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
            Console.Clear();
            ListBooks(allBooks, "available", "");
            List<string> availableISBN = allBooks.GetAvailableISBN();

            if (availableISBN.Count > 0) // Checks if there are available books to be taken
            {
                Console.CursorLeft = 0;

                Console.WriteLine("Type 'exit' any time to return to the main menu...");

                Console.Write("Enter desired book's ISBN code: ");
                string iSBN = Console.ReadLine();
                if (iSBN == "exit") { return; }
                while (!availableISBN.Contains(iSBN))   // Vallidation for available and correct ISBN code
                {
                    Console.Write("Entered ISBN is not valid. Please re-enter desired book's ISBN code: ");
                    iSBN = Console.ReadLine();
                    if (iSBN == "exit") { return; }
                };

                Console.Write("Enter your full name: ");
                string fullName = Console.ReadLine();
                if (fullName == "exit") { return; }
                while (fullName.Length == 0) // Length vallidation
                {
                    Console.Write("Entered name is not valid. Length cannot be zero. Please re-enter your full name: ");
                    fullName = Console.ReadLine();
                    if (fullName == "exit") { return; }
                }

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

                Console.Write("For how many days do you want to take this book? (You can only take a book for a maximum of 60 days): ");
                string toTakeDays = Console.ReadLine();
                if (toTakeDays == "exit") { return; }
                bool parsed = int.TryParse(toTakeDays, out int daysToTake);
                if (daysToTake > 60 || daysToTake < 0) { parsed = false; }
                while (!parsed) // Vallidation to see if a person is not trying to take book for more than two months
                {
                    Console.Write("Entered number is not valid. Please re-enter desired days number: ");
                    toTakeDays = Console.ReadLine();
                    if (toTakeDays == "exit") { return; }
                    parsed = int.TryParse(toTakeDays, out daysToTake);
                    if (daysToTake > 60 || daysToTake < 0) { parsed = false; }
                };

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

                Console.Write("Enter book's to return ISBN code: ");
                string iSBN = Console.ReadLine();
                if (iSBN == "exit") { return; }
                while (!takenISBN.Contains(iSBN))  // Vallidation for available and correct ISBN code
                {
                    Console.Write("Entered ISBN is not valid. Please re-enter desired book's ISBN code: ");
                    iSBN = Console.ReadLine();
                    if (iSBN == "exit") { return; }
                };

                TakingHistory lastEvent = allBooks.GetBookByISBN(iSBN).GetLastHistoryEvent();
                if(lastEvent != null)
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

                Console.Write("Enter book's to remove ISBN code: ");
                string iSBN = Console.ReadLine();
                if (iSBN == "exit") { return; }
                while (!allISBN.Contains(iSBN)) // Vallidation for available and correct ISBN code
                {
                    Console.Write("Entered ISBN is not valid. Please re-enter desired book's ISBN code: ");
                    iSBN = Console.ReadLine();
                    if (iSBN == "exit") { return; }
                };

                allBooks.DeleteBookByISBN(iSBN);

                Console.Clear();
                Console.WriteLine("Book deleted succesfuly. Press any key to continue...");
                Console.ReadKey();
            }
        }
    }
}
