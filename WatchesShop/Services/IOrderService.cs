using System.Collections.Generic;
using WatchesShop.Models;

namespace WatchesShop.Services
{
    public interface IOrderService
    {
        IEnumerable<Order> GetAllOrders();
        Order GetOrderById(int orderId);
        void UpdateOrder(Order order);
    }
}
