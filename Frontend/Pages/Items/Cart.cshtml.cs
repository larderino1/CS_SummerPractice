using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DbManager.Models;
using Frontend.Data.Models;
using Frontend.Services.CartService;
using Frontend.Services.ItemService;
using Frontend.Services.OrderService;
using Frontend.Services.SessionService;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
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

        private readonly IEmailSender emailSender;

        public CartModel(IItemService service, ICartService cartService, IOrderService orderService, UserManager<IdentityUser> manager, IEmailSender emailSender)
        {
            this.service = service;
            this.cartService = cartService;
            this.orderService = orderService;
            this.manager = manager;
            this.emailSender = emailSender;
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
            if (Cart == null)
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
                if (index == -1)
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
            for (var i = 0; i < Cart.Count; i++)
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

            foreach (var cart in Cart)
            {
                var order = new Order
                {
                    ItemName = cart.ProductItem.Name,
                    Quantity = cart.Quantity,
                    Price = cart.ProductItem.Price * cart.Quantity,
                    UserId = user.Id,
                    SupplierId = cart.ProductItem.SupplierId
                };

                var sb = new StringBuilder();
                await orderService.CreateOrder(order);
                sb.AppendLine("<p>Hello!<br>");
                sb.AppendLine("Thank you for choosing us.</p>");
                sb.AppendLine("<p>Your order:</p>");
                sb.AppendLine($"<p>{order.ItemName} - {cart.ProductItem.Price}₴<br>");
                sb.AppendLine($"QTY: {order.Quantity}</p>");
                sb.AppendLine($"<p>Total: {order.Price}</p>");
                sb.AppendLine("<p>Best regards, your HeyTech team!</p>");
                var body = sb.ToString();
                const string subject = "Thank You For Your Purchase";
                await emailSender.SendEmailAsync(user.Email, subject, body);

            }
            Cart.Clear();
            SessionHelper.SetObjectAsJson(HttpContext.Session,"Cart", Cart);
            return RedirectToPage("./Items");
        }
    }
}