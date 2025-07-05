using E_Ticaret.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace E_Ticaret.Persistence.Repositories;

public class FavouriteRepository : Repository<Domain.Entities.Favourite>, Application.Abstracts.Repositories.IFavouriteRepository
{
    private readonly E_TicaretDbContext _context;
    public FavouriteRepository(E_TicaretDbContext context) : base(context)
    {
    }
    public async Task<List<Domain.Entities.Favourite>> GetByAdIdAsync(Guid productId)
    {
        return await _context.Favourites
            .Where(f => f.ProductId == productId)
            .ToListAsync();
    }
    public async Task<bool> IsExistAsync(Guid productId)
    {
        return await _context.Favourites
            .AnyAsync(f => f.ProductId == productId);
    }
    public async Task RemoveByAdIdAsync(Guid productId)
    {
        var favorite = await _context.Favourites
            .FirstOrDefaultAsync(f => f.ProductId == productId);

        if (favorite != null)
        {
            _context.Favourites.Remove(favorite);
            await _context.SaveChangesAsync();
        }
    }
}