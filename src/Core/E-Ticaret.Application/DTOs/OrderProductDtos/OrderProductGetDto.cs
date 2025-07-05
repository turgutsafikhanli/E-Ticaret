namespace E_Ticaret.Application.DTOs.OrderProductDtos;

public class OrderProductGetDto
{
    public Guid ProductId { get; set; }
    public string ProductName { get; set; } = null!;
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
}
