namespace ProductAPI.Modules.Product.DTOs
{
    public class ProductResponseDto
    {
        public int Id { get; set; }

        public string ProductName { get; set; }
            = string.Empty;

        public decimal Price { get; set; }

        public string? ImageUrl { get; set; }
    }
}