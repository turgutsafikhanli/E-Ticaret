namespace E_Ticaret.Application.DTOs.UserDtos;

public record class UserRegisterDto
{
    public string Fullname { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string Email { get; set; } = null!;

}