using DbManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend_Order.Services
{
    public interface IOrderService
    {

        Task DeleteOrder(Guid orderId);

        Task UpdateOrder(Guid orderId, string itemName, int quantity, double price);

        Task<List<Order>> GetAllOrders(string supplierId);

        Task<Order> GetOrderById(Guid id);
    }
}
