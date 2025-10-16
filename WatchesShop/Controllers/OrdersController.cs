using Microsoft.AspNetCore.Mvc;
using WatchesShop.Models;
using WatchesShop.Services;

namespace WatchesShop.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public IActionResult Index()
        {
            var orders = _orderService.GetAllOrders();
            return View(orders);
        }
        public IActionResult UpdateOrders(List<Order> orders)
        {
            foreach (var updatedOrder in orders)
            {
                var order = _orderService.GetOrderById(updatedOrder.OrderId);
                if (order != null)
                {
                    order.IsShipped = updatedOrder.IsShipped;
                    _orderService.UpdateOrder(order);
                }
            }
            return RedirectToAction("Index");
        }
    }
}
