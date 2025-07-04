using System.Net;
using E_Ticaret.Application.Abstracts.Services;
using E_Ticaret.Application.DTOs.CategoryDtos;
using E_Ticaret.Application.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace E_Ticaret.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoriesController : ControllerBase
{
    public CategoriesController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    private ICategoryService _categoryService { get; }

    // POST api/<CategoriesController>
    [HttpPost]
    [Authorize(Policy = Permissions.Category.Create)]
    [ProducesResponseType(typeof(BaseResponse<CategoryUpdateDto>), (int)HttpStatusCode.Created)]
    [ProducesResponseType(typeof(BaseResponse<string>), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(BaseResponse<string>), (int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> Post([FromBody] CategoryCreateDto dto)
    {
        var result = await _categoryService.AddAsync(dto);
        return StatusCode((int)result.StatusCode, result);
    }

    // PUT api/<CategoriesController>/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(Guid id, [FromBody] CategoryUpdateDto dto)
    {
        var result = await _categoryService.UpdateAsync(dto);
        return StatusCode((int)result.StatusCode, result);
    }

    [HttpGet()]
    [ProducesResponseType(typeof(BaseResponse<CategoryGetDto>), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(BaseResponse<string>), (int)HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(BaseResponse<string>), (int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> GetByIdAsync([FromQuery] Guid id, [FromHeader] string Connection)
    {
        var category = await _categoryService.GetByIdAsync(id);
        return StatusCode((int)category.StatusCode, category);
    }

    // DELETE api/<CategoriesController>/5
    [HttpDelete("{id}")]
    [Authorize(Policy = Permissions.Category.Delete)]
    public void Delete(int id)
    {
    }
    [HttpGet("search")]
    [ProducesResponseType(typeof(BaseResponse<List<CategoryGetDto>>), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(BaseResponse<string>), (int)HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(BaseResponse<string>), (int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> GetByNameSearchAsync(string search)
    {
        var category = await _categoryService.GetByNameSearchAsync(search);
        return StatusCode((int)category.StatusCode, category);
    }

    [HttpGet("tree/{mainCategoryId}")]
    [ProducesResponseType(typeof(BaseResponse<List<CategoryGetDto>>), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(BaseResponse<string>), (int)HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(BaseResponse<string>), (int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> GetTree(Guid mainCategoryId)
    {
        var response = await _categoryService.GetTreeAsync(mainCategoryId);
        return StatusCode((int)response.StatusCode, response);
    }

    [HttpGet("main-categories-with-tree")]
    [ProducesResponseType(typeof(BaseResponse<List<CategoryGetDto>>), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(BaseResponse<string>), (int)HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(BaseResponse<string>), (int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> GetAllMainCategoriesWithTree()
    {
        var response = await _categoryService.GetAllMainCategoriesWithTreeAsync();
        return StatusCode((int)response.StatusCode, response);
    }
}
