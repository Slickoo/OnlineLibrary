using OnlineLibrary.DataAccess.Entities;

namespace OnlibeLibrary.Services.UnitTests.Concrete_Tests.Builders
{
    public class CategoryBuilder
    {
        private Category _category;

        public CategoryBuilder()
        {
            _category = new Category();
        }

        public CategoryBuilder WithId(int id)
        {
            _category.Id = id;
            return this;
        }

        public CategoryBuilder WithName(string name)
        {
            _category.Name = name;
            return this;
        }

        public CategoryBuilder WithSubcategory(SubCategory subcategory)
        {
            _category.SubCategories.Add(subcategory);
            return this;
        }

        public Category Build()
        {
            return _category;
        }
    }
}