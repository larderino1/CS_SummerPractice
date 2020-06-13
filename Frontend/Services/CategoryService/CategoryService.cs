using DbManager.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Frontend.Services.CategoryService
{
    public class CategoryService : ICategoryService
    {
        public async Task CreateCategory(Category category)
        {
            using(var client = new HttpClient())
            {
                var request = new HttpRequestMessage
                {
                    RequestUri = new Uri("https://localhost:44304/api/category")
                };
                var json = JsonConvert.SerializeObject(category);
                await client.PostAsync(request.RequestUri, new StringContent(json, Encoding.UTF8, "application/json"));
            }
        }

        public async Task DeleteCategory(Guid? id)
        {
            using(var client = new HttpClient())
            {
                var request = new HttpRequestMessage
                {
                    RequestUri = new Uri($"https://localhost:44304/api/category/{id}")
                };
                await client.DeleteAsync(request.RequestUri);
            }
        }

        public async Task<List<Category>> GetAllCategories()
        {
            using (var client = new HttpClient())
            {
                var request = new HttpRequestMessage
                {
                    RequestUri = new Uri("https://localhost:44304/api/category")
                };
                var response = await client.GetAsync(request.RequestUri);
                var jsonResult = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<List<Category>>(jsonResult);
                return result;
            }
        }

        public async Task<Category> GetCategoryById(Guid? id)
        {
            using (var client = new HttpClient())
            {
                var request = new HttpRequestMessage
                {
                    RequestUri = new Uri($"https://localhost:44304/api/category/{id}")
                };
                var jsonResult = await client.GetAsync(request.RequestUri);
                var response = await jsonResult.Content.ReadAsStringAsync();
                var category = JsonConvert.DeserializeObject<Category>(response);
                return category;
            }
        }

        public async Task UpdateCategory(Category category)
        {
            using (var client = new HttpClient())
            {
                var request = new HttpRequestMessage
                {
                    RequestUri = new Uri($"https://localhost:44304/api/category/{category.Id}")
                };
                var json = JsonConvert.SerializeObject(category);
                await client.PutAsync(request.RequestUri, new StringContent(json, Encoding.UTF8, "application/json"));
            }
        }
    }
}
