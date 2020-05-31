
using Frontend.Services.CategoryService;
using Frontend.Services.ItemService;
using DbManager.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Web;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.ComponentModel.DataAnnotations;
using Frontend.Services;
using Frontend.Services.AzureStorageService;
using Microsoft.AspNetCore.Authorization;

namespace Frontend.Pages.ItemPages
{
    public class CreateModel : PageModel
    {
        private readonly IItemService itemService;

        private readonly UserManager<IdentityUser> _userManager;

        private readonly ICategoryService categoryService;

        private readonly IAzureBlobStorage azureService;

        [BindProperty]
        public BufferedSingleFile FileManager { get; set; }

        public CreateModel(IItemService itemService, IAzureBlobStorage azureService, UserManager<IdentityUser> user, ICategoryService categoryService)
        {
            this.itemService = itemService;
            this.azureService = azureService;
            _userManager = user;
            this.categoryService = categoryService;
        }

        public async Task<IActionResult> OnGet()
        {
            using (var client = new HttpClient())
            {
                var categories = await categoryService.GetAllCategories();
                ViewData["CategoryName"] = new SelectList(categories, "Id", "Name");
                return Page();
            }
        }

        [BindProperty]
        public ShopItem ShopItem { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            ShopItem.Image = await azureService.UploadFileToBlob(FileManager.FormFile);
            ShopItem.UserId = new Guid(_userManager.GetUserId(User));

            await itemService.CreateItem(ShopItem);

            return RedirectToPage("./Items");
        }


    }

    public class BufferedSingleFile
    {
        [Required]
        [Display(Name = "FileManager")]
        public IFormFile FormFile { get; set; }
    }
}
