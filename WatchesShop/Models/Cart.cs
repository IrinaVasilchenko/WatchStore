using System.Collections.Generic;
using System.Linq;

namespace WatchesShop.Models
{
    public class Cart
    {
        private List<CartItem> itemCollection = new List<CartItem>();

        public virtual void AddItem(Watch watch, int quantity)
        {
            var item = itemCollection
                .FirstOrDefault(w => w.Watch.WatchId == watch.WatchId);

            if (item == null)
            {
                itemCollection.Add(new CartItem
                {
                    Watch = watch,
                    Quantity = quantity
                });
            }
            else
            {
                item.Quantity += quantity;
            }
        }

        public virtual void RemoveItem(int watchId)
        {
            itemCollection.RemoveAll(w => w.Watch.WatchId == watchId);
        }

        public virtual decimal ComputeTotalValue()
        {
            return itemCollection.Sum(e => e.Watch.Price * e.Quantity);
        }

        public virtual void Clear()
        {
            itemCollection.Clear();
        }

        public virtual IEnumerable<CartItem> Items => itemCollection;
    }
}
