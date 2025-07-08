using System.Net;
using AutoMapper;
using E_Ticaret.Application.Abstracts.Repositories;
using E_Ticaret.Application.Abstracts.Services;
using E_Ticaret.Application.DTOs.CategoryDtos;
using E_Ticaret.Application.Shared;
using E_Ticaret.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace E_Ticaret.Persistence.Services;

public class CategoryService : ICategoryService
{
    private ICategoryRepository _categoryRepository { get; }
    private readonly IMapper _mapper; // AutoMapper istifadə olunur

    public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
    {
        _categoryRepository = categoryRepository;
        _mapper = mapper;
    }

    public async Task<BaseResponse<string>> AddAsync(CategoryCreateDto dto)
    {
        var categoryDb = await _categoryRepository.GetByFiltered(c => c.Name.Trim().ToLower() == dto.Name.Trim().ToLower()).FirstOrDefaultAsync();
        if (categoryDb is not null)
        {
            return new BaseResponse<string>("This category already exists", System.Net.HttpStatusCode.BadRequest);
        }
        Category category = new()
        {
            Name = dto.Name.Trim(),
            MainCategoryId = dto.MainCategoryId
        };
        await _categoryRepository.AddAsync(category);
        await _categoryRepository.SaveChangeAsync();
        return new BaseResponse<string>(System.Net.HttpStatusCode.Created);
    }

    public async Task<BaseResponse<string>> DeleteAsync(Guid id)
    {
        var category = await _categoryRepository.GetByIdAsync(id);
        if (category is null)
            return new BaseResponse<string>("Category not found", HttpStatusCode.NotFound);

        // Alt kateqoriya varsa silməyə icazə vermə
        var hasSubCategories = _categoryRepository
            .GetByFiltered(c => c.MainCategoryId == id && !c.IsDeleted)
            .Any();

        if (hasSubCategories)
        {
            return new BaseResponse<string>("This category has sub-categories and cannot be deleted", HttpStatusCode.BadRequest);
        }

        await _categoryRepository.SoftDeleteAsync(category);

        return new BaseResponse<string>("Category soft-deleted successfully", HttpStatusCode.OK);
    }

    public async Task<BaseResponse<List<CategoryGetDto>>> GetAllAsync()
    {
        var categories = _categoryRepository.GetAll();
        if (categories is null)
        {
            return new BaseResponse<List<CategoryGetDto>>(HttpStatusCode.NotFound);
        }
        var dtoList = new List<CategoryGetDto>();
        foreach (var category in categories)
        {
            dtoList.Add(new CategoryGetDto
            {
                Id = category.Id,
                Name = category.Name
            });
        }
        return new BaseResponse<List<CategoryGetDto>>("Data", dtoList, HttpStatusCode.OK);


    }

    public async Task<BaseResponse<CategoryGetDto>> GetByIdAsync(Guid id)
    {
        var category = await _categoryRepository.GetByIdAsync(id);
        if (category is null)
        {
            return new BaseResponse<CategoryGetDto>(HttpStatusCode.NotFound);
        }
        var dto = new CategoryGetDto
        {
            Id = category.Id,
            Name = category.Name
        };
        return new BaseResponse<CategoryGetDto>("Data", dto, HttpStatusCode.OK);
    }

    public Task<BaseResponse<CategoryGetDto>> GetByNameAsync(string search)
    {
        var categories = _categoryRepository.GetAll();
        var dtoCategory = new CategoryGetDto();
        foreach (var category in categories)
        {
            if (category.Name == search)
            {
                dtoCategory.Name = category.Name;
                dtoCategory.Id = category.Id;
            }

        }
        if (dtoCategory is null)
        {
            return Task.FromResult(new BaseResponse<CategoryGetDto>(HttpStatusCode.NotFound));
        }
        return Task.FromResult(new BaseResponse<CategoryGetDto>("Data", dtoCategory, HttpStatusCode.OK));

    }

    public async Task<BaseResponse<CategoryUpdateDto>> UpdateAsync(CategoryUpdateDto dto)
    {
        var categoryDb = await _categoryRepository.GetByIdAsync(dto.Id);
        if (categoryDb is not null)
        {
            return new BaseResponse<CategoryUpdateDto>(HttpStatusCode.NotFound);
        }

        var existedCategory = await _categoryRepository
            .GetByFiltered(c => c.Name.Trim().ToLower() == dto.Name.Trim().ToLower())
            .FirstOrDefaultAsync();
        if (existedCategory is not null)
        {
            return new BaseResponse<CategoryUpdateDto>("This category already exists", HttpStatusCode.BadRequest);
        }
        categoryDb.Name = dto.Name.Trim();



        await _categoryRepository.SaveChangeAsync();
        return new BaseResponse<CategoryUpdateDto>("Category updated successfully", dto, HttpStatusCode.OK);
    }

    public async Task<BaseResponse<List<CategoryGetDto>>> GetByNameSearchAsync(string namePart)
    {
        var categories = await _categoryRepository.GetByNameSearchAsync(namePart);
        if (categories == null || !categories.Any())
        {
            return new BaseResponse<List<CategoryGetDto>>("No categories found with the given name part", HttpStatusCode.NotFound);
        }

        return new BaseResponse<List<CategoryGetDto>>("Data", _mapper.Map<List<CategoryGetDto>>(categories), HttpStatusCode.OK);
    }

    public async Task<BaseResponse<CategoryTreeDto>> GetTreeAsync(Guid mainCategoryId)
    {
        var mainCategory = await _categoryRepository.GetByIdAsync(mainCategoryId);
        if (mainCategory == null)
        {
            return new BaseResponse<CategoryTreeDto>("Main category not found", HttpStatusCode.NotFound);
        }

        CategoryTreeDto BuildTree(Domain.Entities.Category category)
        {
            var dto = new CategoryTreeDto
            {
                Id = category.Id,
                Name = category.Name,
                SubCategories = new List<CategoryTreeDto>()
            };

            var children = _categoryRepository.GetByFiltered(c => c.MainCategoryId == category.Id).ToList();

            foreach (var child in children)
            {
                dto.SubCategories.Add(BuildTree(child));
            }

            return dto;
        }

        var tree = BuildTree(mainCategory);

        return new BaseResponse<CategoryTreeDto>("Data", tree, HttpStatusCode.OK);
    }

    public async Task<BaseResponse<List<CategoryTreeDto>>> GetAllMainCategoriesWithTreeAsync()
    {
        var mainCategories = _categoryRepository.GetByFiltered(c => c.MainCategoryId == null).ToList();

        if (mainCategories == null || !mainCategories.Any())
        {
            return new BaseResponse<List<CategoryTreeDto>>("No main categories found", HttpStatusCode.NotFound);
        }

        List<CategoryTreeDto> result = new();

        CategoryTreeDto BuildTree(Category category)
        {
            var dto = new CategoryTreeDto
            {
                Id = category.Id,
                Name = category.Name,
                SubCategories = new List<CategoryTreeDto>()
            };

            var children = _categoryRepository.GetByFiltered(c => c.MainCategoryId == category.Id).ToList();

            foreach (var child in children)
            {
                dto.SubCategories.Add(BuildTree(child));
            }

            return dto;
        }

        foreach (var mainCategory in mainCategories)
        {
            result.Add(BuildTree(mainCategory));
        }

        return new BaseResponse<List<CategoryTreeDto>>("Data", result, HttpStatusCode.OK);
    }
}