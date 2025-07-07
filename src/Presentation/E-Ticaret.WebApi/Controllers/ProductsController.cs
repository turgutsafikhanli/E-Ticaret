using System.Net;
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

    public ProductsController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpPost]
    [Authorize(Policy = "Permissions.Product.Create")]
    public async Task<IActionResult> Create([FromBody] ProductCreateDto dto)
    {
        var result = await _productService.CreateAsync(dto);
        return StatusCode((int)result.StatusCode, result);
    }

    [HttpPut("{id:guid}")]
    [Authorize(Policy = "Permissions.Product.Update")]
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
    [Authorize(Policy = "Permissions.Product.Delete")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await _productService.DeleteAsync(id);
        return StatusCode((int)result.StatusCode, result);
    }

    [HttpGet("{id:guid}")]
    [Authorize(Policy = "Permissions.Product.Get")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _productService.GetByIdAsync(id);
        return StatusCode((int)result.StatusCode, result);
    }

    [HttpGet]
    [Authorize(Policy = "Permissions.Product.Get")]
    public async Task<IActionResult> GetAll()
    {
        var result = await _productService.GetAllAsync();
        return StatusCode((int)result.StatusCode, result);
    }

    [HttpGet("user/{userId}")]
    [Authorize(Policy = "Permissions.Product.Get")]
    public async Task<IActionResult> GetByUserId(string userId)
    {
        var result = await _productService.GetByUserIdAsync(userId);
        return StatusCode((int)result.StatusCode, result);
    }

    [HttpGet("category/{categoryId:guid}")]
    [Authorize(Policy = "Permissions.Product.Get")]
    public async Task<IActionResult> GetByCategoryId(Guid categoryId)
    {
        var result = await _productService.GetByCategoryIdAsync(categoryId);
        return StatusCode((int)result.StatusCode, result);
    }

    [HttpGet("name/{name}")]
    [Authorize(Policy = "Permissions.Product.Get")]
    public async Task<IActionResult> GetByName(string name)
    {
        var result = await _productService.GetByNameAsync(name);
        return StatusCode((int)result.StatusCode, result);
    }

    [HttpGet("search")]
    [Authorize(Policy = "Permissions.Product.Get")]
    public async Task<IActionResult> Search([FromQuery] string keyword)
    {
        var result = await _productService.SearchAsync(keyword);
        return StatusCode((int)result.StatusCode, result);
    }
}
