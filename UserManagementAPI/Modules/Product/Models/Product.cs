using System.ComponentModel.DataAnnotations;

namespace ProductAPI.Modules.Product.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        public string ProductName { get; set; } = string.Empty;

        public decimal Price { get; set; }

        public string? ImageUrl { get; set; }

        public bool IsDeleted { get; set; } = false;

        public DateTime CreatedAt { get; set; }
            = DateTime.Now;
    }
}