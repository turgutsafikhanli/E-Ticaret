using E_Ticaret.Domain.Entities;

namespace E_Ticaret.Application.Abstracts.Repositories;

public interface IOrderRepository : IRepository<Order>
{
    Task<List<Order>> GetOrdersByUserIdAsync(string userId);
}
