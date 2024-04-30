using System.ComponentModel.DataAnnotations;

namespace RetailStore.Data
{
    public class Product : BaseClass
    {
        [MaxLength(50)]
        public required string Sku { get; set; }

        [MaxLength(200)]
        public required string Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }

    }
}
