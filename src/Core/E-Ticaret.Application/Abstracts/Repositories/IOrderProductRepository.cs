namespace E_Ticaret.Application.Abstracts.Repositories;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using E_Ticaret.Domain.Entities;

public interface IOrderProductRepository : IRepository<OrderProduct>
{
    Task<List<OrderProduct>> GetOrderProductsByOrderIdAsync(Guid orderId);
    Task<OrderProduct?> GetByOrderIdAndProductIdAsync(Guid orderId, Guid productId);
}
