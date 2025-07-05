using System;
using E_Ticaret.Application.Abstracts.Repositories;
using E_Ticaret.Domain.Entities;
using E_Ticaret.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace E_Ticaret.Persistence.Repositories;

public class OrderRepository : Repository<Order>, Application.Abstracts.Repositories.IOrderRepository
{
    private readonly E_TicaretDbContext _context;

    public OrderRepository(E_TicaretDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<List<Order>> GetAllAsync()
    {
        return await _context.Orders.ToListAsync();
    }

    public async Task<List<Order>> GetOrdersByUserIdAsync(string userId)
    {
        return await _context.Orders
            .Where(o => o.UserId == userId)
            .ToListAsync();
    }
}
