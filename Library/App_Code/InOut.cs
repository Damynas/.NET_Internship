using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace Library.App_Code
{
    /// <summary>
    /// Class that handles data reading and writing with JSON files
    /// </summary>
    public class InOut
    {
        /// <summary>
        /// Method that saves information about books in a JSON file
        /// </summary>
        /// <param name="fileName">The name of the data file</param>
        /// <param name="allBooks">Books register that holds all the books</param>
        public static void SaveBookData(string fileName, BooksRegister allBooks)
        {
            List<Book> books = allBooks.Filter("allBooks", "");

            using (StreamWriter writer = new(fileName))
            using (JsonWriter jsonWriter = new JsonTextWriter(writer))
            {
                JsonSerializer serializer = new();
                serializer.Serialize(jsonWriter, books);
            }
        }

        /// <summary>
        /// Method that reads saved data about the books
        /// </summary>
        /// <param name="fileName">The name of the data file</param>
        /// <returns>Returns library register with already stored books</returns>
        public static BooksRegister ReadBookData(string fileName)
        {
            List<Book> books;

            using (StreamReader reader = new(fileName))
            {
                JsonSerializer deSerializer = new();
                books = (List<Book>)deSerializer.Deserialize(reader, typeof(List<Book>));
            }

            BooksRegister allBooks = new(books);
            return allBooks;
        }

        /// <summary>
        /// Method that saves information about people who have taken the books in a JSON file
        /// </summary>
        /// <param name="fileName">The name of the data file</param>
        /// <param name="peopleData">Dictionary that holds data about how many books each person has taken</param>
        public static void SavePeopleData(string fileName, Dictionary<string, int> peopleData)
        {
            using (StreamWriter writer = new(fileName))
            using (JsonWriter jsonWriter = new JsonTextWriter(writer))
            {
                JsonSerializer serializer = new();
                serializer.Serialize(jsonWriter, peopleData);
            }
        }

        /// <summary>
        /// Method that reads saved data about the people who have taken books
        /// </summary>
        /// <param name="fileName">The name of the data file</param>
        /// <returns>Returns dictionary that holds data about how many books each person has taken</returns>
        public static Dictionary<string, int> ReadPeopleData(string fileName)
        {
            Dictionary<string, int> peopleData;

            using (StreamReader reader = new(fileName))
            {
                JsonSerializer deSerializer = new();
                peopleData = (Dictionary<string, int>)deSerializer.Deserialize(reader, typeof(Dictionary<string, int>));
            }

            return peopleData;
        }
    }
}
