namespace E_Ticaret.Application.DTOs.ProductDtos;

public record class ProductWithImagesDto : ProductGetDto
{
    public List<string> ImageUrls { get; set; } = new();
}
