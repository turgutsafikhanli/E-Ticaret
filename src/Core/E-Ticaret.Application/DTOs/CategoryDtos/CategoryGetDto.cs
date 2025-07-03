namespace E_Ticaret.Application.DTOs.CategoryDtos;

public record class CategoryGetDto
{
    public string Name
    { get; set; } = null!;
    public Guid Id
    { get; set; }
}
