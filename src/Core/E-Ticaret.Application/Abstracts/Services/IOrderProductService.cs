using E_Ticaret.Application.DTOs.OrderProductDtos;
using E_Ticaret.Application.Shared;

namespace E_Ticaret.Application.Abstracts.Services;

public interface IOrderProductService
{
    Task<BaseResponse<List<OrderProductGetDto>>> GetByOrderIdAsync(Guid orderId);
    Task<BaseResponse<string>> AddAsync(OrderProductCreateDto dto, Guid orderId);
    Task<BaseResponse<string>> UpdateAsync(OrderProductUpdateDto dto);
    Task<BaseResponse<string>> DeleteAsync(Guid orderId, Guid productId);
}
