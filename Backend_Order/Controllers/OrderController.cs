using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend_Order.Services;
using DbManager.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Backend_Order.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {

        private readonly IOrderService orderService;

        public OrderController(IOrderService orderService)
        {
            this.orderService = orderService;
        }

        // GET: api/Order
        [HttpGet("{supplierId}")]
        public async Task<List<Order>> Get(string supplierId)
        {
            return await orderService.GetAllOrders(supplierId);
        }

        // GET: api/Order/5
        [HttpGet("{id}", Name = "Get")]
        public async Task<Order> Get(Guid id)
        {
            return await orderService.GetOrderById(id);
        }

        // POST: api/Order
        [HttpPost]
        public async Task Post([FromBody] Order order)
        {
            await orderService.CreateOrder(order.ItemName, order.Quantity, order.Price, order.UserId, order.SupplierId);
        }

        // PUT: api/Order/5
        [HttpPut("{id}")]
        public async Task Put(Guid id, [FromBody] Order order)
        {
            await orderService.UpdateOrder(id, order.ItemName, order.Quantity, order.Price);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task Delete(Guid id)
        {
            await orderService.DeleteOrder(id);
        }
    }
}
