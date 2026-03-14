using System.ComponentModel.DataAnnotations;

namespace ShopKart.API.DTOs
{
    // client input CREAT
    public class ProductCreateDTO
    {
        [Required]
        [StringLength(200,MinimumLength = 3)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(1000)]
        public string Description { get; set; } = string.Empty;

        [Range(1,999999.99)]
        public decimal Price { get; set; }

        [Range(0,int.MaxValue)]
        public int Stock { get; set; }

        [MaxLength(500)]
        public string ImageUrl { get; set; } = string.Empty;

        [Required]
        [Range(1, 100000)]
        public int CategoryId { get; set; }
    }

    // client input UPDATE
    public class ProductUpdateDTO
    {
        [Required]
        [StringLength(200,MinimumLength =3)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(1000)]
        public string Description { get; set; } = string.Empty;

        [Range(1,999999.99)]
        public decimal Price { get; set; }

        [Range(0,int.MaxValue)]
        public int Stock { get; set; }

        [MaxLength(500)]
        public string ImageUrl { get; set; } = string.Empty;

        [Required]
        [Range(1,100000)]
        public int CategoryId { get; set; }
    }

    // client output RESPONSE
    public class    ProductResponseDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = string.Empty;
    }
}
