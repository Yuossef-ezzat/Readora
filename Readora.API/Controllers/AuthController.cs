using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Readora.API.Extensions;
using Readora.Application.DTOs.Auth;
using Readora.Application.Interfaces.Services;

namespace Readora.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
	private readonly IAuthService _authService;

	public AuthController(IAuthService authService)
	{
		_authService = authService;
	}

	[HttpPost("register")]
	public async Task<IActionResult> Register(RegisterRequestDto request)
	{
		return (await _authService.RegisterAsync(request)).ToActionResult();
	}

	[HttpPost("create-librarian")]
	[Authorize(Roles = "Admin")]
	public async Task<IActionResult> CreateLibrarian(RegisterRequestDto request)
	{
		return (await _authService.CreateLibrarianAsync(request)).ToActionResult();
	}

	[HttpPost("login")]
	public async Task<IActionResult> Login(LoginRequestDto request)
	{
		return (await _authService.LoginAsync(request)).ToActionResult();
	}

	[HttpPost("refresh-token")]
	public async Task<IActionResult> RefreshToken(RefreshTokenRequestDto request)
	{
		return (await _authService.RefreshTokenAsync(request)).ToActionResult();
	}

	[HttpPost("revoke-token")]
	[Authorize]
	public async Task<IActionResult> RevokeToken(string token)
	{
		return (await _authService.RevokeTokenAsync(token)).ToActionResult();
	}

	[HttpPost("forgot-password")]
	public async Task<IActionResult> ForgotPassword([FromBody] string email)
	{
		return (await _authService.ForgotPasswordAsync(email)).ToActionResult();
	}

	[HttpPost("verify-otp")]
	public async Task<IActionResult> VerifyOtp(VerifyOtpRequestDto request)
	{
		return (await _authService.VerifyOtpAsync(request)).ToActionResult();
	}

	[HttpPost("reset-password")]
	public async Task<IActionResult> ResetPassword(ResetPasswordRequestDto request)
	{
		return (await _authService.ResetPasswordAsync(request)).ToActionResult();
	}
}
