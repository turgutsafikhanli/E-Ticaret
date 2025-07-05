namespace E_Ticaret.Application.DTOs.FavouriteDtos;

public record class FavouriteGetDto
{
    public string Name { get; set; } = null!;
    public Guid ProductId { get; set; }
}
