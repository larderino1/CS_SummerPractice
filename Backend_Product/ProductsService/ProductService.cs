
using Frontend.Data;
using DbManager.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;

namespace Backend_Product.Services.ProductsService
{
    public class ProductService : IProductService
    {
        private readonly IConfiguration config;

        public ProductService(IConfiguration config)
        {
           this.config = config;
        }

        public async Task<List<ShopItem>> GetAll()
        {
            var items = new List<ShopItem>();
            using(var db = new AzureSqlDbContext(config))
            {
                items = await db.ShopItems.ToListAsync();
            }
            return items;
        }

        public async Task<ShopItem> GetById(Guid id)
        {
            var item = new ShopItem();
            using (var db = new AzureSqlDbContext(config))
            {
                item = await db.ShopItems.Where(idProduct => idProduct.Id.Equals(id)).FirstOrDefaultAsync();
            }
            return item;
        }

        public async Task<List<ShopItem>> GetProductsByCategory(string category)
        {
            var items = new List<ShopItem>();
            using(var db = new AzureSqlDbContext(config))
            {
                items = await db.ShopItems.Where(c => c.Category.Equals(category)).ToListAsync();
            }
            return items;
        }

        public async Task CreateProduct(string name, string description, double price, string image, Guid categoryId, string userId)
        {
            using(var db = new AzureSqlDbContext(config))
            {
                await db.ShopItems.AddAsync(new ShopItem(name, description, price, image, categoryId, userId));
                await db.SaveChangesAsync();
            }
        }

        public async Task UpdateProduct(Guid productId, string name, string description, double price, string image, Guid categoryId)
        {
            using(var db = new AzureSqlDbContext(config))
            {
                var product = await db.ShopItems.Where(id => id.Id.Equals(productId)).FirstAsync();
                db.ShopItems.Update(product);
                product.Name = name;
                product.Description = description;
                product.Price = price;
                product.Image = image;
                product.CategoryId = categoryId;
                await db.SaveChangesAsync();
            }
        }

        public async Task DeleteProduct(Guid productId)
        {
            using(var db = new AzureSqlDbContext(config))
            {
                var product = await db.ShopItems.Where(id => id.Id.Equals(productId)).FirstAsync();
                db.ShopItems.Remove(product);
                await db.SaveChangesAsync();
            }
        }
    }
}
