using System;
using OnlineLibrary.DataAccess.Entities;

namespace OnlibeLibrary.Services.UnitTests.Concrete_Tests.Builders
{
    public class BookBuilder
    {
        private Book _book;

        public BookBuilder()
        {
            _book= new Book();
        }

        public BookBuilder Default()
        {
            _book.Id = 1;
            _book.Title =
                "Learn C# in One Day and Learn It Well: C# for Beginners with Hands-on Project (Learn Coding Fast with Hands-On Project) (Volume 3)";
            _book.ISBN = "1518800270";
            _book.FrontCover = "~/Content/Images/Books/front-covers/1518800270.jpg";
            _book.Description =
                "If you are a beginning programmer wanting to learn C# .NET programming, this book is the perfect introduction. It is too basic for experienced programmers, but it is just perfect for someone just starting out with C# programming, since it is easy to follow and all the concepts are explained very well.";
            _book.PublishDate = new DateTime(2015, 10, 27);
            return this;
        }

        public BookBuilder WithId(int id)
        {
            _book.Id = id;
            return this;
        }

        public BookBuilder WithTitle(string title)
        {
            _book.Title = title;
            return this;
        }

        public BookBuilder WithFrontCover(string frontCover)
        {
            _book.FrontCover = frontCover;
            return this;
        }

        public BookBuilder WithISBN(string isbn)
        {
            _book.ISBN = isbn;
            return this;
        }

        public BookBuilder WithDescription(string description)
        {
            _book.Description = description;
            return this;
        }

        public BookBuilder WithPublishDate(DateTime publishDate)
        {
            _book.PublishDate = new DateTime(publishDate.Year,publishDate.Month,publishDate.Day);
            return this;
        }

        public Book Build()
        {
            return _book;
        }
    }
}