using Frontend.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Frontend.Services.CartService
{
    public interface ICartService
    {
        public int Exists(List<Product> cart, Guid? id);
    }
}
