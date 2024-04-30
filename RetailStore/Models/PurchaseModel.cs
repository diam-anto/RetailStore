using RetailStore.Data;

namespace RetailStore.Models
{
    public class PurchaseModel
    {
        public required List<OrderProductModel> OrderProducts { get; set; } = new();
        public required int CustomerId { get; set; }
        public Customer Customer { get; set; }
    }
}
