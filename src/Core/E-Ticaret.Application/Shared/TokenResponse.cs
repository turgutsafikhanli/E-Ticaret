﻿namespace E_Ticaret.Application.Shared;

public class TokenResponse
{
    public string? Token { get; set; }
    public string? RefreshToken { get; set; }
    public DateTime? ExpireDate { get; set; }
}
