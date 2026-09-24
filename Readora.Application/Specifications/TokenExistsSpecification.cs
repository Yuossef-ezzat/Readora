using System;
using Readora.Domain.Entities;

namespace Readora.Application.Specifications;

public class TokenExistsSpecification : BaseSpecification<RefreshToken>
{
    public TokenExistsSpecification(int userId)
        : base(t => t.UserId == userId && t.ExpiresAt > DateTime.UtcNow && t.RevokedAt == null)
    {
    }
}
