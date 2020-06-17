using DbManager;
using DbManager.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend_Category.Services.CategoryService
{
    public class CategoryService : ICategoryService
    {
        private readonly IConfiguration config;

        public CategoryService(IConfiguration config)
        {
            this.config = config;
        }

        public async Task<List<Category>> GetAll()
        {
            var categories = new List<Category>();
            using(var db = new AzureSqlDbContext(config))
            {
                if(db.Categories != null)
                {
                    categories = await db.Categories.ToListAsync();
                }
            }
            return categories;
        }

        public async Task<Category> GetCategory(Guid? categoryId)
        {
            using (var db = new AzureSqlDbContext(config))
            {
                var category = new Category();
                if(db.Categories != null)
                {
                    category = await db.Categories.Where(id => id.Id.Equals(categoryId)).FirstOrDefaultAsync();
                }
                return category;
            }
        }

        public async Task CreateCategory(string name, string description)
        {
            var category = new Category(name, description);
            using(var db = new AzureSqlDbContext(config))
            {
                if(!await db.Categories.ContainsAsync(category))
                {
                    await db.Categories.AddAsync(category);
                    await db.SaveChangesAsync();
                }
            }
        }

        public async Task UpdateCategory(Guid categoryId, string name, string description)
        {
            using(var db = new AzureSqlDbContext(config))
            {
                var category = await db.Categories.Where(id => id.Id.Equals(categoryId)).FirstAsync();
                db.Categories.Update(category);

                category.Name = name;
                category.Description = description;

                await db.SaveChangesAsync();
            }
        }

        public async Task DeleteCategory(Guid categoryId)
        {
            using(var db = new AzureSqlDbContext(config))
            {
                var category = await db.Categories.Where(id => id.Id.Equals(categoryId)).FirstAsync();
                db.Categories.Remove(category);
                await db.SaveChangesAsync();
            }
        }
    }
}
