using E_Ticaret.Domain.Entities;

namespace E_Ticaret.Application.Abstracts.Repositories;

public interface IProductRepository : IRepository<Product>
{
    Task<List<Product>> GetByUserIdAsync(string userId);
    Task<List<Product>> GetByCategoryIdAsync(Guid categoryId);
    Task<List<Product>> GetByNameAsync(string name);
    Task<List<Product>> SearchAsync(string keyword);
}
