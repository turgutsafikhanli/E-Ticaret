using Microsoft.AspNetCore.Http;

namespace E_Ticaret.Application.DTOs.FileUploadDtos;

public record class FileUploadDto
{
    public IFormFile File { get; set; } = null!;
}
