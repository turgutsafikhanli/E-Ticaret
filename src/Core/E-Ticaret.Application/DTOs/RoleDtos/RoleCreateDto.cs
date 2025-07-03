namespace E_Ticaret.Application.DTOs.RoleDtos;

public record class RoleCreateDto
{
    public string Name { get; set; } = null!;
    public List<string> PermissionsList { get; set; }
}
