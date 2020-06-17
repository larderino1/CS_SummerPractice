using DbManager;
using DbManager.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend_Order.Services
{
    public class OrderService : IOrderService
    {
        private readonly AzureSqlDbContext context;

        public OrderService(AzureSqlDbContext context)
        {
            this.context = context;
        }
        public async Task CreateOrder(string itemName, int quantity, double price, string userId, string supplierId)
        {
            await context.Orders.AddAsync(new Order(itemName, price, quantity, userId, supplierId));
            await context.SaveChangesAsync();
        }

        public async Task DeleteOrder(Guid orderId)
        {
            var order = await context.Orders.FirstOrDefaultAsync(id => id.Id.Equals(orderId));
            context.Orders.Remove(order);
            await context.SaveChangesAsync();
        }

        public Task<List<Order>> GetAllOrders()
        {
            throw new NotImplementedException();
        }

        public Task<Order> GetOrderById()
        {
            throw new NotImplementedException();
        }

        public Task UpdateOrder()
        {
            throw new NotImplementedException();
        }
    }
}
