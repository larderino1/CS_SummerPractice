using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend_Product.Services.ProductsService;
using DbManager.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Backend_Product.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IConfiguration config)
        {
            _productService = new ProductService(config);
        }

        // GET: api/Product
        [HttpGet]
        public async Task<List<ShopItem>> Get()
        {
            return await _productService.GetAll();
        }

        [HttpGet("{id}")]
        public async Task<ShopItem> Get(Guid id)
        {
            return await _productService.GetById(id);
        }

        // GET: api/Product/{category}
        [HttpGet("category", Name = "Get")]
        public async Task<List<ShopItem>> GetByCategory([FromRoute]string category)
        {
            return await _productService.GetProductsByCategory(category);
        }

        // POST: api/Product
        [HttpPost]
        public async Task Post([FromBody] ShopItem product)
        {
            await _productService.CreateProduct(product.Name, product.Description, product.Price, product.Image, product.CategoryId, product.SupplierId);
        }

        //use only by supplier
        [HttpPut("{id}")]
        public async Task Put(Guid id, [FromBody] ShopItem item)
        {
            await _productService.UpdateProduct(id, item.Name, item.Description, item.Price, item.Image, item.CategoryId);
        }

        //use only by supplier
        // DELETE: api/ApiWithActions/{guid}
        [HttpDelete("{id}")]
        public async Task Delete(Guid id)
        {
            await _productService.DeleteProduct(id);
        }
    }
}
