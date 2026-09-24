using System;
using System.IO;
using System.Threading.Tasks;
using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.Extensions.Options;
using Readora.Application.Interfaces.Services;
using Readora.Infrastructure.Authentication;

namespace Readora.Infrastructure.Services;

public class SupabaseStorageService : IStorageService
{
    private readonly IAmazonS3 _s3Client;
    private readonly SupabaseS3Settings _settings;

    public SupabaseStorageService(IAmazonS3 s3Client, IOptions<SupabaseS3Settings> settings)
    {
        _s3Client = s3Client;
        _settings = settings.Value;
    }

    public async Task<string> UploadFileAsync(Stream fileStream, string fileName, string contentType, string? prefix = null)
    {
        var fileExtension = Path.GetExtension(fileName);
        var generatedFileName = $"{Guid.NewGuid()}{fileExtension}";
        var fileKey = string.IsNullOrEmpty(prefix) ? generatedFileName : $"{prefix}/{generatedFileName}";

        var putRequest = new PutObjectRequest
        {
            BucketName = _settings.BucketName,
            Key = fileKey,
            InputStream = fileStream,
            ContentType = contentType,
            DisablePayloadSigning = true
        };

        await _s3Client.PutObjectAsync(putRequest);
        return fileKey;
    }

    public async Task DeleteFileAsync(string fileKey)
    {
        var deleteRequest = new DeleteObjectRequest
        {
            BucketName = _settings.BucketName,
            Key = fileKey
        };

        await _s3Client.DeleteObjectAsync(deleteRequest);
    }

    public string GetFileUrl(string fileKey)
    {
        if (string.IsNullOrEmpty(_settings.Endpoint))
        {
            return fileKey;
        }

        var baseUrl = _settings.Endpoint;
        var s3Suffix = "/storage/v1/s3";

        if (baseUrl.EndsWith(s3Suffix, StringComparison.OrdinalIgnoreCase))
        {
            baseUrl = baseUrl.Substring(0, baseUrl.Length - s3Suffix.Length);
        }

        return $"{baseUrl.TrimEnd('/')}/storage/v1/object/public/{_settings.BucketName}/{fileKey}";
    }
}
