using System.Net;
using System.Security.Claims;
using E_Ticaret.Application.Abstracts.Services;
using E_Ticaret.Application.DTOs.ProductDtos;
using E_Ticaret.Application.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace E_Ticaret.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;
    private readonly IFileUploadService _fileUploadService;

    public ProductsController(IProductService productService, IFileUploadService fileUploadService)
    {
        _productService = productService;
        _fileUploadService = fileUploadService;
    }

    [HttpPost]
    [Authorize(Policy = Permissions.Product.Create)]
    [ProducesResponseType(typeof(BaseResponse<string>), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(BaseResponse<string>), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(BaseResponse<string>), (int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> Create([FromForm] ProductCreateDto dto)
    {
        if (dto.Images == null || !dto.Images.Any())
            return BadRequest(new BaseResponse<string>(HttpStatusCode.BadRequest)
            {
                Success = false,
                Message = "No images provided"
            });

        var uploadedImageUrls = new List<string>();

        foreach (var image in dto.Images)
        {
            var url = await _fileUploadService.UploadAsync(image);
            uploadedImageUrls.Add(url);
        }

        dto.ImageUrls = uploadedImageUrls;

        var result = await _productService.CreateAsync(dto);
        return StatusCode((int)result.StatusCode, result);
    }

    [HttpPut("{id:guid}")]
    [Authorize(Policy = Permissions.Product.Update)]
    [ProducesResponseType(typeof(BaseResponse<string>), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(BaseResponse<string>), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(BaseResponse<string>), (int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> Update(Guid id, [FromBody] ProductUpdateDto dto)
    {
        if (id != dto.Id)
        {
            return BadRequest(new BaseResponse<string>(HttpStatusCode.BadRequest)
            {
                Success = false,
                Message = "ID mismatch between route and body"
            });
        }

        var result = await _productService.UpdateAsync(dto);
        return StatusCode((int)result.StatusCode, result);
    }

    [HttpDelete("{id:guid}")]
    [Authorize(Policy = Permissions.Product.Delete)]
    [ProducesResponseType(typeof(BaseResponse<string>), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(BaseResponse<string>), (int)HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(BaseResponse<string>), (int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await _productService.DeleteAsync(id);
        return StatusCode((int)result.StatusCode, result);
    }

    [HttpGet("{id:guid}")]
    [Authorize(Policy = Permissions.Product.Get)]
    [ProducesResponseType(typeof(BaseResponse<ProductGetDto>), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(BaseResponse<string>), (int)HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(BaseResponse<string>), (int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _productService.GetByIdAsync(id);
        return StatusCode((int)result.StatusCode, result);
    }

    [HttpGet]
    [Authorize(Policy = Permissions.Product.GetAll)]
    [ProducesResponseType(typeof(BaseResponse<List<ProductGetDto>>), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(BaseResponse<string>), (int)HttpStatusCode.InternalServerError)]
    [ProducesResponseType(typeof(BaseResponse<string>), (int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> GetAll()
    {
        var result = await _productService.GetAllAsync();
        return StatusCode((int)result.StatusCode, result);
    }

    [HttpGet("GetByUserId")]
    [Authorize(Policy = Permissions.Product.GetByUserId)]
    [ProducesResponseType(typeof(BaseResponse<List<ProductGetDto>>), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(BaseResponse<string>), (int)HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(BaseResponse<string>), (int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> GetByUserId(string userId)
    {
        var result = await _productService.GetByUserIdAsync(userId);
        return StatusCode((int)result.StatusCode, result);
    }

    [HttpGet("GetByCategoryId")]
    [Authorize(Policy = Permissions.Product.GetByCategoryId)]
    [ProducesResponseType(typeof(BaseResponse<List<ProductGetDto>>), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(BaseResponse<string>), (int)HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(BaseResponse<string>), (int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> GetByCategoryId(Guid categoryId)
    {
        var result = await _productService.GetByCategoryIdAsync(categoryId);
        return StatusCode((int)result.StatusCode, result);
    }

    [HttpGet("GetByName")]
    [Authorize(Policy = Permissions.Product.GetByName)]
    [ProducesResponseType(typeof(BaseResponse<ProductGetDto>), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(BaseResponse<string>), (int)HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(BaseResponse<string>), (int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> GetByName(string name)
    {
        var result = await _productService.GetByNameAsync(name);
        return StatusCode((int)result.StatusCode, result);
    }
    [HttpGet("my")]
    [Authorize(Policy = Permissions.Product.GetMyProducts)]
    [ProducesResponseType(typeof(BaseResponse<List<ProductGetDto>>), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(BaseResponse<string>), (int)HttpStatusCode.Unauthorized)]
    [ProducesResponseType(typeof(BaseResponse<string>), (int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> GetMyProducts()
    {
        var userId = User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userId))
        {
            return Unauthorized(new BaseResponse<string>("İstifadəçi identifikasiyası tapılmadı.", false, HttpStatusCode.Unauthorized));
        }

        var result = await _productService.GetByUserIdAsync(userId);
        return StatusCode((int)result.StatusCode, result);
    }
}
