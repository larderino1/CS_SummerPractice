using DbManager.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Frontend.Services.OrderService
{
    public class OrderService : IOrderService
    {
        public async Task CreateOrder(Order order)
        {
            using (var client = new HttpClient())
            {
                var request = new HttpRequestMessage()
                {
                    RequestUri = new Uri($"https://localhost:44329/api/order")
                };
                var orderJson = JsonConvert.SerializeObject(order);
                await client.PostAsync(request.RequestUri, new StringContent(orderJson, Encoding.UTF8, "application/json"));
            }
        }

        public async Task DeleteOrderById(Guid? id)
        {
            using (var client = new HttpClient())
            {
                var request = new HttpRequestMessage()
                {
                    RequestUri = new Uri($"https://localhost:44329/api/order/{id}")
                };
                await client.DeleteAsync(request.RequestUri);
            }
        }

        public async Task<List<Order>> GetAllOrders(string id)
        {
            using (var client = new HttpClient())
            {
                var request = new HttpRequestMessage()
                {
                    RequestUri = new Uri($"https://localhost:44329/api/order/{id}")
                };
                var response = await client.GetAsync(request.RequestUri);
                var jsonResult = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<List<Order>>(jsonResult);
                return result;
            }
        }

        public async Task<Order> GetOrderById(Guid? id)
        {
            using (var client = new HttpClient())
            {
                var request = new HttpRequestMessage()
                {
                    RequestUri = new Uri($"https://localhost:44329/api/order/{id}")
                };
                var response = await client.GetAsync(request.RequestUri);
                var jsonResult = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<Order>(jsonResult);
                return result;
            }
        }

        public async Task UpdateOrder(Order order)
        {
            using (var client = new HttpClient())
            {
                var request = new HttpRequestMessage()
                {
                    RequestUri = new Uri($"https://localhost:44329/api/order/{order.Id}")
                };
                var json = JsonConvert.SerializeObject(order);
                await client.PutAsync(request.RequestUri, new StringContent(json, Encoding.UTF8, "application/json"));
            }
        }
    }
}
