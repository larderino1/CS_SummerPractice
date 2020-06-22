using DbManager.Models;
using Frontend.Services.ItemService;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice_Tests
{
    [TestClass]
    public class ItemApiTests
    {
        private readonly ItemService itemService = new ItemService();

        [TestMethod]
        public async Task GetAllItems()
        {
            var items = await itemService.GetAllItems();
            Assert.IsTrue(condition: items.Any());
        }

        [TestMethod]
        public async Task GetItemById()
        {
            var item = await itemService.GetItemById(new Guid("4e3db31c-3b94-4aa9-91f0-efabc2cf5adc"));
            Assert.IsNotNull(item);
        }

        [TestMethod]
        public async Task DeleteItem()
        {
            var item = new ShopItem()
            {
                Name = "testing item",
                Description = "item for unit testing",
                Price = 1,
                Image = "C:/Users/oksan/Pictures/2014-04/IMG_8082.JPG",
                CategoryId = new Guid("6dff5cc6-056f-46d0-a6b1-25471651cf2f"),
                SupplierId = "d802685e-4a5e-465b-8df6-886b266cfe18"
            };

            await itemService.CreateItem(item);

            var items = await itemService.GetAllItems();

            var itemFromList = items.FirstOrDefault(name => name.Name.Equals("testing item"));

            await itemService.DeleteItemById(itemFromList.Id);
            Assert.IsNull(await itemService.GetItemById(itemFromList.Id));
        }

        [TestMethod]
        public async Task CreateItem()
        {
            var item = new ShopItem()
            {
                Name = "testing item",
                Description = "item for unit testing",
                Price = 1,
                Image = "C:/Users/oksan/Pictures/2014-04/IMG_8082.JPG",
                CategoryId = new Guid("6dff5cc6-056f-46d0-a6b1-25471651cf2f"),
                SupplierId = "d802685e-4a5e-465b-8df6-886b266cfe18"
            };

            await itemService.CreateItem(item);

            var items = await itemService.GetAllItems();
            Assert.IsTrue(items.FirstOrDefault(name => name.Name.Equals("testing item")).Description.Equals(item.Description));
        }

        [TestMethod]
        public async Task UpdateItem()
        {
            var item = new ShopItem()
            {
                Name = "testing item",
                Description = "item for unit testing",
                Price = 1,
                Image = "C:/Users/oksan/Pictures/2014-04/IMG_8082.JPG",
                CategoryId = new Guid("6dff5cc6-056f-46d0-a6b1-25471651cf2f"),
                SupplierId = "d802685e-4a5e-465b-8df6-886b266cfe18"
            };

            await itemService.CreateItem(item);

            var items = await itemService.GetAllItems();

            var itemFromList = items.FirstOrDefault(name => name.Name.Equals("testing item"));

            itemFromList.Name = "Apple";

            await itemService.UpdateItem(itemFromList);

            var updatedItem = await itemService.GetItemById(itemFromList.Id);

            Assert.IsTrue(updatedItem.Name.Equals("Apple"));

            await itemService.DeleteItemById(itemFromList.Id);
        }
    }
}
