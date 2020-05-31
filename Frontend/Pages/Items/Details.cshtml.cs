using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;
using DbManager.Models;
using Frontend.Services.ItemService;
using Frontend.Services.CategoryService;
using Frontend.Services.AzureStorageService;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace Frontend.Pages.ItemPages
{
    public class DetailsModel : PageModel
    {
        private readonly IItemService service;

        private readonly ICategoryService categoryService;

        public readonly UserManager<IdentityUser> _userManager;

        public DetailsModel(IItemService service, ICategoryService categoryService, UserManager<IdentityUser> user)
        {
            this.service = service;
            this.categoryService = categoryService;
            _userManager = user;
        }

        public Guid LoggedUserId { get; set; }

        public ShopItem ShopItem { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ShopItem = await service.GetItemById(id);
            ShopItem.Category = await categoryService.GetCategoryById(ShopItem.CategoryId);
            LoggedUserId = new Guid(_userManager.GetUserId(User));

            if (ShopItem == null)
            {
                return NotFound();
            }
            return Page();
        }

    }
}
