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
        private readonly AzureSqlDbContext _context;

        private readonly UserManager<IdentityUser> userManager;

        private readonly IOrderService orderService;

        public OrdersModel(AzureSqlDbContext context, UserManager<IdentityUser> userManager, IOrderService orderService)
        {
            _context = context;
            this.userManager = userManager;
            this.orderService = orderService;
        }

        [BindProperty]
        public IList<Order> Order { get;set; }

        public async Task OnGetAsync()
        {
            var user = await userManager.FindByNameAsync(User.Identity.Name);
            Order = await orderService.GetAllOrders(user.Id);
        }
    }
}
