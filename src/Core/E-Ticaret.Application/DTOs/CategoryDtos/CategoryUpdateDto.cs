namespace E_Ticaret.Application.DTOs.CategoryDtos;

public record class CategoryUpdateDto
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
}
