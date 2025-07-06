namespace E_Ticaret.Application.DTOs.OrderProductDtos;

public record class OrderProductUpdateDto
{
    public Guid OrderId { get; set; }
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
}

