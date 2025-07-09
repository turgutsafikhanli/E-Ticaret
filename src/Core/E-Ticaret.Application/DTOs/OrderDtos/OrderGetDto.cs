using E_Ticaret.Domain.Enums;

namespace E_Ticaret.Application.DTOs.OrderDtos;

public record class OrderGetDto
{
    public Guid Id { get; set; }
    public string UserId { get; set; }
    public OrderStatus Status { get; set; }
}
