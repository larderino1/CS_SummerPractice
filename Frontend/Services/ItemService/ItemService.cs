using DbManager.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Frontend.Services.ItemService
{
    // service that handle requests to product api 
    public class ItemService : IItemService
    {
        public async Task CreateItem(ShopItem item)
        {
            using(var client = new HttpClient())
            {
                var request = new HttpRequestMessage
                {
                    RequestUri = new Uri($"https://backendproduct.azurewebsites.net/api/product")
                };
                var json = JsonConvert.SerializeObject(item);
                await client.PostAsync(request.RequestUri, new StringContent(json, Encoding.UTF8, "application/json"));
            }
        }

        public async Task DeleteItemById(Guid? id)
        {
            using (var client = new HttpClient())
            {
                var request = new HttpRequestMessage
                {
                    RequestUri = new Uri($"https://backendproduct.azurewebsites.net/api/product/{id}")
                };
                await client.DeleteAsync(request.RequestUri);
            }
        }

        public async Task<List<ShopItem>> GetAllItems()
        {
            using (var client = new HttpClient())
            {
                var request = new HttpRequestMessage
                {
                    RequestUri = new Uri($"https://backendproduct.azurewebsites.net/api/product")
                };
                var response = await client.GetAsync(request.RequestUri);
                var jsonResult = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<List<ShopItem>>(jsonResult);
                return result;
            }
        }

        public async Task<ShopItem> GetItemById(Guid? id)
        {
            using (var client = new HttpClient())
            {
                var request = new HttpRequestMessage
                {
                    RequestUri = new Uri($"https://backendproduct.azurewebsites.net/api/product/{id}")
                };
                var jsonResult = await client.GetAsync(request.RequestUri);
                var response = await jsonResult.Content.ReadAsStringAsync();
                var item = JsonConvert.DeserializeObject<ShopItem>(response);
                return item;
            }
        }

        public async Task UpdateItem(ShopItem item)
        {
            using (var client = new HttpClient())
            {
                var request = new HttpRequestMessage
                {
                    RequestUri = new Uri($"https://backendproduct.azurewebsites.net/api/product/{item.Id}")
                };
                var json = JsonConvert.SerializeObject(item);
                await client.PutAsync(request.RequestUri, new StringContent(json, Encoding.UTF8, "application/json"));
            }
        }
    }
}
