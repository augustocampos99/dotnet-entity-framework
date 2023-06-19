using Microsoft.EntityFrameworkCore;
using MySqlConnector;
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

            if (productResult == null)
                return null;

            productResult.Name = product.Name;
            productResult.Price = product.Price;
            productResult.Quantity = product.Quantity;
            productResult.CategoryId = product.CategoryId;

            _context.Products.Update(productResult);

            await _context.SaveChangesAsync();

            return productResult;
        }

        public async Task<Product> Create(Product product)
        {
            product.CreatedAt = DateTime.Now;

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return product;
        }

        public async Task<int> Delete(int id)
        {
            var productResult = _context.Products.Where(e => e.Id == id).FirstOrDefault();

            if (productResult == null)
                return 0;

            _context.Products.Remove(productResult);
            return await _context.SaveChangesAsync();
        }

        public async Task<List<Product>> GetAllRawQuery()
        {
            return await _context.Products.FromSqlRaw("SELECT * FROM products ORDER BY id DESC").ToListAsync<Product>();
        }

        public async Task<Product> GetByIdRawQuery(int id)
        {
            return await _context.Products.FromSqlRaw("SELECT * FROM products WHERE id = @Id", new MySqlParameter("Id", id)).FirstOrDefaultAsync<Product>();
        }

        public async Task<List<Product>> GetAllLinqQuery()
        {
            return await (from p in _context.Products 
                          join c in _context.Categories
                          on p.CategoryId equals c.Id
                          select new Product()
                          {
                              Id = p.Id,
                              Name = p.Name,
                              Price = p.Price,
                              Quantity = p.Quantity,
                              CreatedAt = p.CreatedAt,
                              CategoryId = p.CategoryId,
                              Category = c
                          }).ToListAsync<Product>();
        }

        public async Task<Product> GetByIdLinqQuery(int id)
        {
            return await (from p in _context.Products
                          join c in _context.Categories
                          on p.CategoryId equals c.Id
                          where p.Id == id
                          select new Product()
                          {
                              Id = p.Id,
                              Name = p.Name,
                              Price = p.Price,
                              Quantity = p.Quantity,
                              CreatedAt = p.CreatedAt,
                              CategoryId = p.CategoryId,
                              Category = c
                          }).FirstOrDefaultAsync<Product>();
        }

    }
}
