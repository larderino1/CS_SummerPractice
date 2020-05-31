using DbManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Frontend.Data.Models
{
    public class Product
    {
        public ShopItem ProductItem { get; set; }
        public int Quantity { get; set; }
    }
}
