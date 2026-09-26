using Readora.Application.Common;
using System.Threading.Tasks;

namespace Readora.Application.Interfaces.Services;

public interface IOtpService
{
	Task<Result<string>> GenerateAndStoreOtpAsync(string email);

	Task<Result> VerifyOtpAsync(string email, string otp);
}
