using Frontend.Services.CategoryService;
using Frontend.Services.ItemService;
using DbManager.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Frontend.Pages.ItemPages
{
    public class ItemsModel : PageModel
    {
        private readonly IItemService service;

        private readonly ICategoryService categoryService;

        public readonly UserManager<IdentityUser> _userManager;
        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }
        public string CategoryId { get; set; }

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
            CategoryId = HttpContext.Request.Query["CategoryId"]; 
            ShopItem = await service.GetAllItems();
            foreach (var item in ShopItem)
            {
                item.Category = await categoryService.GetCategoryById(item.CategoryId);
            }
            if (!string.IsNullOrEmpty(CategoryId))
            {
                ShopItem = ShopItem.Where(s => s.CategoryId.ToString() == CategoryId).ToList();
            }
            if (!string.IsNullOrEmpty(SearchString))
            {
                ShopItem = ShopItem.Where(s => s.Name.Contains(SearchString) || s.Description.Contains(SearchString) || s.Category.Name.Contains(SearchString)).ToList();
            }
            LoggedUserId = new Guid(_userManager.GetUserId(User));
        }
    }
}
