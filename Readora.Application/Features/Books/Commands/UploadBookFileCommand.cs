using System.IO;
using MediatR;
using Readora.Application.Common;

namespace Readora.Application.Features.Books.Commands;

public record UploadBookFileCommand(int BookId, Stream FileStream, string FileName, string ContentType, bool IsCoverImage) : IRequest<Result<string>>, IBaseRequest;
