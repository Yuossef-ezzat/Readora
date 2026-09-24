using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;
using Readora.Application.Interfaces.Services;

namespace Readora.Infrastructure.Services;

public class OtpService : IOtpService
{
    private readonly IDistributedCache _cache;

    public OtpService(IDistributedCache cache)
    {
        _cache = cache;
    }

    public async Task<string> GenerateAndStoreOtpAsync(string email)
    {
        var otp = new Random().Next(100000, 999999).ToString();
        var options = new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10)
        };
        
        await _cache.SetStringAsync($"otp:{email}", otp, options);
        return otp;
    }

    public async Task<bool> VerifyOtpAsync(string email, string otp)
    {
        var storedOtp = await _cache.GetStringAsync($"otp:{email}");
        if (storedOtp == null || storedOtp != otp)
        {
            return false;
        }

        return true;
    }
}
