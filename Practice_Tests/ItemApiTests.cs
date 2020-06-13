using Frontend.Services.ItemService;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
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
    }
}
