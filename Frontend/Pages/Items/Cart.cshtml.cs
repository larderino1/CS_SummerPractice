using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DbManager.Models;
using Frontend.Data.Models;
using Frontend.Services.CartService;
using Frontend.Services.ItemService;
using Frontend.Services.OrderService;
using Frontend.Services.SessionService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Frontend.Pages.Items
{
    public class CartModel : PageModel
    {
        public List<Product> Cart { get; set; }

        public double Total { get; set; }

        private readonly IItemService service;

        private readonly ICartService cartService;

        private readonly IOrderService orderService;

        private readonly UserManager<IdentityUser> manager;

        public CartModel(IItemService service, ICartService cartService, IOrderService orderService, UserManager<IdentityUser> manager)
        {
            this.service = service;
            this.cartService = cartService;
            this.orderService = orderService;
            this.manager = manager;
        }

        public void OnGet()
        {
            Cart = SessionHelper.GetObjectFromJson<List<Product>>(HttpContext.Session, "Cart");
            Total = Cart.Sum(sum => sum.ProductItem.Price * sum.Quantity);
        }

        //add action with name Buy
        public async Task<IActionResult> OnGetBuy(Guid? id)
        {
            Cart = SessionHelper.GetObjectFromJson<List<Product>>(HttpContext.Session, "Cart");
            if(Cart == null)
            {
                Cart = new List<Product>();
                Cart.Add(new Product
                {
                    ProductItem = await service.GetItemById(id),
                    Quantity = 1
                });
                SessionHelper.SetObjectAsJson(HttpContext.Session, "Cart", Cart); 
            }
            else
            {
                int index = cartService.Exists(Cart, id);
                if(index == -1)
                {
                    Cart.Add(new Product
                    {
                        ProductItem = await service.GetItemById(id),
                        Quantity = 1
                    });
                }
                else
                {
                    Cart[index].Quantity++;
                }
                SessionHelper.SetObjectAsJson(HttpContext.Session, "Cart", Cart);
            }
            return RedirectToPage("./Cart");
        }
        //add action with name Delete
        public IActionResult OnGetDelete(Guid id)
        {
            Cart = SessionHelper.GetObjectFromJson<List<Product>>(HttpContext.Session, "Cart");
            int index = cartService.Exists(Cart, id);
            Cart.RemoveAt(index);
            SessionHelper.SetObjectAsJson(HttpContext.Session, "Cart", Cart);
            return RedirectToPage("./Cart");
        }

        public IActionResult OnPostUpdate(int[] quantity)
        {
            Cart = SessionHelper.GetObjectFromJson<List<Product>>(HttpContext.Session, "Cart");
            for(var i = 0; i < Cart.Count; i++)
            {
                Cart[i].Quantity = quantity[i];
            }
            SessionHelper.SetObjectAsJson(HttpContext.Session, "Cart", Cart);
            return RedirectToPage("./Cart");
        }

        public async Task<IActionResult> OnPostBuy()
        {
            Cart = SessionHelper.GetObjectFromJson<List<Product>>(HttpContext.Session, "Cart");

            var user = await manager.FindByNameAsync(User.Identity.Name);

            foreach(var cart in Cart)
            {
                var order = new Order
                {
                    ItemName = cart.ProductItem.Name,
                    Quantity = cart.Quantity,
                    Price = cart.ProductItem.Price * cart.Quantity,
                    UserId = user.Id,
                    SupplierId = cart.ProductItem.SupplierId
                };

                await orderService.CreateOrder(order);
            }

            Cart.Clear();
            return RedirectToPage("./Index");
        }
    }
}