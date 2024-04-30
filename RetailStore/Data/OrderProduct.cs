namespace RetailStore.Data
{
    public class OrderProduct : BaseClass
    {
        public required int PurchaseId { get; set; }
        public Purchase Purchase { get; set; }
        public required int ProductId { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; } = 1;
    }
}