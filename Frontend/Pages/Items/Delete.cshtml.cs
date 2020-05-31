
using Azure.Storage.Blobs;
using Frontend.Services.ItemService;
using DbManager.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Frontend.Pages.ItemPages
{
    public class DeleteModel : PageModel
    {
        private readonly IItemService service;
        public DeleteModel(IItemService service)
        {
            this.service = service;
        }

        [BindProperty]
        public ShopItem ShopItem { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ShopItem = await service.GetItemById(id);


            if (ShopItem == null)
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

            ShopItem = await service.GetItemById(id);

            var client = new BlobClient(new Uri(ShopItem.Image));
            await client.DeleteIfExistsAsync();

            if (ShopItem != null)
            {
                await service.DeleteItemById(id);
            }

            return RedirectToPage("./Items");
        }
    }


}
