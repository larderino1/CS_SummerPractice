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
            var itemId = new Guid("7f85aae0-a3ff-45b9-b8f9-6afac64ecd5a");
            var item = await itemService.GetItemById(itemId);
            Assert.IsTrue(item.Name.Equals("test item"));
        }

        [TestMethod]
        public async Task DeleteItem()
        {
            var itemId = new Guid("12bf8e8a-685a-4641-b22b-21e79a9e4270");
            var item = await itemService.GetItemById(itemId);

            await itemService.DeleteItemById(itemId);
            Assert.IsNull(await itemService.GetItemById(itemId));

            await itemService.CreateItem(item);
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
                UserId = new Guid("d802685e-4a5e-465b-8df6-886b266cfe18")
            };

            await itemService.CreateItem(item);

            var items = await itemService.GetAllItems();
            Assert.IsTrue(items.FirstOrDefault(name => name.Name.Equals("test item")).Description.Equals(item.Description));
        }

        [TestMethod]
        public async Task UpdateItem()
        {
            var itemId = new Guid("7f85aae0-a3ff-45b9-b8f9-6afac64ecd5a");
            var item = await itemService.GetItemById(itemId);

            item.Name = "Apple";
            await itemService.UpdateItem(item);
            var updatedItem = await itemService.GetItemById(itemId);
            Assert.IsTrue(updatedItem.Name.Equals("Apple"));
            updatedItem.Name = "test item";
            await itemService.UpdateItem(updatedItem);
        }
    }
}
