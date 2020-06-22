using DbManager;
using DbManager.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend_Category.Services.CategoryService
{
    public class CategoryService : ICategoryService
    {
        private readonly AzureSqlDbContext context;

        public CategoryService(AzureSqlDbContext context)
        {
            this.context = context;
        }

        public async Task<List<Category>> GetAll()
        {
            return await context.Categories.ToListAsync(); ;
        }

        public async Task<Category> GetCategory(Guid? categoryId)
        {
            return await context.Categories.Where(id => id.Id.Equals(categoryId)).FirstOrDefaultAsync();
        }

        public async Task CreateCategory(string name, string description)
        {
            await context.Categories.AddAsync(new Category(name, description));
            await context.SaveChangesAsync();
        }

        public async Task UpdateCategory(Guid categoryId, string name, string description)
        {
            var category = await context.Categories.Where(id => id.Id.Equals(categoryId)).FirstAsync();
            context.Categories.Update(category);

            category.Name = name;
            category.Description = description;

            await context.SaveChangesAsync();
        }

        public async Task DeleteCategory(Guid categoryId)
        {
            var category = await context.Categories.Where(id => id.Id.Equals(categoryId)).FirstAsync();
            context.Categories.Remove(category);
            await context.SaveChangesAsync();
        }
    }
}
