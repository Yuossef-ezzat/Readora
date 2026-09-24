using System.Threading.Tasks;

namespace Readora.Application.Interfaces.Services;

public interface IOtpService
{
	Task<string> GenerateAndStoreOtpAsync(string email);

	Task<bool> VerifyOtpAsync(string email, string otp);
}
