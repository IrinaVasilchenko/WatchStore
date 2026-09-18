using Microsoft.AspNetCore.Mvc;
using WatchesShop.Data;
using WatchesShop.Models;
using WatchesShop.Services;

namespace WatchesShop.Controllers.Api
{
    /// <summary>
    /// REST API для замовлень. При створенні замовлення надсилає подію "order.created"
    /// на зовнішній webhook (IOrderNotificationService) — приклад інтеграції з іншою системою (напр. CRM).
    /// </summary>
    [ApiController]
    [Route("api/orders")]
    [Produces("application/json")]
    public class OrdersApiController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IWatchService _watchService;
        private readonly WatchContext _context;
        private readonly IOrderNotificationService _notificationService;

        public OrdersApiController(
            IOrderService orderService,
            IWatchService watchService,
            WatchContext context,
            IOrderNotificationService notificationService)
        {
            _orderService = orderService;
            _watchService = watchService;
            _context = context;
            _notificationService = notificationService;
        }

        /// <summary>Отримати всі замовлення.</summary>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<OrderResponse>), StatusCodes.Status200OK)]
        public IActionResult GetAll()
        {
            var orders = _orderService.GetAllOrders().Select(OrderResponse.FromEntity);
            return Ok(orders);
        }

        /// <summary>Отримати замовлення за ідентифікатором.</summary>
        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(OrderResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetById(int id)
        {
            var order = _orderService.GetOrderById(id);
            if (order == null) return NotFound(new { message = $"Order with id {id} not found." });
            return Ok(OrderResponse.FromEntity(order));
        }

        /// <summary>
        /// Створити нове замовлення. Після успішного збереження в БД надсилає webhook-подію
        /// "order.created" на URL, вказаний у конфігурації Webhook:OrderCreatedUrl.
        /// Якщо зовнішній сервіс недоступний — замовлення все одно створюється,
        /// помилка інтеграції лише логується.
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(OrderResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] CreateOrderRequest request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var order = new Order
            {
                Name = request.Name,
                Address = request.Address,
                Email = request.Email,
                OrderDate = DateTime.UtcNow,
                IsShipped = false,
                OrderItems = new List<OrderItem>()
            };

            foreach (var item in request.Items)
            {
                var watch = _watchService.GetWatchById(item.WatchId);
                if (watch == null)
                    return BadRequest(new { message = $"Watch with id {item.WatchId} not found." });

                order.OrderItems.Add(new OrderItem
                {
                    WatchId = watch.WatchId,
                    Quantity = item.Quantity,
                    Price = watch.Price
                });
            }

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            // --- Зовнішня інтеграція: сповіщаємо іншу систему про нове замовлення ---
            await _notificationService.NotifyOrderCreatedAsync(order);

            var response = OrderResponse.FromEntity(order);
            return CreatedAtAction(nameof(GetById), new { id = order.OrderId }, response);
        }

        /// <summary>Позначити замовлення як відправлене.</summary>
        [HttpPut("{id:int}/ship")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult MarkAsShipped(int id)
        {
            var order = _orderService.GetOrderById(id);
            if (order == null) return NotFound(new { message = $"Order with id {id} not found." });

            order.IsShipped = true;
            _orderService.UpdateOrder(order);
            return NoContent();
        }
    }
}
