namespace E_Ticaret.Persistence.Repositories;

using System;
using E_Ticaret.Application.Abstracts.Repositories;
using E_Ticaret.Domain.Entities;
using E_Ticaret.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

public class ProductRepository : Repository<Product>, IProductRepository
{
    private readonly E_TicaretDbContext _context;

    public ProductRepository(E_TicaretDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<List<Product>> GetByUserIdAsync(string userId)
    {
        return await _context.Products.Where(p => p.UserId == userId).ToListAsync();
    }

    public async Task<List<Product>> GetByCategoryIdAsync(Guid categoryId)
    {
        return await _context.Products.Where(p => p.CategoryId == categoryId).ToListAsync();
    }

    public async Task<List<Product>> GetByNameAsync(string name)
    {
        return await _context.Products.Where(p => p.Name.ToLower() == name.ToLower()).ToListAsync();
    }

    public async Task<List<Product>> SearchAsync(string keyword)
    {
        return await _context.Products
            .Where(p => p.Name.ToLower().Contains(keyword.ToLower()))
            .ToListAsync();
    }
}

