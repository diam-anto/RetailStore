namespace RetailStore.Data
{
    public class Purchase : BaseClass
    {
        //public required List<OrderProduct> OrderProducts { get; set; } = new();
        public required int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public decimal TotalCost { get; set; }
    }
}
