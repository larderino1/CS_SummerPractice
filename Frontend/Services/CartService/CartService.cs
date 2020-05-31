using Frontend.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Frontend.Services.CartService
{
    public class CartService : ICartService
    {
        public int Exists(List<Product> cart, Guid? id)
        {
            for (var i = 0; i < cart.Count; i++)
            {
                if (cart[i].ProductItem.Id == id)
                {
                    return i;
                }
            }
            return -1;
        }
    }
}
