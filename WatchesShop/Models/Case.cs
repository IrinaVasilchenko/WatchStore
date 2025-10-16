using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WatchesShop.Models
{
    [Table("Cases")]
    public class Case
    {
        public int CaseId { get; set; }
        public decimal? Diameter { get; set; }
        public int MaterialId { get; set; }
        public Material Material { get; set; }
        public decimal? WaterResistance { get; set; }
        public decimal? LengthCase { get; set; }
        public decimal? Thickness { get; set; }
        public decimal? Weight { get; set; }
        public int ColorId { get; set; }
        public WatchColor Color { get; set; }
    }
}
