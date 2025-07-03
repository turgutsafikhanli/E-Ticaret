using E_Ticaret.Application.Abstracts.Repositories;
using E_Ticaret.Application.DTOs.RoleDtos;
using E_Ticaret.Application.Shared.Helpers;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace E_Ticaret.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RolesController : ControllerBase
{
    private IRoleService _roleService { get; }

    public RolesController(IRoleService roleService)
    {
        _roleService = roleService;
    }
    // GET: api/<RolesController>
    [HttpGet("permissions")]

    public IActionResult GetAllPermissions()
    {
        var permissions = PermissionHelper.GetAllPermissions();
        return Ok(permissions);

    }

    [HttpPost]
    public async Task<IActionResult> CreateRole(RoleCreateDto dto)
    {
        var result = await _roleService.CreateRole(dto);
        return StatusCode((int)result.StatusCode, result);
    }
}
