using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using WatchesShop.Data;
using WatchesShop.Models;
using WatchesShop.Extensions;
using WatchesShop.Models;
using WatchesShop.Services;


namespace WatchesShop.Controllers
{
    public class CartController : Controller
    {
        private readonly IWatchService _watchService;

        private readonly WatchContext _context;

        public CartController(IWatchService watchService, WatchContext context)
        {
            _watchService = watchService;
            _context = context;
        }

        public IActionResult Index()
        {
            var cart = GetCart();
            return View(cart);
        }

        public IActionResult AddToCart(int watchId)
        {
            var watch = _watchService.GetWatchById(watchId);
            if (watch != null)
            {
                var cart = GetCart();
                cart.AddItem(watch, 1);
                SaveCart(cart);
            }
            return RedirectToAction("Index");
        }

        public IActionResult RemoveFromCart(int watchId)
        {
            var cart = GetCart();
            cart.RemoveItem(watchId);
            SaveCart(cart);
            return RedirectToAction("Index");
        }

        private Cart GetCart()
        {
            var cart = HttpContext.Session.Get<Cart>("Cart") ?? new Cart();
            return cart;
        }

        private void SaveCart(Cart cart)
        {
            HttpContext.Session.Set("Cart", cart);
        }

        public IActionResult Checkout()
        {
            var cart = GetCart();
            if (!cart.Items.Any())
            {
                return RedirectToAction("Index");
            }
            return View(cart);
        }

        [HttpPost]
        public async Task<IActionResult> Checkout(Order order)
        {
            var cart = GetCart();
            if (!cart.Items.Any())
            {
                return RedirectToAction("Index");
            }

            // Сохраните заказ в базе данных
            order.OrderItems = cart.Items.Select(item => new OrderItem
            {
                WatchId = item.Watch.WatchId,
                Quantity = item.Quantity,
                Price = item.Watch.Price
            }).ToList();

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            // Подготовьте содержание письма для продавца
            var subject = "New Order Received";
            var body = $"<h2>New order received!</h2><p>Order details:</p><ul>";
            body += $"<li>Name: {order.Name}</li>";
            body += $"<li>Address: {order.Address}</li>";
            body += $"<li>Email: {order.Email}</li>";
            body += $"</ul><p>Items:</p><ul>";

            foreach (var item in cart.Items)
            {
                body += $"<li>{item.Watch.Model} - Quantity: {item.Quantity} - Price: {item.Watch.Price:C}</li>";
            }
            body += $"</ul><p>Total: {cart.ComputeTotalValue():C}</p>";


            cart.Clear();
            SaveCart(cart);
            return RedirectToAction("OrderComplete");
        }

        public IActionResult OrderComplete()
        {
            return View();
        }

    }
}
