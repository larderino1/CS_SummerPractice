﻿using DbManager;
using DbManager.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend_Order.Services
{
    //order service, which helps controlling order entity in db
    public class OrderService : IOrderService
    {
        private readonly AzureSqlDbContext context;

        public OrderService(AzureSqlDbContext context)
        {
            this.context = context;
        }

        public async Task CreateOrder(Order order)
        {
            context.Orders.Add(order);
            await context.SaveChangesAsync();
        }

        public async Task DeleteOrder(Guid orderId)
        {
            var order = await context.Orders.FirstOrDefaultAsync(id => id.Id.Equals(orderId));
            context.Orders.Remove(order);
            await context.SaveChangesAsync();
        }

        public async Task<List<Order>> GetAllOrders(string supplierId)
        {
            return await context.Orders.Where(id => id.SupplierId.Equals(supplierId)).ToListAsync();
        }

        public async Task<Order> GetOrderById(Guid orderId)
        {
            return await context.Orders.Where(id => id.Id.Equals(orderId)).FirstOrDefaultAsync();
        }

        public async Task UpdateOrder(Guid orderId, string itemName, int quantity, double price)
        {
            var order = await context.Orders.Where(id => id.Id.Equals(orderId)).FirstOrDefaultAsync();
            context.Orders.Update(order);
            order.ItemName = itemName;
            order.Quantity = quantity;
            order.Price = price;
            await context.SaveChangesAsync();
        }
    }
}
