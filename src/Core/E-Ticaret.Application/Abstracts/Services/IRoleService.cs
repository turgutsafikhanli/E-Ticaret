using E_Ticaret.Application.DTOs.RoleDtos;
using E_Ticaret.Application.Shared;

namespace E_Ticaret.Application.Abstracts.Services;

public interface IRoleService
{
    Task<BaseResponse<string?>> CreateRole(RoleCreateDto dto);
}
