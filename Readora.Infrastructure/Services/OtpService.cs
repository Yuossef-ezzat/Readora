using System.Security.Cryptography;
using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;
using Readora.Application.Common;
using Readora.Application.Interfaces.Services;

namespace Readora.Infrastructure.Services;

public class OtpService : IOtpService
{
    private readonly IDistributedCache _cache;

    private const int MaxAttempts = 3;
    private static readonly TimeSpan OtpLifetime = TimeSpan.FromMinutes(5);

    public OtpService(IDistributedCache cache)
    {
        _cache = cache;
    }

    public async Task<Result<string>> GenerateAndStoreOtpAsync(string email)
    {
        var otp = RandomNumberGenerator
            .GetInt32(100000, 1000000)
            .ToString();

        var otpData = new Otp
        {
            Code = otp,
            ExpiresAt = DateTime.UtcNow.Add(OtpLifetime)
        };

        var options = new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = OtpLifetime
        };

        var key = GetOtpKey(email);

        await _cache.SetStringAsync(
            key,
            JsonSerializer.Serialize(otpData),
            options);

        return Result.Success(otp);
    }

    public async Task<Result> VerifyOtpAsync(string email, string otp)
    {
        var key = GetOtpKey(email);

        var storedOtp = await _cache.GetStringAsync(key);

        if (storedOtp is null)
        {
            return Result.Failure(
                Error.Validation(
                    "Otp.NotFound",
                    "OTP is invalid or has expired."));
        }

        var otpData = JsonSerializer.Deserialize<Otp>(storedOtp);

        if (otpData is null)
        {
            return Result.Failure(
                Error.Failure(
                    "Otp.InvalidData",
                    "Unable to process the OTP."));
        }

        if (otpData.ExpiresAt <= DateTime.UtcNow)
        {
            await _cache.RemoveAsync(key);

            return Result.Failure(
                Error.Validation(
                    "Otp.Expired",
                    "OTP has expired. Please request a new OTP."));
        }

        if (otpData.Attempts >= MaxAttempts)
        {
            return Result.Failure(
                Error.Validation(
                    "Otp.MaxAttemptsExceeded",
                    "Maximum OTP attempts exceeded. Please request a new OTP."));
        }

        if (otpData.Code != otp)
        {
            otpData.Attempts++;

            var remainingTime = otpData.ExpiresAt - DateTime.UtcNow;

            if (remainingTime <= TimeSpan.Zero)
            {
                await _cache.RemoveAsync(key);

                return Result.Failure(
                    Error.Validation(
                        "Otp.Expired",
                        "OTP has expired. Please request a new OTP."));
            }

            await _cache.SetStringAsync(
                key,
                JsonSerializer.Serialize(otpData),
                new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = remainingTime
                });

            var remainingAttempts = MaxAttempts - otpData.Attempts;

            if (remainingAttempts == 0)
            {
                return Result.Failure(
                    Error.Validation(
                        "Otp.MaxAttemptsExceeded",
                        "Maximum OTP attempts exceeded. Please request a new OTP."));
            }

            return Result.Failure(
                Error.Validation(
                    "Otp.Invalid",
                    $"Invalid OTP. You have {remainingAttempts} attempt(s) remaining."));
        }

        await _cache.RemoveAsync(key);

        return Result.Success();
    }

    private static string GetOtpKey(string email)
        => $"otp:{email}";
}

public class Otp
{
    public string Code { get; set; } = string.Empty;

    public int Attempts { get; set; }

    public DateTime ExpiresAt { get; set; }
}
