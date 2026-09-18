using System.ComponentModel.DataAnnotations;

namespace WatchesShop.Models
{
    /// <summary>Тіло запиту для створення замовлення через API.</summary>
    public class CreateOrderRequest
    {
        [Required]
        public string Name { get; set; } = null!;

        [Required]
        public string Address { get; set; } = null!;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [Required]
        [MinLength(1, ErrorMessage = "Order must contain at least one item.")]
        public List<CreateOrderItemRequest> Items { get; set; } = new();
    }

    public class CreateOrderItemRequest
    {
        [Required]
        public int WatchId { get; set; }

        [Range(1, 100)]
        public int Quantity { get; set; } = 1;
    }

    /// <summary>Плоске DTO для відповіді — без циклічних посилань Order/OrderItem/Watch.</summary>
    public class OrderResponse
    {
        public int OrderId { get; set; }
        public string Name { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string Email { get; set; } = null!;
        public DateTime OrderDate { get; set; }
        public bool IsShipped { get; set; }
        public decimal TotalPrice { get; set; }
        public List<OrderItemResponse> Items { get; set; } = new();

        public static OrderResponse FromEntity(Order order) => new()
        {
            OrderId = order.OrderId,
            Name = order.Name,
            Address = order.Address,
            Email = order.Email,
            OrderDate = order.OrderDate,
            IsShipped = order.IsShipped,
            TotalPrice = order.OrderItems?.Sum(i => i.Price * i.Quantity) ?? 0,
            Items = order.OrderItems?.Select(i => new OrderItemResponse
            {
                WatchId = i.WatchId,
                Model = i.Watch?.Model,
                Quantity = i.Quantity,
                Price = i.Price
            }).ToList() ?? new List<OrderItemResponse>()
        };
    }

    public class OrderItemResponse
    {
        public int WatchId { get; set; }
        public string? Model { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
