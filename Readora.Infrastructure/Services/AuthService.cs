using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Readora.Application.Common;
using Readora.Application.DTOs.Auth;
using Readora.Application.Interfaces.Services;
using Readora.Domain.Entities;

namespace Readora.Infrastructure.Services;

public class AuthService : IAuthService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ITokenService _tokenService;
    private readonly IOtpService _otpService;
    private readonly IEmailService _emailService;

    public AuthService(UserManager<ApplicationUser> userManager, ITokenService tokenService, IOtpService otpService, IEmailService emailService)
    {
        _userManager = userManager;
        _tokenService = tokenService;
        _otpService = otpService;
        _emailService = emailService;
    }

    public async Task<Result<AuthResponseDto>> RegisterAsync(RegisterRequestDto request)
    {
        if (await _userManager.FindByEmailAsync(request.Email) != null)
        {
            return Result.Failure<AuthResponseDto>(Error.Conflict("Auth.EmailExists", "Email is already registered."));
        }

        var user = new ApplicationUser
        {
            UserName = request.Email,
            Email = request.Email,
            FirstName = request.FirstName,
            LastName = request.LastName
        };

        var result = await _userManager.CreateAsync(user, request.Password);
        
        if (!result.Succeeded)
        {
            var errors = string.Join(", ", result.Errors.Select(e => e.Description));
            return Result.Failure<AuthResponseDto>(Error.Validation("Auth.RegisterFailed", errors));
        }

        var roleResult = await _userManager.AddToRoleAsync(user, "Member");
        if (!roleResult.Succeeded)
        {
            var errors = string.Join(", ", roleResult.Errors.Select(e => e.Description));

            return Result.Failure<AuthResponseDto>(Error.Validation("Auth.RoleAssignmentFailed",errors));
        }
        return await GenerateAuthResponseAsync(user);
    }

    public async Task<Result> CreateLibrarianAsync(RegisterRequestDto request)
    {
        if (await _userManager.FindByEmailAsync(request.Email) != null)
        {
            return Result.Failure(Error.Conflict("Auth.EmailExists", "Email is already registered."));
        }

        var user = new ApplicationUser
        {
            UserName = request.Email,
            Email = request.Email,
            FirstName = request.FirstName,
            LastName = request.LastName
        };

        var result = await _userManager.CreateAsync(user, request.Password);
        
        if (!result.Succeeded)
        {
            var errors = string.Join(", ", result.Errors.Select(e => e.Description));
            return Result.Failure(Error.Validation("Auth.CreateFailed", errors));
        }

        var roleResult = await _userManager.AddToRoleAsync(user, "Librarian");
        if (!roleResult.Succeeded)
        {
            var errors = string.Join(", ", roleResult.Errors.Select(e => e.Description));
            return Result.Failure(Error.Validation("Auth.RoleAssignmentFailed", errors));
        }
        return Result.Success();
    }

    public async Task<Result<AuthResponseDto>> LoginAsync(LoginRequestDto request)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);
        
        if (user == null || !await _userManager.CheckPasswordAsync(user, request.Password))
        {
            return Result.Failure<AuthResponseDto>(Error.Unauthorized("Auth.InvalidCredentials", "Invalid email or password."));
        }

        return await GenerateAuthResponseAsync(user);
    }
    public async Task LogoutAsync( string? refreshToken = null)
    {
        if (!string.IsNullOrEmpty(refreshToken))
        {
            await _tokenService.RevokeRefreshTokenAsync(refreshToken);
        }
        // Return nothing as the return type is Task
    }
    public async Task<Result<AuthResponseDto>> RefreshTokenAsync(RefreshTokenRequestDto request)
    {
        if (string.IsNullOrWhiteSpace(request.RefreshToken))
            return Result.Failure<AuthResponseDto>(Error.Unauthorized("Auth.RefreshTokenRequired","Refresh token is required."));
        var result = await _tokenService.RefreshAccessTokenAsync(request.RefreshToken);
        if (result.IsFailure)
        {
            return Result.Failure<AuthResponseDto>(result.Error);
        }
        return Result.Success(new AuthResponseDto
        {
            Token = result.Value.AccessToken,
            RefreshToken = result.Value.RefreshToken,
            RefreshTokenExpiration = result.Value.RefreshTokenExpiresAt
        });
    }

    public async Task<Result> RevokeTokenAsync(string token)
    {
        return await _tokenService.RevokeRefreshTokenAsync(token);
    }

    public async Task<Result> ForgotPasswordAsync(string email)
    {
        if (await _userManager.FindByEmailAsync(email) == null)
        {
            return Result.Success();
        }

        var otp = await _otpService.GenerateAndStoreOtpAsync(email);
        await _emailService.SendEmailAsync(email, "Readora - Password Reset", $"Your password reset code is: {otp}. It will expire in 10 minutes.");
        
        return Result.Success();
    }

    public async Task<Result> VerifyOtpAsync(VerifyOtpRequestDto request)
    {
        if (!await _otpService.VerifyOtpAsync(request.Email, request.Otp))
        {
            return Result.Failure(Error.Validation("Auth.InvalidOtp", "Invalid or expired OTP."));
        }
        return Result.Success();
    }

    public async Task<Result> ResetPasswordAsync(ResetPasswordRequestDto request)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user == null)
        {
            return Result.Failure(Error.NotFound("Auth.UserNotFound", "User not found."));
        }

        if (!await _otpService.VerifyOtpAsync(request.Email, request.Otp))
        {
            return Result.Failure(Error.Validation("Auth.InvalidOtp", "Invalid or expired OTP."));
        }

        var resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
        var result = await _userManager.ResetPasswordAsync(user, resetToken, request.NewPassword);
        
        if (!result.Succeeded)
        {
            var errors = string.Join(", ", result.Errors.Select(e => e.Description));
            return Result.Failure(Error.Validation("Auth.ResetFailed", errors));
        }

        return Result.Success();
    }

    private async Task<Result<AuthResponseDto>> GenerateAuthResponseAsync(ApplicationUser user)
    {
        var role = (await _userManager.GetRolesAsync(user)).FirstOrDefault();
        var accessToken = await _tokenService.GenerateAccessToken(new TokenUserDto
        {
            UserId = user.Id,
            Email = user.Email,
            Role = role
        });
        
        var refreshTokenResult = await _tokenService.GenerateRefreshTokenAsync(user.Id);
        if (!refreshTokenResult.IsSuccess)
        {
            return Result.Failure<AuthResponseDto>(refreshTokenResult.Error);
        }

        return Result.Success(new AuthResponseDto
        {
            Token = accessToken.Value,
            RefreshToken = refreshTokenResult.Value.Token,
            RefreshTokenExpiration = DateTime.UtcNow.AddDays(7)
        });
    }
}
