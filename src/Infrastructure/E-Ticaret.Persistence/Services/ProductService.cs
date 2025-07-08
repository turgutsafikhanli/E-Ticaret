using System.Linq.Expressions;
using System.Net;
using E_Ticaret.Application.Abstracts.Repositories;
using E_Ticaret.Application.Abstracts.Services;
using E_Ticaret.Application.DTOs.ProductDtos;
using E_Ticaret.Application.Shared;
using E_Ticaret.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace E_Ticaret.Persistence.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;

    public ProductService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<BaseResponse<string>> CreateAsync(ProductCreateDto dto)
    {
        // Məhsul adının unikal olub-olmadığını yoxla
        var exists = await _productRepository
            .GetByFiltered(p => p.Name.Trim().ToLower() == dto.Name.Trim().ToLower())
            .AnyAsync();
        if (exists)
        {
            return new BaseResponse<string>(
                "A product with the same name already exists",
                HttpStatusCode.BadRequest
            );
        }

        var product = new Domain.Entities.Product
        {
            Name = dto.Name.Trim(),
            CategoryId = dto.CategoryId,
            UserId = dto.UserId,
            Images = dto.ImageUrls.Select(url => new Image { ImageUrl = url }).ToList()
        };

        await _productRepository.SoftDeleteAsync(product);

        return new BaseResponse<string>(HttpStatusCode.Created)
        {
            Data = product.Id.ToString(),
            Message = "Product created successfully"
        };
    }

    public async Task<BaseResponse<string>> DeleteAsync(Guid id)
    {
        var product = await _productRepository.GetByIdAsync(id);
        if (product is null)
        {
            return new BaseResponse<string>("Product not found", HttpStatusCode.NotFound);
        }

        _productRepository.Delete(product);
        await _productRepository.SaveChangeAsync();

        return new BaseResponse<string>("Product deleted successfully", HttpStatusCode.OK);
    }

    public async Task<BaseResponse<List<ProductGetDto>>> GetAllAsync()
    {
        var products = _productRepository.GetAll()
            .Include(p => p.Images)
            .ToList();

        if (!products.Any())
        {
            return new BaseResponse<List<ProductGetDto>>("No products found", HttpStatusCode.NotFound);
        }

        var dtoList = products.Select(p => new ProductGetDto
        {
            Id = p.Id,
            Name = p.Name,
            CategoryId = p.CategoryId,
            UserId = p.UserId
        }).ToList();

        return new BaseResponse<List<ProductGetDto>>("Data", dtoList, HttpStatusCode.OK);
    }

    public async Task<BaseResponse<ProductGetDto>> GetByIdAsync(Guid id)
    {
        var product = await _productRepository
            .GetByFiltered(p => p.Id == id, include: new[] { (Expression<Func<Domain.Entities.Product, object>>)(p => p.Images) })
            .FirstOrDefaultAsync();
        if (product is null)
        {
            return new BaseResponse<ProductGetDto>("Product not found", HttpStatusCode.NotFound);
        }

        var dto = new ProductGetDto
        {
            Id = product.Id,
            Name = product.Name,
            CategoryId = product.CategoryId,
            UserId = product.UserId
        };

        return new BaseResponse<ProductGetDto>("Data", dto, HttpStatusCode.OK);
    }

    public async Task<BaseResponse<List<ProductGetDto>>> GetByUserIdAsync(string userId)
    {
        var products = await _productRepository.GetByUserIdAsync(userId);
        if (!products.Any())
        {
            return new BaseResponse<List<ProductGetDto>>("No products found for user", HttpStatusCode.NotFound);
        }

        var dtoList = products.Select(p => new ProductGetDto
        {
            Id = p.Id,
            Name = p.Name,
            CategoryId = p.CategoryId,
            UserId = p.UserId
        }).ToList();

        return new BaseResponse<List<ProductGetDto>>("Data", dtoList, HttpStatusCode.OK);
    }

    public async Task<BaseResponse<List<ProductGetDto>>> GetByCategoryIdAsync(Guid categoryId)
    {
        var products = await _productRepository.GetByCategoryIdAsync(categoryId);
        if (!products.Any())
        {
            return new BaseResponse<List<ProductGetDto>>("No products found in this category", HttpStatusCode.NotFound);
        }

        var dtoList = products.Select(p => new ProductGetDto
        {
            Id = p.Id,
            Name = p.Name,
            CategoryId = p.CategoryId,
            UserId = p.UserId
        }).ToList();

        return new BaseResponse<List<ProductGetDto>>("Data", dtoList, HttpStatusCode.OK);
    }

    public async Task<BaseResponse<List<ProductGetDto>>> GetByNameAsync(string name)
    {
        var products = await _productRepository.GetByNameAsync(name);
        if (!products.Any())
        {
            return new BaseResponse<List<ProductGetDto>>("No products found with this exact name", HttpStatusCode.NotFound);
        }

        var dtoList = products.Select(p => new ProductGetDto
        {
            Id = p.Id,
            Name = p.Name,
            CategoryId = p.CategoryId,
            UserId = p.UserId
        }).ToList();

        return new BaseResponse<List<ProductGetDto>>("Data", dtoList, HttpStatusCode.OK);
    }

    public async Task<BaseResponse<List<ProductGetDto>>> SearchAsync(string keyword)
    {
        var products = await _productRepository.SearchAsync(keyword);
        if (!products.Any())
        {
            return new BaseResponse<List<ProductGetDto>>("No products match the search keyword", HttpStatusCode.NotFound);
        }

        var dtoList = products.Select(p => new ProductGetDto
        {
            Id = p.Id,
            Name = p.Name,
            CategoryId = p.CategoryId,
            UserId = p.UserId
        }).ToList();

        return new BaseResponse<List<ProductGetDto>>("Data", dtoList, HttpStatusCode.OK);
    }

    public async Task<BaseResponse<string>> UpdateAsync(ProductUpdateDto dto)
    {
        var product = await _productRepository.GetByIdAsync(dto.Id);
        if (product is null)
        {
            return new BaseResponse<string>("Product not found", HttpStatusCode.NotFound);
        }

        // Yenilənəcək adı başqa məhsulda var?
        var conflict = await _productRepository
            .GetByFiltered(p => p.Name.Trim().ToLower() == dto.Name.Trim().ToLower() && p.Id != dto.Id)
            .AnyAsync();
        if (conflict)
        {
            return new BaseResponse<string>(
                "Another product with the same name exists",
                HttpStatusCode.BadRequest
            );
        }

        product.Name = dto.Name.Trim();
        product.CategoryId = dto.CategoryId;

        _productRepository.Update(product);
        await _productRepository.SaveChangeAsync();

        return new BaseResponse<string>("Product updated successfully", HttpStatusCode.OK);
    }
}
