using E_Ticaret.Application.Abstracts.Services;
using E_Ticaret.Application.DTOs.FileUploadDtos;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace E_Ticaret.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FilesController : ControllerBase
{
    private readonly IFileUploadService _fileUpload;
    public FilesController(IFileUploadService fileUpload)
    {
        _fileUpload = fileUpload;
    }
    // GET: api/<FilesController>
    [HttpGet]
    public IEnumerable<string> Get()
    {
        return new string[] { "value1", "value2" };
    }

    // GET api/<FilesController>/5
    [HttpGet("{id}")]
    public string Get(int id)
    {
        return "value";
    }

    // POST api/<FilesController>
    [HttpPost("upload")]
    public async Task<IActionResult> Upload([FromForm] FileUploadDto dto)
    {
        if (dto.File == null || dto.File.Length == 0)
        {
            return BadRequest("No file uploaded.");
        }

        try
        {
            var filePath = await _fileUpload.UploadAsync(dto.File);
            return Ok(new { FilePath = filePath });
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    // PUT api/<FilesController>/5
    [HttpPut("{id}")]
    public void Put(int id, [FromBody] string value)
    {
    }

    // DELETE api/<FilesController>/5
    [HttpDelete("{id}")]
    public void Delete(int id)
    {
    }
}
