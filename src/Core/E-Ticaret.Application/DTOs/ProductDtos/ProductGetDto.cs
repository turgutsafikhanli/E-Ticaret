namespace E_Ticaret.Application.DTOs.ProductDtos;

public record class ProductGetDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string UserId { get; set; } = null!;
    public Guid CategoryId { get; set; }
}
