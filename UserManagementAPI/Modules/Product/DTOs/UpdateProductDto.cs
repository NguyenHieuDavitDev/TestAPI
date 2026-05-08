using Microsoft.AspNetCore.Http;

namespace ProductAPI.Modules.Product.DTOs
{
    public class UpdateProductDto
    {
        public string ProductName { get; set; }
            = string.Empty;

        public decimal Price { get; set; }

        public IFormFile? Image { get; set; }
    }
}