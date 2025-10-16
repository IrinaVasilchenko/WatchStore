using WatchesShop.Models;

namespace WatchesShop.Services
{
    public interface ICartService
    {
        void AddItem(Watch watch, int quantity);
        void RemoveItem(int watchId);
        void Clear();
        decimal ComputeTotalValue();
        IEnumerable<CartItem> GetItems();
    }
}
