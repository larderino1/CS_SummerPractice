using DbManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Frontend.Services.ItemService
{
    public interface IItemService
    {
        public Task<List<ShopItem>> GetAllItems();

        public Task<ShopItem> GetItemById(Guid? id);

        public Task UpdateItem(ShopItem item);

        public Task DeleteItemById(Guid? id);

        public Task CreateItem(ShopItem item);
    }
}
