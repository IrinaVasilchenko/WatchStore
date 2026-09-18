using WatchesShop.Models;

namespace WatchesShop.Services
{
    /// <summary>
    /// Відповідає за сповіщення зовнішніх систем (напр. CRM) про події із замовленнями.
    /// Приклад реалізації методу обміну даними через API (вихідний webhook).
    /// </summary>
    public interface IOrderNotificationService
    {
        Task NotifyOrderCreatedAsync(Order order);
    }
}
