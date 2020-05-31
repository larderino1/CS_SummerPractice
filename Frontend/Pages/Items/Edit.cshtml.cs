using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;
using DbManager.Models;
using Frontend.Services.ItemService;
using Frontend.Services.CategoryService;
using Frontend.Services.AzureStorageService;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;

namespace Frontend.Pages.ItemPages
{
    public class EditModel : PageModel
    {
        private readonly IItemService itemService;

        private readonly ICategoryService categoryService;

        private readonly IAzureBlobStorage azureService;

        [BindProperty]
        public BufferedSingleFile FileManager { get; set; }

        public EditModel(IItemService itemService, ICategoryService categoryService, IAzureBlobStorage azureService)
        {
            this.itemService = itemService;
            this.categoryService = categoryService;
            this.azureService = azureService;
        }

        [BindProperty]
        public ShopItem ShopItem { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ShopItem = await itemService.GetItemById(id);

            if (ShopItem == null)
            {
                return NotFound();
            }
            using(var client = new HttpClient())
            {
                var categories = await categoryService.GetAllCategories();
                ViewData["CategoryName"] = new SelectList(categories, "Id", "Name");
            }
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                ShopItem.Image = await azureService.UploadFileToBlob(FileManager.FormFile);
                await itemService.UpdateItem(ShopItem);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return RedirectToPage("./Items");
        }
    }

}
