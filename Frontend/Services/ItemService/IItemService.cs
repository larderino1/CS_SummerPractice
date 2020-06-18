using DbManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Frontend.Services.ItemService
{
    public interface IItemService
    {
        Task<List<ShopItem>> GetAllItems();

        Task<ShopItem> GetItemById(Guid? id);

        Task UpdateItem(ShopItem item);

        Task DeleteItemById(Guid? id);

        Task CreateItem(ShopItem item);
    }
}
