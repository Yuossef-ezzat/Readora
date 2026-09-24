using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Readora.Application.Common;
using Readora.Application.DTOs.Auth;
using Readora.Application.Interfaces.Persistence;
using Readora.Application.Interfaces.Services;
using Readora.Application.Specifications;
using Readora.Domain.Entities;
using Readora.Infrastructure.Authentication;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Readora.Infrastructure.Services;

public class TokenService : ITokenService
{
    private readonly JwtSettings _jwtSettings;
    private readonly IUnitOfWork _unitOfWork;
    private readonly UserManager<ApplicationUser> _userManager;
    public TokenService(IOptions<JwtSettings> jwtSettings, IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager)
    {
        _jwtSettings = jwtSettings.Value;
        _unitOfWork = unitOfWork;
        _userManager = userManager;
    }

    public async Task<Result<string>> GenerateAccessToken(TokenUserDto tokenUserDto)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, tokenUserDto.UserId.ToString()),
            new Claim(ClaimTypes.Email, tokenUserDto.Email!),
            new Claim(ClaimTypes.Role, tokenUserDto.Role!)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(_jwtSettings.ExpiryMinutes),
            signingCredentials: creds
        );



       return Result.Success(new JwtSecurityTokenHandler().WriteToken(token));
    }

    public async Task<Result<GeneratedRefreshTokenDto>> GenerateRefreshTokenAsync(int userId)
    {
        var tokenExist = await _unitOfWork.Repository<RefreshToken>().GetEntityWithSpecAsync(new TokenExistsSpecification(userId));
        if (tokenExist != null)
        {
            tokenExist.RevokedAt = DateTime.UtcNow;
            await _unitOfWork.SaveChangesAsync();
        }

        var tokenBytes = new byte[64];

        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(tokenBytes);

        var token = Convert.ToBase64String(tokenBytes);

        var tokenHash = HashToken(token);

        var refreshToken = new RefreshToken
        {
            UserId = userId,
            TokenHash = tokenHash,
            CreatedAt = DateTime.UtcNow,
            ExpiresAt = DateTime.UtcNow.AddDays(7)
        };

        await _unitOfWork.Repository<RefreshToken>().AddAsync(refreshToken);
        await _unitOfWork.SaveChangesAsync();
        return Result.Success(new GeneratedRefreshTokenDto
        {
            Token = token,
            TokenHash = tokenHash
        });
    }

    public async Task<Result<UserToReturnDto>> RefreshAccessTokenAsync(string refreshToken)
    {
        var repo = _unitOfWork.Repository<RefreshToken>();
        var tokenHash = HashToken(refreshToken);
        var storedToken = await repo.GetEntityWithSpecAsync(new TokenByTokenHashSpecification(tokenHash));

        if (storedToken == null)
            return Result.Failure<UserToReturnDto>(Error.Unauthorized("Auth.InvalidRefreshToken", "The refresh token is invalid or does not exist."));

        var user = await _userManager.FindByIdAsync(storedToken.UserId.ToString());

        if (storedToken.RevokedAt != null)
            return Result.Failure<UserToReturnDto>(Error.Unauthorized("Auth.RefreshTokenRevoked","The refresh token has been revoked."));
        storedToken.RevokedAt = DateTime.UtcNow;
        var newRefreshTokenResult = await GenerateRefreshTokenAsync(storedToken.UserId);
        if (newRefreshTokenResult.IsFailure)
            return Result.Failure<UserToReturnDto>(newRefreshTokenResult.Error);
        var role = (await _userManager.GetRolesAsync(user)).FirstOrDefault();
        if (role is null)
        {
            return Result.Failure<UserToReturnDto>(Error.Unauthorized("Auth.UserRoleMissing","The user does not have an assigned role."));
        }
        var tokenUserDto = new TokenUserDto
        {
            UserId = storedToken.UserId,
            Email = user.Email,
            Role = role,
        };

        var accessTokenResult = await GenerateAccessToken(tokenUserDto);

        if (accessTokenResult.IsFailure)
            return Result.Failure<UserToReturnDto>(accessTokenResult.Error);

        return Result.Success(new UserToReturnDto
        {
            AccessToken = accessTokenResult.Value,
            RefreshToken = newRefreshTokenResult.Value.Token,
            AccessTokenExpiresAt = DateTime.UtcNow.AddMinutes(_jwtSettings.ExpiryMinutes),
            UserId = storedToken.UserId,
            Email = user.Email,
        });
    }

    public async Task<Result> RevokeRefreshTokenAsync(string refreshToken)
    {
        var tokenHash = HashToken(refreshToken);

        var token = await _unitOfWork.Repository<RefreshToken>()
         .GetEntityWithSpecAsync(new TokenByTokenHashSpecification(tokenHash));

        if (token != null)
        {
            token.RevokedAt = DateTime.UtcNow;
            await _unitOfWork.SaveChangesAsync();
        }

        return Result.Success();
    }


    private string HashToken(string token)
    {
        var bytes = SHA256.HashData(Encoding.UTF8.GetBytes(token));
        return Convert.ToBase64String(bytes);
    }

}
