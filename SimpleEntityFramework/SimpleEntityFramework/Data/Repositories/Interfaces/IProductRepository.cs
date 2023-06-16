using SimpleEntityFramework.Entities;

namespace SimpleEntityFramework.Data.Repositories.Interfaces
{
    public interface IProductRepository
    {
        Task<List<Product>> GetAll();
        Task<Product> GetById(int id);
        Task<Product> Create(Product product);
        Task<Product> Update(Product product, int id);
        Task<int> Delete(int id);
        Task<List<Product>> GetAllRawQuery();
        Task<Product> GetByIdRawQuery(int id);
        Task<List<Product>> GetAllLinqQuery();
        Task<Product> GetByIdLinqQuery(int id);
    }
}
