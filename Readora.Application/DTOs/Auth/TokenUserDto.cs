
namespace Readora.Application.DTOs.Auth
{
    public class TokenUserDto
    {
        public string Email { get; set; } = null!; 
        public int UserId { get; set; } 
        public string Role { get; set; } = null!;

    }
    public class GeneratedRefreshTokenDto
    {
        public string Token { get; set; } = string.Empty;
        public string TokenHash { get; set; } = string.Empty;
    }
}
