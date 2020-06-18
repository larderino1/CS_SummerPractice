using DbManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Frontend.Services.OrderService
{
    public interface IOrderService
    {
        Task<List<Order>> GetAllOrders(string id);

        Task<Order> GetOrderById(Guid? id);

        Task UpdateOrder(Order order);

        Task DeleteOrderById(Guid? id);
    }
}
