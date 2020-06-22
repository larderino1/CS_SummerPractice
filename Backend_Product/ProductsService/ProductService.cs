using DbManager;
using DbManager.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend_Product.Services.ProductsService
{
    //order service, which helps controlling product entity in db
    public class ProductService : IProductService
    {
        private readonly AzureSqlDbContext context;
        public ProductService(AzureSqlDbContext context)
        {
            this.context = context;
        }

        public async Task<List<ShopItem>> GetAll()
        {
            return await context.ShopItems.ToListAsync(); ;
        }

        public async Task<ShopItem> GetById(Guid id)
        {
            return await context.ShopItems.Where(idProduct => idProduct.Id.Equals(id)).FirstOrDefaultAsync(); ;
        }

        public async Task<List<ShopItem>> GetProductsByCategory(string category)
        {
            return await context.ShopItems.Where(c => c.Category.Equals(category)).ToListAsync(); ;
        }

        public async Task CreateProduct(string name, string description, double price, string image, Guid categoryId, string userId)
        {
            await context.ShopItems.AddAsync(new ShopItem(name, description, price, image, categoryId, userId));
            await context.SaveChangesAsync();
        }

        public async Task UpdateProduct(Guid productId, string name, string description, double price, string image, Guid categoryId)
        {
            var product = await context.ShopItems.Where(id => id.Id.Equals(productId)).FirstAsync();
            context.ShopItems.Update(product);
            product.Name = name;
            product.Description = description;
            product.Price = price;
            product.Image = image;
            product.CategoryId = categoryId;
            await context.SaveChangesAsync();
        }

        public async Task DeleteProduct(Guid productId)
        {
            var product = await context.ShopItems.Where(id => id.Id.Equals(productId)).FirstAsync();
            context.ShopItems.Remove(product);
            await context.SaveChangesAsync();
        }
    }
}
