namespace WatchesShop.Models
{
    public class CartItem
    {
        public int WatchId { get; set; }
        public Watch Watch { get; set; }
        public int Quantity { get; set; }
    }
}
