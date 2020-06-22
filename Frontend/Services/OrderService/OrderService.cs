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
    // service that handle requests to order api 
    public class OrderService : IOrderService
    {
        public async Task CreateOrder(Order order)
        {
            using (var client = new HttpClient())
            {
                var request = new HttpRequestMessage()
                {
                    RequestUri = new Uri($"https://backendorder.azurewebsites.net/api/order")
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
                    RequestUri = new Uri($"https://backendorder.azurewebsites.net/api/order/{id}")
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
                    RequestUri = new Uri($"https://backendorder.azurewebsites.net/api/order/supplier/{id}")
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
                    RequestUri = new Uri($"https://backendorder.azurewebsites.net/api/order/{id}")
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
                    RequestUri = new Uri($"https://backendorder.azurewebsites.net/api/order/{order.Id}")
                };
                var json = JsonConvert.SerializeObject(order);
                await client.PutAsync(request.RequestUri, new StringContent(json, Encoding.UTF8, "application/json"));
            }
        }
    }
}
