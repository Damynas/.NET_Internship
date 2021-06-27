using FluentAssertions;
using Library.App_Code;
using System;
using System.Collections.Generic;
using Xunit;

namespace Library.Tests
{
    public class BooksRegisterTests
    {
        [Fact]
        public void AddBook_AddSameBookTwoTimes_ShouldBeAddedOnlyOneToBooksRegister()
        {
            //Arange
            BooksRegister booksRegister = new();
            Book book = new("NewBook", "Me", "Fantasy novel", "Native", new DateTime(2021, 4, 4), "948-34-4");
            //Act
            if (!booksRegister.Contains(book)) { booksRegister.AddBook(book); }
            if (!booksRegister.Contains(book)) { booksRegister.AddBook(book); }
            //Assert
            booksRegister.Count().Should().Be(1);
            booksRegister.GetBookByISBN(book.ISBN).Should().Be(book);
        }

        [Fact]
        public void GetBookByISBN_GetBookFromRegister_ShouldGetTheSameBook()
        {
            //Arange
            BooksRegister booksRegister = new();
            Book book = new("NewBook", "Me", "Fantasy novel", "Native", new DateTime(2021, 4, 4), "948-34-4");
            //Act
            booksRegister.AddBook(book);
            //Assert
            booksRegister.GetBookByISBN(book.ISBN).Should().Be(book);
            booksRegister.GetBookByISBN(book.ISBN).ISBN.Should().Be(book.ISBN);
            booksRegister.GetBookByISBN(book.ISBN).ISBN.Should().Be("948-34-4");
            booksRegister.GetBookByISBN(book.ISBN).Name.Should().Be(book.Name);
            booksRegister.GetBookByISBN(book.ISBN).Name.Should().Be("NewBook");
        }

        [Fact]
        public void GetISBNList_TestAllVariants_ShouldGetSpecifiedISBNLists()
        {
            //Arange
            BooksRegister booksRegister = new();
            List<string> allISBN = new();
            List<string> availableISBN = new();
            List<string> takenISBN = new();
            Book book1 = new("NewBook", "Me", "Fantasy novel", "Native", new DateTime(2021, 4, 4), "948-34-4");
            Book book2 = new("NewBook", "You", "Fantasy novel", "Native", new DateTime(2021, 4, 4), "436-34-4");
            Book book3 = new("NewBook", "He", "Fantasy novel", "Native", new DateTime(2021, 4, 4), "998-94-8");
            book1.IsTaken = true;
            //Act
            booksRegister.AddBook(book1);
            booksRegister.AddBook(book2);
            booksRegister.AddBook(book3);

            allISBN.Add(book1.ISBN);
            allISBN.Add(book2.ISBN);
            allISBN.Add(book3.ISBN);

            availableISBN.Add(book2.ISBN);
            availableISBN.Add(book3.ISBN);

            takenISBN.Add(book1.ISBN);
            //Assert
            booksRegister.GetAllISBN().Should().Equal(allISBN);
            booksRegister.GetAllISBN().Count.Should().Be(3);
            booksRegister.GetAvailableISBN().Should().Equal(availableISBN);
            booksRegister.GetAvailableISBN().Count.Should().Be(2);
            booksRegister.GetTakenISBN().Should().Equal(takenISBN);
            booksRegister.GetTakenISBN().Count.Should().Be(1);
        }

        [Fact]
        public void DeleteBookByISBN_DeleteBookFromRegister_ShouldRemoveABookBySpecifiedISBN()
        {
            //Arange
            BooksRegister booksRegister = new();
            Book book1 = new("NewBook", "Me", "Fantasy novel", "Native", new DateTime(2021, 4, 4), "948-34-4");
            Book book2 = new("NewBook", "You", "Fantasy novel", "Native", new DateTime(2021, 4, 4), "436-34-4");
            Book book3 = new("NewBook", "He", "Fantasy novel", "Native", new DateTime(2021, 4, 4), "998-94-8");
            book1.IsTaken = true;
            //Act
            booksRegister.AddBook(book1);
            booksRegister.AddBook(book2);
            booksRegister.AddBook(book3);

            booksRegister.DeleteBookByISBN(book2.ISBN);
            //Assert
            booksRegister.Count().Should().Be(2);
            booksRegister.GetAllISBN().Should().NotContain(book2.ISBN);
        }

        [Fact]
        public void Filter_FiltersAllBooksBySpecificCriteria_ShouldGetFilteredListsAndTestAllVariants()
        {
            //Arange
            BooksRegister booksRegister = new();
            Book book1 = new("NewBook1", "Me", "Fantasy novel", "Spanish", new DateTime(2021, 4, 4), "948-34-4");
            Book book2 = new("NewBook2", "You", "Documentary", "Native", new DateTime(2021, 4, 4), "436-34-4");
            Book book3 = new("NewBook3", "He", "Fantasy novel", "English", new DateTime(2021, 4, 4), "998-94-8");
            book1.IsTaken = true;
            //Act
            booksRegister.AddBook(book1);
            booksRegister.AddBook(book2);
            booksRegister.AddBook(book3);
            //Assert
            booksRegister.Filter("allBooks", "").Count.Should().Be(3);
            booksRegister.Filter("author", "Me").Count.Should().Be(1);
            booksRegister.Filter("category", "fantasy novel").Count.Should().Be(2);
            booksRegister.Filter("language", "native").Count.Should().Be(1);
            booksRegister.Filter("iSBN", "998-94-8").Count.Should().Be(1);
            booksRegister.Filter("name", "NewBook3").Count.Should().Be(1);
            booksRegister.Filter("taken", "").Count.Should().Be(1);
            booksRegister.Filter("available", "").Count.Should().Be(2);
        }
    }
}