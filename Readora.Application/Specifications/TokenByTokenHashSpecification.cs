using Readora.Domain.Entities;

namespace Readora.Application.Specifications;

public class TokenByTokenHashSpecification : BaseSpecification<RefreshToken>
{
    //public TokenByTokenHashSpecification(string tokenHash)
    //    : base(t => t.TokenHash == tokenHash)
    //{
    //}
    public TokenByTokenHashSpecification(string tokenHash)
        : base(t => t.TokenHash == tokenHash && t.ExpiresAt > DateTime.UtcNow)
    {
    }
}
