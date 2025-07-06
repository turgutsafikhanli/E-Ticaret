namespace E_Ticaret.Application.DTOs.OrderProductDtos;

public record class OrderProductGetDto
{
    public Guid ProductId { get; set; }
    public Guid OrderId { get; set; }
}
