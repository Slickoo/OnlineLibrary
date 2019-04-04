using NSubstitute;
using OnlineLibrary.DataAccess.Abstract;
using OnlineLibrary.DataAccess.Entities;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace OnlibeLibrary.Services.UnitTests.Concrete_Tests.Builders
{
    public class LibraryBuilder
    {
        private ILibraryDbContext _library;
        private ICollection<Author> _authors = new List<Author>();
        private ICollection<Book> _books = new List<Book>();
        private ICollection<Category> _categories = new List<Category>();
        private ICollection<SubCategory> _subCategories = new List<SubCategory>();

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

        public LibraryBuilder WithCategory(Category category)
        {
            _categories.Add(category);
            return this;
        }

        public LibraryBuilder WithSubCategory(SubCategory subCategory)
        {
            _subCategories.Add(subCategory);
            return this;
        }

        public ILibraryDbContext Build()
        {
            var authorsSet = GetSet(_authors.AsQueryable());

            var booksSet = GetSet(_books.AsQueryable());

            var subcategoriesSet = GetSet<SubCategory>(_subCategories.AsQueryable());
            subcategoriesSet.Find(Arg.Any<object>()).Returns(callinfo =>
            {
                object[] idValues = callinfo.Arg<object[]>();
                if (idValues != null && idValues.Length == 1)
                {
                    int requestedId = (int)idValues[0];
                    return _subCategories.SingleOrDefault(c => c.Id == requestedId);
                }

                return null;
            });

            subcategoriesSet.Include(Arg.Any<string>()).Returns(subcategoriesSet);


            var categoriesSet = GetSet(_categories.AsQueryable());
            categoriesSet.Include(Arg.Any<string>()).Returns(categoriesSet);

            _library = Substitute.For<ILibraryDbContext>();
            _library.Authors.Returns(authorsSet);
            _library.Books.Returns(booksSet);
            _library.SubCategories.Returns(subcategoriesSet);
            _library.Categories.Returns(categoriesSet);
            return _library;
        }

        public DbSet<T> GetSet<T>(IQueryable<T> listAsQueryable) where T : class
        {
            var returnSet = Substitute.For<DbSet<T>, IQueryable<T>>();
            ((IQueryable<T>)returnSet).Provider.Returns(listAsQueryable.Provider);
            ((IQueryable<T>)returnSet).Expression.Returns(listAsQueryable.Expression);
            ((IQueryable<T>)returnSet).ElementType.Returns(listAsQueryable.ElementType);
            ((IQueryable<T>)returnSet).GetEnumerator().Returns(listAsQueryable.GetEnumerator());
            return returnSet;
        }

    }
}