using E_Ticaret.Domain.Entities;

namespace E_Ticaret.Application.Abstracts.Repositories;

public interface ICategoryRepository : IRepository<Domain.Entities.Category>
{
    Task<List<Category>> GetByNameSearchAsync(string namePart);
}
