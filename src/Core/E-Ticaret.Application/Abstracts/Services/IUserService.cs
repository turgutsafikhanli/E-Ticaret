using E_Ticaret.Application.DTOs.UserDtos;
using E_Ticaret.Application.Shared;

namespace E_Ticaret.Application.Abstracts.Services;

public interface IUserService
{
    Task<BaseResponse<string>> Register(UserRegisterDto dto);
    Task<BaseResponse<TokenResponse>> Login(UserLoginDto dto);
    Task<BaseResponse<TokenResponse>> RefreshTokenAsync(RefreshTokenRequest request);
    Task<BaseResponse<string>> AddRole(UserAddRoleDto dto);
    Task<BaseResponse<string>> ConfirmEmail(string userId, string token);
    Task<BaseResponse<string>> SendResetPasswordEmailAsync(string email);
    Task<BaseResponse<string>> ResetPasswordAsync(UserResetPasswordDto dto);
    Task<BaseResponse<TokenResponse>> HandleExternalLoginAsync(string email, string name);
}
