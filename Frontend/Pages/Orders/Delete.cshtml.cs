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
using Microsoft.AspNetCore.Identity;

namespace Frontend.Pages.Orders
{
    public class DeleteModel : PageModel
    {
        private readonly AzureSqlDbContext _context;

        private readonly IOrderService orderService;

        private readonly UserManager<IdentityUser> userManager;

        public DeleteModel(AzureSqlDbContext context, UserManager<IdentityUser> userManager, IOrderService orderService)
        {
            _context = context;
            this.orderService = orderService;
            this.userManager = userManager;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Order = await orderService.GetOrderById(id);
            var orderUser = await userManager.FindByIdAsync(Order.UserId);
            Order.UserName = orderUser.UserName;

            if (Order != null)
            {
                await orderService.DeleteOrderById(id);
            }

            return RedirectToPage("./Index");
        }
    }
}
