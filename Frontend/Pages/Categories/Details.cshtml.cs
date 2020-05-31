using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using Newtonsoft.Json;
using DbManager.Models;
using Frontend.Services.CategoryService;
using Microsoft.AspNetCore.Authorization;

namespace Frontend.Pages.Categories
{
    [Authorize(Roles = "Administrator")]
    public class DetailsModel : PageModel
    {
        private readonly ICategoryService service;

        public DetailsModel(ICategoryService service)
        {
            this.service = service;
        }

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
    }
}
