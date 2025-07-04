using E_Ticaret.Application.DTOs.CategoryDtos;
using E_Ticaret.Application.Shared;

namespace E_Ticaret.Application.Abstracts.Services;

public interface ICategoryService
{
    Task<BaseResponse<string>> AddAsync(CategoryCreateDto dto);
    Task<BaseResponse<string>> DeleteAsync(Guid id);
    Task<BaseResponse<CategoryUpdateDto>> UpdateAsync(CategoryUpdateDto dto);
    Task<BaseResponse<CategoryGetDto>> GetByIdAsync(Guid id);
    Task<BaseResponse<CategoryGetDto>> GetByNameAsync(string search);
    Task<BaseResponse<List<CategoryGetDto>>> GetAllAsync();
    Task<BaseResponse<List<CategoryGetDto>>> GetByNameSearchAsync(string namePart);
    Task<BaseResponse<CategoryTreeDto>> GetTreeAsync(Guid mainCategoryId);
    Task<BaseResponse<List<CategoryTreeDto>>> GetAllMainCategoriesWithTreeAsync();
}
