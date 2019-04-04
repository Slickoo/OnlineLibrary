using OnlineLibrary.DataAccess.Entities;

namespace OnlibeLibrary.Services.UnitTests.Concrete_Tests.Builders
{
    public class SubCategoryBuilder
    {
        private SubCategory _subCategory;
        public SubCategoryBuilder()
        {
            _subCategory = new SubCategory();
        }

        public SubCategoryBuilder WithId(int id)
        {
            _subCategory.Id = id;
            return this;
        }

        public SubCategoryBuilder WithName(string name)
        {
            _subCategory.Name = name;
            return this;
        }

        public SubCategoryBuilder WithCategory(Category category)
        {
            _subCategory.Category = category;
            return this;
        }
        public SubCategory Build()
        {
            return _subCategory;
        }
    }
}