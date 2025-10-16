using System.ComponentModel.DataAnnotations;

namespace WatchesShop.Models
{
    public class Watch
    {
        public int WatchId { get; set; }
        public string Model { get; set; }
        public int GenderId { get; set; }
        public Gender Gender { get; set; }
        public int BrandId { get; set; }
        public Brand Brand { get; set; }
        public int StyleId { get; set; }
        public Style Style { get; set; }
        public int MechanismTypeId { get; set; }
        public Mechanism MechanismType { get; set; }
        public string AssemblyFactory { get; set; }
        public int CaseId { get; set; }
        public Case Case { get; set; }
        public decimal Price { get; set; }

        public string ImagePath { get; set; } = null!;

    }
}
