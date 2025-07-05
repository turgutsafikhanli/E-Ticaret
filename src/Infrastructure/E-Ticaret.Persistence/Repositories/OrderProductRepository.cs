using System;
using E_Ticaret.Application.Abstracts.Repositories;
using E_Ticaret.Domain.Entities;
using E_Ticaret.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace E_Ticaret.Persistence.Repositories;

public class OrderProductRepository : Repository<OrderProduct>, IOrderProductRepository
{
    private readonly E_TicaretDbContext _context;

    public OrderProductRepository(E_TicaretDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<List<OrderProduct>> GetOrderProductsByOrderIdAsync(Guid orderId)
    {
        return await _context.OrderProducts
            .Where(op => op.OrderId == orderId)
            .Include(op => op.Product)
            .ToListAsync();
    }

    public async Task<OrderProduct?> GetByOrderIdAndProductIdAsync(Guid orderId, Guid productId)
    {
        return await _context.OrderProducts
            .FirstOrDefaultAsync(op => op.OrderId == orderId && op.ProductId == productId);
    }
}