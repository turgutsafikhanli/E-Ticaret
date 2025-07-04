using E_Ticaret.Application.Abstracts.Repositories;
using E_Ticaret.Domain.Entities;
using E_Ticaret.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace E_Ticaret.Persistence.Repositories;

public class CategoryRepository : Repository<Domain.Entities.Category>, ICategoryRepository
{
    private readonly E_TicaretDbContext _context;
    public CategoryRepository(E_TicaretDbContext context) : base(context)
    {
    }
    public async Task<List<Category>> GetByNameSearchAsync(string namePart)
    {
        return await _context.Categories
            .Where(c => c.Name.Contains(namePart))
            .ToListAsync();
    }
}
