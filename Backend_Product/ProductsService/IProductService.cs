
using DbManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend_Product.Services.ProductsService
{
    public interface IProductService
    {
        Task<List<ShopItem>> GetAll();

        Task<ShopItem> GetById(Guid id);

        Task<List<ShopItem>> GetProductsByCategory(string category);

        Task CreateProduct(string name, string description, double price, string image, Guid categoryId, string userId);

        Task UpdateProduct(Guid id, string name, string description, double price, string image, Guid categoryId);

        Task DeleteProduct(Guid id);
    }
}
