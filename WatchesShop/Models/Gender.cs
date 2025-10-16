using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WatchesShop.Models
{
    public class Gender
    {
        [Key]
        public int GenderId { get; set; }

        public string Name { get; set; }
    }
}