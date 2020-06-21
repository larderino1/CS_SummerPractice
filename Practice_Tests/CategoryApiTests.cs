using DbManager.Models;
using Frontend.Services.CategoryService;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Practice_Tests
{
    [TestClass]
    public class CategoryApiTests
    {
        private readonly CategoryService categoryService = new CategoryService();
        

        [TestMethod]
        public async Task GetAllCategories()
        {
            var categories = await categoryService.GetAllCategories();
            Assert.IsTrue(condition: categories.Any());
        }

        [TestMethod]
        public async Task GetCategoryById()
        {
            var category = await categoryService.GetCategoryById(new Guid("944ec12e-b5dd-48f2-b898-90a82be1c638"));
            Assert.IsNotNull(category);
        }

        [TestMethod]
        public async Task DeleteCategory()
        {
            var category = new Category()
            {
                Name = "test category",
                Description = "category for testing"
            };

            await categoryService.CreateCategory(category);

            var categories = await categoryService.GetAllCategories();

            var categoryFromList = categories.FirstOrDefault(name => name.Name.Equals(category.Name));

            await categoryService.DeleteCategory(categoryFromList.Id);
            Assert.IsNull(await categoryService.GetCategoryById(categoryFromList.Id));
        }

        [TestMethod]
        public async Task CreateCategory()
        {
            var category = new Category()
            {
                Name = "test category",
                Description = "category for testing"
            };

            await categoryService.CreateCategory(category);

            var categories = await categoryService.GetAllCategories();

            var categoryFromList = categories.FirstOrDefault(name => name.Name.Equals("test category"));

            Assert.IsTrue(categories.FirstOrDefault(name => name.Name.Equals("test category")).Description.Equals(category.Description));

            await categoryService.DeleteCategory(categoryFromList.Id);
        }

        [TestMethod]
        public async Task UpdateCategory()
        {
            var category = new Category()
            {
                Name = "test category",
                Description = "category for testing"
            };

            await categoryService.CreateCategory(category);

            var categories = await categoryService.GetAllCategories();

            var categoryFromList = categories.FirstOrDefault(name => name.Name.Equals("test category"));

            categoryFromList.Name = "Fruits";

            await categoryService.UpdateCategory(categoryFromList);

            var categoryUpdated = await categoryService.GetCategoryById(categoryFromList.Id);

            Assert.IsTrue(categoryUpdated.Name.Equals("Fruits"));

            await categoryService.DeleteCategory(categoryFromList.Id);
        }

    }
}
