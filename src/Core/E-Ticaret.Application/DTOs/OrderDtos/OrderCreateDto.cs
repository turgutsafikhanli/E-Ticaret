using E_Ticaret.Application.DTOs.OrderProductDtos;
using E_Ticaret.Domain.Enums;

namespace E_Ticaret.Application.DTOs.OrderDtos;

public record class OrderCreateDto
{
    public string UserId { get; set; } = null!;
    public List<OrderProductCreateDto> Products { get; set; } = new();
    public OrderStatus Status { get; set; } = OrderStatus.Pending;
}
