using System.Collections.Generic;
using System.Linq;
using WatchesShop.Data;
using WatchesShop.Models;
using Microsoft.EntityFrameworkCore;

namespace WatchesShop.Services
{
    public class OrderService : IOrderService
    {
        private readonly WatchContext _context;

        public OrderService(WatchContext context)
        {
            _context = context;
        }

        public IEnumerable<Order> GetAllOrders()
        {
            return _context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Watch)
                .ToList();
        }
        public Order GetOrderById(int orderId)
        {
            return _context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Watch)
                .FirstOrDefault(o => o.OrderId == orderId);
        }

        public void UpdateOrder(Order order)
        {
            _context.Orders.Update(order);
            _context.SaveChanges();
        }
    }
}
