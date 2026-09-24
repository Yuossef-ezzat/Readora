using MediatR;
using Readora.Application.Common;
using Readora.Application.Common.Caching;

namespace Readora.Application.Features.Books.Commands;

public record DeleteBookCommand(int Id) : IRequest<Result>, IBaseRequest, ICacheInvalidatorCommand
{
	public string[] CacheKeys => new string[1] { $"Book_{Id}" };
}
