using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WatchesShop.Models
{
    public class Mechanism
    {
        [Key]
        public int MechanismTypeId { get; set; }

        public string Name { get; set; }
    }
}
