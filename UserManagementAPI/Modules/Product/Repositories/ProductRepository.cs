using Microsoft.EntityFrameworkCore;
using ProductAPI.Data;
using ProductAPI.Modules.Product.Models;
using ProductAPI.Modules.Product.Repositories.Interfaces;
using ProductEntity = ProductAPI.Modules.Product.Models.Product;

namespace ProductAPI.Modules.Product.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;

        public ProductRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<ProductEntity>> GetAllAsync()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<ProductEntity?> GetByIdAsync(int id)
        {
            return await _context.Products
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task AddAsync(ProductEntity product)
        {
            await _context.Products.AddAsync(product);
        }

        public async Task UpdateAsync(ProductEntity product)
        {
            _context.Products.Update(product);

            await Task.CompletedTask;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}