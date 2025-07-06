using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_Ticaret.Application.DTOs.ProductDtos;
using E_Ticaret.Application.Shared;

namespace E_Ticaret.Application.Abstracts.Services;

public interface IProductService
{
    Task<BaseResponse<string>> CreateAsync(ProductCreateDto dto);
    Task<BaseResponse<string>> UpdateAsync(ProductUpdateDto dto);
    Task<BaseResponse<string>> DeleteAsync(Guid id);
    Task<BaseResponse<ProductGetDto>> GetByIdAsync(Guid id);
    Task<BaseResponse<List<ProductGetDto>>> GetAllAsync();
    Task<BaseResponse<List<ProductGetDto>>> GetByUserIdAsync(string userId);
    Task<BaseResponse<List<ProductGetDto>>> GetByCategoryIdAsync(Guid categoryId);
    Task<BaseResponse<List<ProductGetDto>>> GetByNameAsync(string name);
    Task<BaseResponse<List<ProductGetDto>>> SearchAsync(string keyword);
}
