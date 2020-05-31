
using DbManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend_Category.Services.CategoryService
{
    interface ICategoryService
    {
        public Task<List<Category>> GetAll();

        public Task<Category> GetCategory(Guid? categoryId);

        public Task CreateCategory(string name, string description);

        public Task UpdateCategory(Guid categoryId, string name, string description);

        public Task DeleteCategory(Guid categoryId);
    }
}
