using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using DbManager.Models;
using Frontend.Services.CategoryService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace Frontend.Pages.Categories
{
    [Authorize(Roles = "Administrator")]
    public class CategoriesModel : PageModel
    {
        private readonly ICategoryService service;
        public CategoriesModel(ICategoryService service)
        {
            this.service = service;
        }

        public IList<Category> Category { get; set; }
        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }

        public async Task OnGetAsync()
        {
            Category = await service.GetAllCategories();
            if (!string.IsNullOrEmpty(SearchString))
            {
                Category = Category.Where(s => s.Name.Contains(SearchString) || s.Description.Contains(SearchString)).ToList();
            }
        }
    }
}
