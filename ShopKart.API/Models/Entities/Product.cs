namespace ShopKart.API.Models.Entities
{
    public class Product:BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public string ImageUrl { get; set; } = string.Empty;

        //Foreign key
        public int CategoryId { get; set; }

        //Navigation property
        public Category Category { get; set; } = null!;

        //Navigation property
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}
