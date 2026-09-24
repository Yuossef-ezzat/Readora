using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Readora.Infrastructure.Authentication
{
    public class EmailSettings
    {
        public string SenderName { get; set; } = null!;
        public string SenderEmail { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string SmtpHost { get; set; } = null!;
        public int SmtpPort { get; set; }
    }
}
