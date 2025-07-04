using E_Ticaret.Domain.Entities;

namespace E_Ticaret.Application.Abstracts.Repositories;

public interface IImageRepository : IRepository<Image>
{
    Task<List<Image>> GetImagesByProductIdAsync(Guid productId);
}
