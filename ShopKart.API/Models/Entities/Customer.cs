namespace ShopKart.API.Models.Entities
{
    public class Customer : BaseEntity
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;

        //Navigation key
        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
