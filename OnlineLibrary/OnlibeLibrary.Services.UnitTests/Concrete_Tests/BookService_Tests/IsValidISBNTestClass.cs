using NUnit.Framework;
using OnlibeLibrary.Services.UnitTests.Concrete_Tests.Builders;
using OnlineLibrary.Services.Concrete;

namespace OnlibeLibrary.Services.UnitTests.Concrete_Tests.BookService_Tests
{
    [TestFixture]
    class IsValidISBNTestClass
    {
        [Test]
        public void Should_ReturnFalse_Given_ExistingISBN()
        {
            // Arrange.           
            var expectedResult = false;
            var _dbContext = new LibraryBuilder()
                                    .WithBook(new BookBuilder()
                                                    .WithISBN("1518800270")
                                                    .Build())
                                    .Build();

            var sut = new BookService(_dbContext, null);
            var ISBN = "1518800270";

            // Act.
            var returnedResult = sut.IsValidISBN(ISBN);

            // Assert.
            Assert.AreEqual(expectedResult, returnedResult);
        }

        [Test]
        public void Should_ReturnTrue_Given_UniqueISBN()
        {
            // Arrange.           
            var expectedResult = true;

            //var _dbContext = new LibraryBuilder().WithBook(new BookBuilder().WithISBN("1518800270").Build())
            //                                     .WithBook(new BookBuilder().WithISBN("1546898978").Build())
            //                                     .Build();

            var _dbContext = new LibraryBuilder().Build();
            var sut = new BookService(_dbContext, null);
            var ISBN = "1111111111";

            // Act.
            var returnedResult = sut.IsValidISBN(ISBN);

            // Assert.
            Assert.AreEqual(expectedResult, returnedResult);
        }

        [Test]
        public void Should_ReturnTrue_Given_DefaultUnknownISBN()
        {
            // Arrange.           
            var expectedResult = true;
            var _dbContext = new LibraryBuilder().Build();
            var sut = new BookService(_dbContext, null);
            var ISBN = OnlineLibrary.Common.Infrastructure.LibraryConstants.undefinedISBN;

            // Act.
            var returnedResult = sut.IsValidISBN(ISBN);

            // Assert.
            Assert.AreEqual(expectedResult, returnedResult);
        }

        [Test]
        public void Should_ReturnTrue_Given_NullISBN()
        {
            // Arrange.
            var expectedResult = true;
            var _dbContext = new LibraryBuilder().Build();
            var sut = new BookService(_dbContext, null);
            string ISBN = null;

            // Act.
            var returnedResult = sut.IsValidISBN(ISBN);

            // Assert.
            Assert.AreEqual(expectedResult, returnedResult);
        }
    }
}
