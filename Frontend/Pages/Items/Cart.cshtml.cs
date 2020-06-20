using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Frontend.Data.Models;
using Frontend.Services.CartService;
using Frontend.Services.ItemService;
using Frontend.Services.SessionService;
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
        private readonly IEmailSender emailSender;

        public CartModel(IItemService service, ICartService cartService, IEmailSender emailSender)
        {
            this.service = service;
            this.cartService = cartService;
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

        public IActionResult OnPostCheckout(int[] quantity)
        {
            Cart = SessionHelper.GetObjectFromJson<List<Product>>(HttpContext.Session, "Cart");
            for (var i = 0; i < Cart.Count; i++)
            {
                Cart[i].Quantity = quantity[i];
            }
            SessionHelper.SetObjectAsJson(HttpContext.Session, "Cart", Cart);
            var subject = "Thank You For Your Purchase";
            var email = User.Identity.Name;
            var sb = new StringBuilder();
            double total = 0;
            sb.AppendLine("<p>Hello!</p>");
            sb.AppendLine("<p>Thank you for choosing us.</p>");
            sb.AppendLine("<p>Your order:</p>");
            sb.AppendLine("<ul>");
            foreach (var item in Cart)
            {
                sb.AppendLine($"<li>{item.ProductItem.Name},{item.ProductItem.Price}₴, {item.Quantity} item(s)</li>");
                total += item.ProductItem.Price * item.Quantity;
            }
            sb.AppendLine("</ul>");
            sb.AppendLine($"<p>Total: {total}</p>");
            sb.AppendLine("<p>Best Regards, your HeyTech team</p>");
            var body = sb.ToString();
            emailSender.SendEmailAsync(email, subject, body);
            return RedirectToPage("./Cart");
        }
    }
}