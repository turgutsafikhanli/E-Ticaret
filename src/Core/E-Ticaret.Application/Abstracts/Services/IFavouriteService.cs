using E_Ticaret.Application.DTOs.FavouriteDtos;
using E_Ticaret.Application.Shared;

namespace E_Ticaret.Application.Abstracts.Services;

public interface IFavouriteService
{
    Task<BaseResponse<string>> AddAsync(FavouriteCreateDto dto);
    Task<BaseResponse<string>> DeleteAsync(Guid id);
    Task<BaseResponse<FavouriteUpdateDto>> UpdateAsync(FavouriteUpdateDto dto);
    Task<BaseResponse<FavouriteGetDto>> GetByIdAsync(Guid id);
    Task<BaseResponse<FavouriteGetDto>> GetByNameAsync(string search);
    Task<BaseResponse<List<FavouriteGetDto>>> GetAllAsync();
    Task<BaseResponse<List<FavouriteGetDto>>> GetByNameSearchAsync(string namePart);
}
