using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WatchesShop.Models
{
    public class Material
    {
        [Key]
        public int MaterialId { get; set; }

        public string Name { get; set; }
    }
}
