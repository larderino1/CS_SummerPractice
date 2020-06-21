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
    public class EditModel : PageModel
    {
        private readonly ICategoryService service;

        public EditModel(ICategoryService service)
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

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                await service.UpdateCategory(Category);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return RedirectToPage("./Categories");
        }
    }
}
