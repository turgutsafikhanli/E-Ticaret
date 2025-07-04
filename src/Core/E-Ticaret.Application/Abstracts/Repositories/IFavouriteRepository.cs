using E_Ticaret.Domain.Entities;

namespace E_Ticaret.Application.Abstracts.Repositories;

public interface IFavouriteRepository : IRepository<Favourite>
{
    Task<List<Favourite>> GetByAdIdAsync(Guid adId);
    Task<bool> IsExistAsync(Guid adId);
    Task RemoveByAdIdAsync(Guid adId);
}
