using System.Collections.Generic;
using System.Security.Claims;
using Readora.Application.Common;
using Readora.Application.DTOs.Auth;
using Readora.Domain.Entities;

namespace Readora.Application.Interfaces.Services;

public interface ITokenService
{
	Task<Result<string>> GenerateAccessToken(TokenUserDto tokenUser);

	Task<Result<GeneratedRefreshTokenDto>> GenerateRefreshTokenAsync(int userId);
    Task<Result<UserToReturnDto>> RefreshAccessTokenAsync(string refreshToken);

    Task<Result> RevokeRefreshTokenAsync(string refreshToken);


}
