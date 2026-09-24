using System.IO;
using System.Threading.Tasks;

namespace Readora.Application.Interfaces.Services;

public interface IStorageService
{
	Task<string> UploadFileAsync(Stream fileStream, string fileName, string contentType, string? prefix = null);

	Task DeleteFileAsync(string fileKey);

	string GetFileUrl(string fileKey);
}
