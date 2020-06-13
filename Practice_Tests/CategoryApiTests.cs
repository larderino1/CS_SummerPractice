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
            var categoryId = new Guid("944ec12e-b5dd-48f2-b898-90a82be1c638");
            var category = await categoryService.GetCategoryById(categoryId);
            Assert.IsTrue(category.Name.Equals("computers"));
        }

        [TestMethod]
        public async Task DeleteCategory()
        {
            var categoryId = new Guid("e31e20b8-de4e-4099-b74d-0927c946872c");
            var category = await categoryService.GetCategoryById(categoryId);

            await categoryService.DeleteCategory(categoryId);
            Assert.IsNull(await categoryService.GetCategoryById(categoryId));

            await categoryService.CreateCategory(category);
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

            Assert.IsTrue(categories.FirstOrDefault(name => name.Name.Equals("test category")).Description.Equals(category.Description));
        }

        [TestMethod]
        public async Task UpdateCategory()
        {
            var category = await categoryService.GetCategoryById(new Guid("6dff5cc6-056f-46d0-a6b1-25471651cf2f"));
            category.Name = "Fruits";
            await categoryService.UpdateCategory(category);
            var categoryUpdated = await categoryService.GetCategoryById(new Guid("6dff5cc6-056f-46d0-a6b1-25471651cf2f"));
            Assert.IsTrue(categoryUpdated.Name.Equals("Fruits"));
            categoryUpdated.Name = "Computer Details";
            await categoryService.UpdateCategory(categoryUpdated);
        }

    }
}
