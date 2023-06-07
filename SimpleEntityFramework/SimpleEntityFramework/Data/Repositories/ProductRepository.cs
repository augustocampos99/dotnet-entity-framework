using Microsoft.EntityFrameworkCore;
using SimpleEntityFramework.Data.Context;
using SimpleEntityFramework.Data.Repositories.Interfaces;
using SimpleEntityFramework.Entities;

namespace SimpleEntityFramework.Data.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly MySQLContext _context;

        public ProductRepository(MySQLContext context)
        {
            _context = context;
        }

        public async Task<List<Product>> GetAll()
        {
            return await _context.Products
                .Include(e => e.Category)
                .ToListAsync();
        }

        public async Task<Product> GetById(int id)
        {
            return await _context.Products
                .Include(e => e.Category)
                .Where(e => e.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<Product> Update(Product product, int id)
        {
            var productResult = _context.Products.Where(e => e.Id == id).FirstOrDefault();
            productResult.Name = product.Name;
            productResult.Price = product.Price;

            _context.Products.Update(productResult);

            await _context.SaveChangesAsync();

            return productResult;
        }

        public async Task<Product> Create(Product product)
        {
            var productResult = new Product()
            {
                Name = product.Name,
                Price = product.Price,
                CreatedAt = DateTime.Now
            };

            _context.Products.Add(productResult);
            await _context.SaveChangesAsync();

            return productResult;
        }

        public async Task<int> Delete(int id)
        {
            var productResult = _context.Products.Where(e => e.Id == id).FirstOrDefault();
            _context.Products.Remove(productResult);
            return await _context.SaveChangesAsync();
        }

    }
}
