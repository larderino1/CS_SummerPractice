using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using DbManager.Models;
using Frontend.Services.CategoryService;
using Microsoft.AspNetCore.Authorization;

namespace Frontend.Pages.Categories
{
    [Authorize(Roles = "Administrator")]
    public class CreateModel : PageModel
    {
        private readonly ICategoryService service;

        public CreateModel(ICategoryService service)
        {
            this.service = service;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Category Category { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await service.CreateCategory(Category);

            return RedirectToPage("./Categories");
        }
    }
}
