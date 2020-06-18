using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using DbManager.Models;
using Frontend.Services.CategoryService;
using Microsoft.AspNetCore.Authorization;

namespace Frontend.Pages.Categories
{
    [Authorize(Roles = "Administrator")]
    public class DeleteModel : PageModel
    {
        private readonly ICategoryService service;

        public DeleteModel(ICategoryService service)
        {
            this.service = service;
        }

        [BindProperty]
        public Category Category { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Category = await service.GetCategoryById(id);


            if (Category == null)
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

            Category = await service.GetCategoryById(id);

            if (Category != null)
            {
                await service.DeleteCategory(id);
            }

            return RedirectToPage("./Categories");
        }
    }
}
