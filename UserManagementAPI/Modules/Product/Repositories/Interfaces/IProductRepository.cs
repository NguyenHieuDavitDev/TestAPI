using ProductAPI.Modules.Product.Models;
using ProductEntity = ProductAPI.Modules.Product.Models.Product;

namespace ProductAPI.Modules.Product.Repositories.Interfaces
{
    public interface IProductRepository
    {
        Task<List<ProductEntity>> GetAllAsync();

        Task<ProductEntity?> GetByIdAsync(int id);

        Task AddAsync(ProductEntity product);

        Task UpdateAsync(ProductEntity product);

        Task SaveChangesAsync();
    }
}