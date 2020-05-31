using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using DbManager.Models;
using Frontend.Services.CategoryService;
using Microsoft.AspNetCore.Authorization;

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

        public IList<Category> Category { get;set; }

        public async Task OnGetAsync()
        {
            Category = await service.GetAllCategories();
        }
    }
}
