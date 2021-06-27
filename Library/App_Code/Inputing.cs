using System;
using System.Collections.Generic;

namespace Library.App_Code
{
    /// <summary>
    /// Class that store the methods for inputing information into the console
    /// </summary>
    class Inputing
    {
        /// <summary>
        /// Method to input non empty string
        /// </summary>
        /// <param name="message">Message to show at the beggining</param>
        /// <param name="inputing">The name of what we are inputing</param>
        /// <returns>Inputed string</returns>
        public static string InputNonEmptyString(string message, string inputing)
        {
            Console.Write(message);
            string input = Console.ReadLine();
            if (input == "exit") { return input; }
            while (input.Length == 0) // Length vallidation
            {
                Console.Write($"Entered {inputing} is not valid. Length cannot be zero. Please re-enter: ");
                input = Console.ReadLine();
                if (input == "exit") { return input; }
            }
            return input;
        }

        /// <summary>
        /// Method to input book's publication year
        /// </summary>
        /// <returns>Inputed year or -1 if failed</returns>
        public static int InputPublicationYear()
        {
            Console.Write("Enter book's publication year(number): ");
            string publicationYear = Console.ReadLine();
            if (publicationYear == "exit") { return -1; }
            bool parsed = int.TryParse(publicationYear, out int year);
            if (year > DateTime.Now.Year || year < 1) { parsed = false; }
            while (!parsed) // Correct year vallidation
            {
                Console.Write("Entered year is not valid. Please re-enter book's publication year: ");
                publicationYear = Console.ReadLine();
                if (publicationYear == "exit") { return -1; }
                parsed = int.TryParse(publicationYear, out year);
                if (year > DateTime.Now.Year || year < 1) { parsed = false; }
            };
            return year;
        }

        /// <summary>
        /// Method to input book's publication month
        /// </summary>
        /// <param name="year">Year to watch</param>
        /// <returns>Inputed month or -1 if failed</returns>
        public static int InputPublicationMonth(int year)
        {
            Console.Write("Enter book's publication month(number): ");
            string publicationMonth = Console.ReadLine();
            if (publicationMonth == "exit") { return -1; }
            bool parsed = int.TryParse(publicationMonth, out int month);
            if (month > 12 || month < 1) { parsed = false; }
            if (year == DateTime.Now.Year && month > DateTime.Now.Month) { parsed = false; }
            while (!parsed) // Correct month vallidation
            {
                Console.Write("Entered month is not valid. Please re-enter book's publication month: ");
                publicationMonth = Console.ReadLine();
                if (publicationMonth == "exit") { return -1; }
                parsed = int.TryParse(publicationMonth, out month);
                if (month > 12 || month < 1) { parsed = false; }

            }
            return month;
        }

        /// <summary>
        /// Method to input book's publication day
        /// </summary>
        /// <param name="year">Year to watch</param>
        /// <param name="month">Month to watch</param>
        /// <returns>Inputed day or -1 if failed</returns>
        public static int InputPublicationDay(int year, int month)
        {
            Console.Write("Enter book's publication day(number): ");
            string publicationDay = Console.ReadLine();
            if (publicationDay == "exit") { return -1; }
            bool parsed = int.TryParse(publicationDay, out int day);
            if (day > 31 || day < 1) { parsed = false; }
            if (year == DateTime.Now.Year && month == DateTime.Now.Month && day > DateTime.Now.Day) { parsed = false; }
            if ((month == 2 && day > 28) || ((month == 4 || month == 6 || month == 9 || month == 11) && day > 30)) { parsed = false; }
            while (!parsed)  // Correct day vallidation
            {
                Console.Write("Entered day is not valid. Please re-enter book's publication day: ");
                publicationDay = Console.ReadLine();
                if (publicationDay == "exit") { return -1; }
                parsed = int.TryParse(publicationDay, out day);
                if (day > 31 || day < 1) { parsed = false; }
                if (year == DateTime.Now.Year && month == DateTime.Now.Month && day > DateTime.Now.Day) { parsed = false; }
                if ((month == 2 && day > 28) || ((month == 4 || month == 6 || month == 9 || month == 11) && day > 30)) { parsed = false; }
            };
            return day;
        }

        /// <summary>
        /// Method to input eligable ISBN code
        /// </summary>
        /// <param name="iSBNList">List of all ISBN code to check</param>
        /// <param name="shouldExistInList">Param to know if entered ISBN code should exist in the given list or not</param>
        /// <param name="inputMessage">Message to display</param>
        /// <returns>Inputed string</returns>
        public static string InputISBN(List<string> iSBNList, bool shouldExistInList, string inputMessage)
        {
            Console.Write($"Enter book's {inputMessage} ISBN code: ");
            string iSBN = Console.ReadLine();
            while (iSBN.Length == 0) // Length vallidation
            {
                Console.Write($"Entered ISBN code is not valid. Length cannot be zero. Please re-enter: ");
                iSBN = Console.ReadLine();
            }
            if (iSBN == "exit") { return iSBN; }
            while (iSBNList.Contains(iSBN) != shouldExistInList)  // Vallidation to see if this book already exists in the library (ISBN codes in my library version has to be unique)
            {
                Console.Write("Entered ISBN is not valid. Please re-enter book's ISBN code: ");
                iSBN = Console.ReadLine();
                while (iSBN.Length == 0) // Length vallidation
                {
                    Console.Write($"Entered ISBN code is not valid. Length cannot be zero. Please re-enter: ");
                    iSBN = Console.ReadLine();
                }
                if (iSBN == "exit") { return iSBN; }
            };
            return iSBN;
        }

        /// <summary>
        /// Method to input the amount of days the person is taking the book
        /// </summary>
        /// <returns>Inputed days amount or -1 if failed</returns>
        public static int InputDaysToTake()
        {
            Console.Write("For how many days do you want to take this book? (You can only take a book for a maximum of 60 days): ");
            string toTakeDays = Console.ReadLine();
            if (toTakeDays == "exit") { return -1; }
            bool parsed = int.TryParse(toTakeDays, out int daysToTake);
            if (daysToTake > 60 || daysToTake < 0) { parsed = false; }
            while (!parsed) // Vallidation to see if a person is not trying to take book for more than two months
            {
                Console.Write("Entered number is not valid. Please re-enter desired days number: ");
                toTakeDays = Console.ReadLine();
                if (toTakeDays == "exit") { return -1; }
                parsed = int.TryParse(toTakeDays, out daysToTake);
                if (daysToTake > 60 || daysToTake < 0) { parsed = false; }
            };
            return daysToTake;
        }
    }
}
