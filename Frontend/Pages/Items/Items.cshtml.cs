using Frontend.Services.CategoryService;
using Frontend.Services.ItemService;
using DbManager.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Frontend.Pages.ItemPages
{
    public class ItemsModel : PageModel
    {
        private readonly IItemService service;

        private readonly ICategoryService categoryService;

        public readonly UserManager<IdentityUser> _userManager;

        public ItemsModel(IItemService service, ICategoryService categoryService, UserManager<IdentityUser> user)
        {
            this.service = service;
            this.categoryService = categoryService;
            _userManager = user;
        }

        public Guid LoggedUserId { get; set; }

        public IList<ShopItem> ShopItem { get; set; }

        public async Task OnGetAsync()
        {
            ShopItem = await service.GetAllItems();
            foreach(var item in ShopItem)
            {
                item.Category = await categoryService.GetCategoryById(item.CategoryId);
            }
            LoggedUserId = new Guid(_userManager.GetUserId(User));
        }
    }
}
