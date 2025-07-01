﻿namespace E_Ticaret.Application.DTOs.UserDtos;

public record class UserResetPasswordDto
{
    public string Email { get; set; } = string.Empty;
    public string Token { get; set; } = string.Empty;
    public string NewPassword { get; set; } = string.Empty;
}