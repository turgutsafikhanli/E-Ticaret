using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Ticaret.Application.DTOs.FavouriteDtos;

public record class FavoriteCreateDto
{
    public string Name { get; set; } = null!;
    public Guid ProductId { get; set; }
}
