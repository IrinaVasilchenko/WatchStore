using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WatchesShop.Models
{
    public class WatchColor
    {
        [Key]
        public int ColorId { get; set; }

        public string Name { get; set; }
    }
}
