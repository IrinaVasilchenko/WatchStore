using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WatchesShop.Models
{
    public class Style
    {
        [Key]
        public int StyleId { get; set; }

        public string Name { get; set; }
    }
}
