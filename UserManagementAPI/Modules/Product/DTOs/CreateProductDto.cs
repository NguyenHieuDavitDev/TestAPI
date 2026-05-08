using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace ProductAPI.Modules.Product.DTOs
{
    public class CreateProductDto
    {
        [Required]
        public string ProductName { get; set; }
            = string.Empty;

        public decimal Price { get; set; }

        public IFormFile? Image { get; set; }
    }
}