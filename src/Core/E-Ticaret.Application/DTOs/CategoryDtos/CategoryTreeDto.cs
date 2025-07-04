namespace E_Ticaret.Application.DTOs.CategoryDtos;

public class CategoryTreeDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public List<CategoryTreeDto> SubCategories { get; set; }
}
