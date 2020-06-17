using DbManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend_Order.Services
{
    public interface IOrderService
    {
        Task CreateOrder(string itemName, int quantity, double price, string userId, string supplierId);

        Task DeleteOrder(Guid orderId);

        Task UpdateOrder();

        Task<List<Order>> GetAllOrders();

        Task<Order> GetOrderById();
    }
}
