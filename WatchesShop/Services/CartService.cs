using System.Collections.Generic;
using WatchesShop.Models;

namespace WatchesShop.Services
{
    public class CartService : ICartService
    {
        private readonly Cart _cart;

        public CartService(Cart cart)
        {
            _cart = cart;
        }

        public void AddItem(Watch watch, int quantity)
        {
            _cart.AddItem(watch, quantity);
        }

        public void RemoveItem(int watchId)
        {
            _cart.RemoveItem(watchId);
        }

        public void Clear()
        {
            _cart.Clear();
        }

        public decimal ComputeTotalValue()
        {
            return _cart.ComputeTotalValue();
        }

        public IEnumerable<CartItem> GetItems()
        {
            return _cart.Items;
        }
    }
}
