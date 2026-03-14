using System.ComponentModel.DataAnnotations;

namespace ShopKart.API.DTOs
{
    public class PaymentDTO
    {
        [Required]
        public string PaymentMethod { get; set; } = string.Empty;

        [Required]
        [Range(1, maximum: double.MaxValue)]
        public decimal Amount { get; set; }

    }
}
