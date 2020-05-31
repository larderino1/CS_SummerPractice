using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend_Category.Services.CategoryService;
using DbManager.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Backend_Category.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class CategoryController : ControllerBase
    {

        private readonly ICategoryService _categoryService;

        public CategoryController(IConfiguration config)
        {
            _categoryService = new CategoryService(config);
        }

        // GET: api/Category
        [HttpGet]
        public async Task<List<Category>> Get()
        {
            return await _categoryService.GetAll();
        }

        [HttpGet("{id}")]
        public async Task<Category> Get(Guid id)
        {
            return await _categoryService.GetCategory(id);
        }

        // POST: api/Category
        [HttpPost]
        public async Task Post([FromBody]Category category)
        {
            await _categoryService.CreateCategory(category.Name, category.Description);
        }

        [HttpPut("{id}")]
        public async Task Put(Guid id, [FromBody] Category category)
        {
            await _categoryService.UpdateCategory(id, category.Name, category.Description);
        }

        // DELETE: api/ApiWithActions/{guid}
        [HttpDelete("{id}")]
        public async Task Delete(Guid id)
        {
            await _categoryService.DeleteCategory(id);
        }
    }
}
