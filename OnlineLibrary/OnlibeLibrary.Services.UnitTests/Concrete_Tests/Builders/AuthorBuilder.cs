using System.Collections.Generic;
using OnlineLibrary.DataAccess.Entities;

namespace OnlibeLibrary.Services.UnitTests.Concrete_Tests.Builders
{
    public class AuthorBuilder
    {
        private Author _author;

        public AuthorBuilder()
        {
            _author = new Author();
        }

        public AuthorBuilder Default()
        {
            _author.Id = 3;
            _author.FirstName = "Brian";
            _author.LastName = "Goetz";
            _author.Books.Add(new BookBuilder().Default().Build());
            return this;
        }

        public AuthorBuilder WithId(int id)
        {
            _author.Id = id;
            return this;
        }

        public AuthorBuilder WithFirstName(string firstName)
        {
            _author.FirstName = firstName;
            return this;
        }

        public AuthorBuilder WithLastName(string lastName)
        {
            _author.LastName = lastName;
            return this;
        }

        public AuthorBuilder WithMiddleName(string middleName)
        {
            _author.MiddleName = middleName;
            return this;
        }

        public AuthorBuilder WithBook(Book book)
        {
            _author.Books.Add(book);
            return this;
        }
        public Author Build()
        {
            return _author;
        }
    }
}