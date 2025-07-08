using E_Ticaret.Application.Abstracts.Services;
using E_Ticaret.Application.DTOs.FavouriteDtos;
using E_Ticaret.Application.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace E_Ticaret.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FavouritesController : ControllerBase
{
    private readonly IFavouriteService _favouriteService;

    public FavouritesController(IFavouriteService favouriteService)
    {
        _favouriteService = favouriteService;
    }

    [HttpPost]
    [Authorize(Policy = Permissions.Favourite.Create)]
    public async Task<IActionResult> Add([FromBody] FavouriteCreateDto dto)
    {
        var response = await _favouriteService.AddAsync(dto);
        return StatusCode((int)response.StatusCode, response);
    }

    [HttpDelete("{id}")]
    [Authorize(Policy = Permissions.Favourite.Delete)]
    public async Task<IActionResult> Delete(Guid id)
    {
        var response = await _favouriteService.DeleteAsync(id);
        return StatusCode((int)response.StatusCode, response);
    }

    [HttpPut]
    [Authorize(Policy = Permissions.Favourite.Update)]
    public async Task<IActionResult> Update([FromBody] FavouriteUpdateDto dto)
    {
        var response = await _favouriteService.UpdateAsync(dto);
        return StatusCode((int)response.StatusCode, response);
    }

    [HttpGet("{id}")]
    [Authorize(Policy = Permissions.Favourite.Get)]
    public async Task<IActionResult> GetById(Guid id)
    {
        var response = await _favouriteService.GetByIdAsync(id);
        return StatusCode((int)response.StatusCode, response);
    }

    [HttpGet]
    [Authorize(Policy = Permissions.Favourite.Get)]
    public async Task<IActionResult> GetAll()
    {
        var response = await _favouriteService.GetAllAsync();
        return StatusCode((int)response.StatusCode, response);
    }

    [HttpGet("by-name")]
    [Authorize(Policy = Permissions.Favourite.Get)]
    public async Task<IActionResult> GetByName([FromQuery] string search)
    {
        var response = await _favouriteService.GetByNameAsync(search);
        return StatusCode((int)response.StatusCode, response);
    }

    [HttpGet("search")]
    [Authorize(Policy = Permissions.Favourite.Get)]
    public async Task<IActionResult> GetByNameSearch([FromQuery] string namePart)
    {
        var response = await _favouriteService.GetByNameSearchAsync(namePart);
        return StatusCode((int)response.StatusCode, response);
    }
}
