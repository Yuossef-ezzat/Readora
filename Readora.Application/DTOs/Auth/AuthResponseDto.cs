using System;

namespace Readora.Application.DTOs.Auth;

public class AuthResponseDto
{
	public string Token { get; set; } = string.Empty;

	public string RefreshToken { get; set; } = string.Empty;

	public DateTime RefreshTokenExpiration { get; set; }
}
