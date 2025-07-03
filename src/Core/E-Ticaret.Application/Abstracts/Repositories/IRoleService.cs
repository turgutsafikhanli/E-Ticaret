using E_Ticaret.Application.DTOs.RoleDtos;
using E_Ticaret.Application.Shared;

namespace E_Ticaret.Application.Abstracts.Repositories;

public interface IRoleService
{
    Task<BaseResponse<string?>> CreateRole(RoleCreateDto dto);
}
