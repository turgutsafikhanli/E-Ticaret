using System.Net;
using AutoMapper;
using E_Ticaret.Application.Abstracts.Repositories;
using E_Ticaret.Application.Abstracts.Services;
using E_Ticaret.Application.DTOs.FavouriteDtos;
using E_Ticaret.Application.Shared;
using E_Ticaret.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace E_Ticaret.Persistence.Services;

public class FavouriteService : IFavouriteService
{
    IFavouriteRepository _favoriteRepository;
    IMapper _mapper;
    public async Task<BaseResponse<string>> AddAsync(FavouriteCreateDto dto)
    {
        var exists = await _favoriteRepository
            .GetByFiltered(f => f.ProductId == dto.ProductId)
            .FirstOrDefaultAsync();

        if (exists is not null)
        {
            return new BaseResponse<string>("Bu elan artıq favorilərdədir", HttpStatusCode.BadRequest);
        }

        var entity = _mapper.Map<Favourite>(dto);
        await _favoriteRepository.AddAsync(entity);
        await _favoriteRepository.SaveChangeAsync();

        return new BaseResponse<string>("Favoriyə əlavə olundu", HttpStatusCode.Created);
    }

    public async Task<BaseResponse<string>> DeleteAsync(Guid id)
    {
        var favorite = await _favoriteRepository.GetByIdAsync(id);
        if (favorite is null)
        {
            return new BaseResponse<string>("Favori tapılmadı", HttpStatusCode.NotFound);
        }

        _favoriteRepository.Delete(favorite);
        await _favoriteRepository.SaveChangeAsync();

        return new BaseResponse<string>("Favori silindi", HttpStatusCode.OK);
    }

    public async Task<BaseResponse<List<FavouriteGetDto>>> GetAllAsync()
    {
        var favorites = _favoriteRepository.GetAll().ToList();
        if (!favorites.Any())
        {
            return new BaseResponse<List<FavouriteGetDto>>("Favorilər tapılmadı", HttpStatusCode.NotFound);
        }

        var dtos = _mapper.Map<List<FavouriteGetDto>>(favorites);
        return new BaseResponse<List<FavouriteGetDto>>("Data", dtos, HttpStatusCode.OK);
    }

    public async Task<BaseResponse<FavouriteGetDto>> GetByIdAsync(Guid id)
    {
        var favorite = await _favoriteRepository.GetByIdAsync(id);
        if (favorite is null)
        {
            return new BaseResponse<FavouriteGetDto>("Favori tapılmadı", HttpStatusCode.NotFound);
        }

        var dto = _mapper.Map<FavouriteGetDto>(favorite);
        return new BaseResponse<FavouriteGetDto>("Data", dto, HttpStatusCode.OK);
    }

    public async Task<BaseResponse<FavouriteGetDto>> GetByNameAsync(string search)
    {
        // Ad yoxdur deyə bu method lazımsızdır
        return new BaseResponse<FavouriteGetDto>("Favori adı yoxdur", HttpStatusCode.BadRequest);
    }

    public async Task<BaseResponse<List<FavouriteGetDto>>> GetByNameSearchAsync(string namePart)
    {
        // Name ilə search mümkün deyil
        return new BaseResponse<List<FavouriteGetDto>>("Ad ilə axtarış dəstəklənmir", HttpStatusCode.BadRequest);
    }

    public async Task<BaseResponse<FavouriteUpdateDto>> UpdateAsync(FavouriteUpdateDto dto)
    {
        var favorite = await _favoriteRepository.GetByIdAsync(dto.ProductId);
        if (favorite is null)
        {
            return new BaseResponse<FavouriteUpdateDto>("Favori tapılmadı", HttpStatusCode.NotFound);
        }

        // Əgər dəyişmək icazəlidirsə:
        favorite.ProductId = dto.ProductId;

        await _favoriteRepository.SaveChangeAsync();

        return new BaseResponse<FavouriteUpdateDto>("Favori yeniləndi", dto, HttpStatusCode.OK);
    }
}
