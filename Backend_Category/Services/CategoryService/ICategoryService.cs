
using DbManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend_Category.Services.CategoryService
{
    public interface ICategoryService
    {
        Task<List<Category>> GetAll();

        Task<Category> GetCategory(Guid? categoryId);

        Task CreateCategory(string name, string description);

        Task UpdateCategory(Guid categoryId, string name, string description);

        Task DeleteCategory(Guid categoryId);
    }
}
