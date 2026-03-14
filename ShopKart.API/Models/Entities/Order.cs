namespace ShopKart.API.Models.Entities
{
    public class Order : BaseEntity
    {
        public string OrderNumber { get; set; } = string.Empty;
        public DateTime OrderDate { get; set; } = DateTime.UtcNow;
        public decimal TotalAmount { get; set; }
        public string Status { get; set; } = "Pending"; // Pending, Confirmed, Shipped, Delivered

        //Foreign key
        public int CustomerId { get; set; }

        // Navigation property - Many Orders belong to One Customer
        public Customer Customer { get; set; } = null!;

        // Navigation property - One Order has many OrderItems
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    }
}
