namespace RetailStore.Data
{
    public class Customer : BaseClass
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public required string Email { get; set; }
    }
}
