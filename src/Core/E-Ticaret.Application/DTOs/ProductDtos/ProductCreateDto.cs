﻿using Microsoft.AspNetCore.Http;

namespace E_Ticaret.Application.DTOs.ProductDtos;

public record class ProductCreateDto
{
    public string Name { get; set; } = null!;
    public Guid CategoryId { get; set; }
    public string UserId { get; set; } = null!;
    public List<IFormFile>? Images { get; set; } = new();
    public List<string>? ImageUrls { get; set; } = new();
}
