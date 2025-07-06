using Microsoft.AspNetCore.Http;

namespace E_Ticaret.Application.Abstracts.Services;

public interface IFileUploadService
{
    Task<string> UploadAsync(IFormFile file);
}
