using DbManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Frontend.Services.CategoryService
{
    public interface ICategoryService
    {
        Task<List<Category>> GetAllCategories();

        Task<Category> GetCategoryById(Guid? id);

        Task CreateCategory(Category category);

        Task DeleteCategory(Guid? id);

        Task UpdateCategory(Category category);
    }
}
