using E_Ticaret.Application.DTOs.OrderDtos;
using E_Ticaret.Application.Shared;

namespace E_Ticaret.Application.Abstracts.Services;

public interface IOrderService
{
    Task<BaseResponse<string>> CreateAsync(OrderCreateDto dto);
    Task<BaseResponse<string>> DeleteAsync(Guid id);
    Task<BaseResponse<OrderGetDto>> GetByIdAsync(Guid id);
    Task<BaseResponse<List<OrderGetDto>>> GetAllAsync();
    Task<BaseResponse<List<OrderGetDto>>> GetByUserIdAsync(string userId);
    Task<BaseResponse<string>> UpdateAsync(OrderUpdateDto dto);
}

