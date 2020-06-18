using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DbManager;
using DbManager.Models;
using Frontend.Services.OrderService;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Frontend.Pages.Orders
{
    public class OrdersModel : PageModel
    {
        private readonly UserManager<IdentityUser> userManager;

        private readonly IOrderService orderService;

        public OrdersModel(UserManager<IdentityUser> userManager, IOrderService orderService)
        {
            this.userManager = userManager;
            this.orderService = orderService;
        }

        [BindProperty]
        public IList<Order> Order { get;set; }

        public async Task OnGetAsync()
        {
            var user = await userManager.FindByNameAsync(User.Identity.Name);
            var orders = await orderService.GetAllOrders(user.Id);
            foreach(var order in orders)
            {
                var orderUser = await userManager.FindByIdAsync(order.UserId);
                order.UserName = orderUser.UserName;
            }
            Order = orders;
        }
    }
}
