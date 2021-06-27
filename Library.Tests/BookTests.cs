using FluentAssertions;
using Library.App_Code;
using System;
using Xunit;

namespace Library.Tests
{
    public class BookTests
    {
        [Fact]
        public void AddHistoryEvent_AddNewTakingHistoryEvent_ShouldAddHistoryEventToBookObject()
        {
            //Arange
            Book book = new("NewBook", "Me", "Fantasy novel", "Native", new DateTime(2021, 4, 4), "948-34-4");
            TakingHistory historyEvent1 = new("Petras Petraitis", 45, DateTime.Now);
            TakingHistory historyEvent2 = new("Jonas Petraitis", 15, DateTime.Now);
            //Act
            book.AddHistoryEvent(historyEvent1);
            book.AddHistoryEvent(historyEvent2);
            //Assert
            book.HistoryEventCount().Should().Be(2);
        }

        [Fact]
        public void GetLastHistoryEvent_GetTheLastEventAddedToHistory_ShouldGetExactlyTheLast()
        {
            //Arange
            Book book = new("NewBook", "Me", "Fantasy novel", "Native", new DateTime(2021, 4, 4), "948-34-4");
            TakingHistory historyEvent1 = new("Petras Petraitis", 45, DateTime.Now);
            TakingHistory historyEvent2 = new("Jonas Petraitis", 15, DateTime.Now);
            TakingHistory historyEvent3 = new("Jonas Jonaitis", 78, DateTime.Now);
            //Act
            book.AddHistoryEvent(historyEvent1);
            book.AddHistoryEvent(historyEvent2);
            book.AddHistoryEvent(historyEvent3);
            //Assert
            book.HistoryEventCount().Should().Be(3);
            book.GetLastHistoryEvent().Should().BeSameAs(historyEvent3);
            book.GetLastHistoryEvent().Should().NotBeSameAs(historyEvent1);
            book.GetLastHistoryEvent().Should().NotBeSameAs(historyEvent2);
        }
    }
}
