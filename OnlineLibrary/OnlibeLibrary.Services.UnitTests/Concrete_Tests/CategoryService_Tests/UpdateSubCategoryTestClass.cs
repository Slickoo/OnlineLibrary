using NUnit.Framework;
using OnlibeLibrary.Services.UnitTests.Concrete_Tests.Builders;
using OnlineLibrary.Services.Concrete;
using System;
using System.Collections.Generic;

namespace OnlibeLibrary.Services.UnitTests.Concrete_Tests.CategoryService_Tests
{
    [TestFixture]
    class UpdateSubCategoryTestClass
    {
        [Test]
        public void Should_UpdateSubCategory()
        {
            // Arrange.
            var _dbContext = new LibraryBuilder()
                .WithSubCategory(new SubCategoryBuilder()
                    .WithId(2)
                    .WithName("Martial Arts")
                    .WithCategory(new CategoryBuilder()
                        .WithId(2)
                        .WithName("Arts")
                        .WithSubcategory(new SubCategoryBuilder()
                            .WithId(1)
                            .WithName("Martial Arts")
                            .Build())
                        .Build())
                    .Build())
                .Build();

            var categoryService = new CategoryService(_dbContext);
            var subCategoryIdToUpdate = 2;
            var newSubCategoryName = "New Name";

            // Act.
            var updatedCategory = categoryService.UpdateSubCategory(subCategoryIdToUpdate, newSubCategoryName);

            // Assert.
            Assert.AreEqual(newSubCategoryName, updatedCategory.Name);
        }

        [Test]
        public void Should_ThrowArgumentException_Given_DuplicateSubcategoryNameInCategory()
        {
            // Arrange.
            var _dbContext = new LibraryBuilder()
                .WithSubCategory(new SubCategoryBuilder()
                    .WithId(2)
                    .WithName("Martial Arts")
                    .WithCategory(new CategoryBuilder()
                        .WithId(2)
                        .WithName("Arts")
                        .WithSubcategory(new SubCategoryBuilder()
                            .WithId(1)
                            .WithName("Martial Arts")
                            .Build())
                        .Build())
                    .Build())
                .Build();

            var categoryService = new CategoryService(_dbContext);
            var subCategoryId = 2;
            var newName = "Martial Arts";

            // Act.
            var sutDelegate = new TestDelegate(() =>
                categoryService.UpdateSubCategory(subCategoryId, newName));

            // Assert.
            Assert.Throws<ArgumentException>(sutDelegate);
        }

        [Test]
        [TestCase("")] // Empty name.
        [TestCase("New NameNew NameNew NameNew NameNew NameNew NameNew Name")] // Too long name.
        public void Should_ThrowArgumentException_Given_InvalidSubcategoryName(string newName)
        {
            var _dbContext = new LibraryBuilder()
                .WithSubCategory(new SubCategoryBuilder()
                    .WithId(2)
                    .WithName("Martial Arts")
                    .Build())
                .Build();

            // Arrange.
            var categoryService = new CategoryService(_dbContext);
            var subCategoryId = 2;

            // Act.
            var sutDelegate = new TestDelegate(() =>
                categoryService.UpdateSubCategory(subCategoryId, newName));

            // Assert.
            Assert.Throws<ArgumentException>(sutDelegate);
        }

        [Test]
        public void Should_ThrowKeyNotFoundException_Given_InvalidSubCategoryId()
        {
            var _dbContext = new LibraryBuilder()
                .WithSubCategory(new SubCategoryBuilder()
                    .WithId(2)
                    .WithName("Martial Arts")
                    .Build())
                .Build();

            // Arrange.
            var categoryService = new CategoryService(_dbContext);
            var subCategoryIdToUpdate = -1;
            var newSubCategoryName = "NewName";

            var testDelegate = new TestDelegate(() =>
                categoryService.UpdateSubCategory(subCategoryIdToUpdate, newSubCategoryName));

            // Assert.
            Assert.Throws<KeyNotFoundException>(testDelegate);
        }
    }
}
