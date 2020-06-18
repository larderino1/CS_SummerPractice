using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DbManager;
using DbManager.Models;
using Frontend.Services.OrderService;

namespace Frontend.Pages.Orders
{
    public class DetailsModel : PageModel
    {
        private readonly AzureSqlDbContext _context;

        private readonly IOrderService orderService;

        public DetailsModel(AzureSqlDbContext context, IOrderService orderService)
        {
            _context = context;
            this.orderService = orderService;
        }

        public Order Order { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Order = await orderService.GetOrderById(id);

            if (Order == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
