using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using NSubstitute;
using OnlineLibrary.DataAccess.Abstract;
using OnlineLibrary.DataAccess.Entities;

namespace OnlibeLibrary.Services.UnitTests.Concrete_Tests.Builders
{
    public class LibraryBuilder
    {
        private ILibraryDbContext _library;
        private ICollection<Author> _authors = new List<Author>();
        private ICollection<Book> _books = new List<Book>();

        public LibraryBuilder WithBook(Book book)
        {
            _books.Add(book);
            return this;
        }

        public LibraryBuilder WithAuthor(Author author)
        {
            _authors.Add(author);
            return this;
        }

        public ILibraryDbContext Build()
        {
            var authorsAsQueryable = _authors.AsQueryable();
            var booksAsQueryable = _books.AsQueryable();
            var authorsSet = Substitute.For<DbSet<Author>, IQueryable<Author>>();
            ((IQueryable<Author>) authorsSet).Provider.Returns(authorsAsQueryable.Provider);
            ((IQueryable<Author>) authorsSet).Expression.Returns(authorsAsQueryable.Expression);
            ((IQueryable<Author>) authorsSet).ElementType.Returns(authorsAsQueryable.ElementType);
            ((IQueryable<Author>) authorsSet).GetEnumerator().Returns(authorsAsQueryable.GetEnumerator());

            var booksSet = Substitute.For<DbSet<Book>, IQueryable<Book>>();
            ((IQueryable<Book>) booksSet).Provider.Returns(booksAsQueryable.Provider);
            ((IQueryable<Book>) booksSet).Expression.Returns(booksAsQueryable.Expression);
            ((IQueryable<Book>) booksSet).ElementType.Returns(booksAsQueryable.ElementType);
            ((IQueryable<Book>) booksSet).GetEnumerator().Returns(booksAsQueryable.GetEnumerator());

            _library = Substitute.For<ILibraryDbContext>();
            _library.Authors.Returns(authorsSet);
            _library.Books.Returns(booksSet);
            return _library;
        }

    }
}