using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DbManager;
using DbManager.Models;
using Frontend.Services.OrderService;
using Microsoft.AspNetCore.Identity;

namespace Frontend.Pages.Orders
{
    public class EditModel : PageModel
    {
        private readonly AzureSqlDbContext _context;

        private readonly IOrderService orderService;

        private readonly UserManager<IdentityUser> userManager;

        public EditModel(AzureSqlDbContext context, UserManager<IdentityUser> userManager, IOrderService orderService)
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
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Order).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(Order.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Orders/Orders");
        }

        private bool OrderExists(Guid id)
        {
            return _context.Orders.Any(e => e.Id == id);
        }
    }
}
