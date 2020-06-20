
using DbManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend_Product.Services.ProductsService
{
    interface IProductService
    {
        public Task<List<ShopItem>> GetAll();

        public Task<ShopItem> GetById(Guid id);

        public Task<List<ShopItem>> GetProductsByCategory(string category);

        public Task CreateProduct(string name, string description, double price, string image, Guid categoryId, string userId);

        public Task UpdateProduct(Guid id, string name, string description, double price, string image, Guid categoryId);

        public Task DeleteProduct(Guid id);
    }
}
