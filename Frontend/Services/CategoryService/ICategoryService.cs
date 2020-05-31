using DbManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Frontend.Services.CategoryService
{
    public interface ICategoryService
    {
        public Task<List<Category>> GetAllCategories();

        public Task<Category> GetCategoryById(Guid? id);

        public Task CreateCategory(Category category);

        public Task DeleteCategory(Guid? id);

        public Task UpdateCategory(Category category);
    }
}
