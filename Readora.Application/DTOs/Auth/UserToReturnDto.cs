using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Readora.Application.DTOs.Auth
{
    public class UserToReturnDto
    {
        public string FullName { get; set; } = null!;
        public int UserId { get; set; }
        public string Email { get; set; } = null!;
        public DateTime AccessTokenExpiresAt { get; set; }
        public string AccessToken { get; set; } = null!;
        public string RefreshToken { get; set; } = null!;
        public DateTime RefreshTokenExpiresAt { get; set; }

    }
}
