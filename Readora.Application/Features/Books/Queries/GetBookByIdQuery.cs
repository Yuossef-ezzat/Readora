using System;
using MediatR;
using Readora.Application.Common;
using Readora.Application.Common.Caching;
using Readora.Application.DTOs.Books;

namespace Readora.Application.Features.Books.Queries;

public record GetBookByIdQuery(int Id) : IRequest<Result<BookDto>>, IBaseRequest, ICacheableQuery
{
	public string CacheKey => $"Book_{Id}";

	public TimeSpan? Expiration => TimeSpan.FromHours(1);
}
