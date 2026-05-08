using ProductAPI.Modules.Product.DTOs;
using ProductAPI.Modules.Product.Models;
using ProductAPI.Modules.Product.Repositories.Interfaces;
using ProductAPI.Modules.Product.Services.Interfaces;
using ProductEntity = ProductAPI.Modules.Product.Models.Product;

namespace ProductAPI.Modules.Product.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;
        private readonly IWebHostEnvironment _env;

        public ProductService(
            IProductRepository repository,
            IWebHostEnvironment env)
        {
            _repository = repository;
            _env = env;
        }

        public async Task<List<ProductResponseDto>> GetAllAsync()
        {
            var products = await _repository.GetAllAsync();

            return products.Select(x => new ProductResponseDto
            {
                Id = x.Id,
                ProductName = x.ProductName,
                Price = x.Price,
                ImageUrl = x.ImageUrl
            }).ToList();
        }

        public async Task<ProductResponseDto?> GetByIdAsync(
            int id)
        {
            var product = await _repository.GetByIdAsync(id);

            if (product == null)
                return null;

            return new ProductResponseDto
            {
                Id = product.Id,
                ProductName = product.ProductName,
                Price = product.Price,
                ImageUrl = product.ImageUrl
            };
        }

        public async Task<ProductResponseDto> CreateAsync(
            CreateProductDto dto)
        {
            string? imagePath = null;

            if (dto.Image != null)
            {
                string uploadFolder = Path.Combine(
                    Directory.GetCurrentDirectory(),
                    "wwwroot/uploads");

                if (!Directory.Exists(uploadFolder))
                {
                    Directory.CreateDirectory(uploadFolder);
                }

                string fileName =
                    Guid.NewGuid().ToString()
                    + Path.GetExtension(dto.Image.FileName);

                string filePath =
                    Path.Combine(uploadFolder, fileName);

                using (var stream = new FileStream(
                    filePath,
                    FileMode.Create))
                {
                    await dto.Image.CopyToAsync(stream);
                }

                imagePath = "/uploads/" + fileName;
            }

            ProductEntity product = new ProductEntity
            {
                ProductName = dto.ProductName,
                Price = dto.Price,
                ImageUrl = imagePath
            };

            await _repository.AddAsync(product);

            await _repository.SaveChangesAsync();

            return new ProductResponseDto
            {
                Id = product.Id,
                ProductName = product.ProductName,
                Price = product.Price,
                ImageUrl = product.ImageUrl
            };
        }

        public async Task<ProductResponseDto?> UpdateAsync(
            int id,
            UpdateProductDto dto)
        {
            var product = await _repository.GetByIdAsync(id);

            if (product == null)
                return null;

            product.ProductName = dto.ProductName;
            product.Price = dto.Price;

            if (dto.Image != null)
            {
                string uploadFolder = Path.Combine(
                    Directory.GetCurrentDirectory(),
                    "wwwroot/uploads");

                if (!Directory.Exists(uploadFolder))
                {
                    Directory.CreateDirectory(uploadFolder);
                }

                string fileName =
                    Guid.NewGuid().ToString()
                    + Path.GetExtension(dto.Image.FileName);

                string filePath =
                    Path.Combine(uploadFolder, fileName);

                using (var stream = new FileStream(
                    filePath,
                    FileMode.Create))
                {
                    await dto.Image.CopyToAsync(stream);
                }

                product.ImageUrl =
                    "/uploads/" + fileName;
            }

            await _repository.UpdateAsync(product);

            await _repository.SaveChangesAsync();

            return new ProductResponseDto
            {
                Id = product.Id,
                ProductName = product.ProductName,
                Price = product.Price,
                ImageUrl = product.ImageUrl
            };
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var product = await _repository.GetByIdAsync(id);

            if (product == null)
                return false;

            product.IsDeleted = true;

            await _repository.SaveChangesAsync();

            return true;
        }
    }
}