using System.Threading.Tasks;
using Readora.Application.Common;
using Readora.Application.DTOs.Auth;

namespace Readora.Application.Interfaces.Services;

public interface IAuthService
{
	Task<Result<AuthResponseDto>> RegisterAsync(RegisterRequestDto request);

	Task<Result> CreateLibrarianAsync(RegisterRequestDto request);

	Task<Result<AuthResponseDto>> LoginAsync(LoginRequestDto request);

	Task<Result<AuthResponseDto>> RefreshTokenAsync(RefreshTokenRequestDto request);

	Task<Result> RevokeTokenAsync(string token);

	Task<Result> ForgotPasswordAsync(string email);

	Task<Result> VerifyOtpAsync(VerifyOtpRequestDto request);

	Task<Result> ResetPasswordAsync(ResetPasswordRequestDto request);
}
