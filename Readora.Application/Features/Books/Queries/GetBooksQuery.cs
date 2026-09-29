using System;
using MediatR;
using Readora.Application.Common;
using Readora.Application.Common.Caching;
using Readora.Application.DTOs.Books;

namespace Readora.Application.Features.Books.Queries;

public record GetBooksQuery(string? SearchTerm, int Page = 1, int PageSize = 10) : IRequest<Result<PagedResult<BookDto>>>, IBaseRequest, ICacheableQuery // 160 ms to 15 ms with caching and pagination
{
	public string CacheKey => $"Books_{SearchTerm}_{Page}_{PageSize}";

	public TimeSpan? Expiration => TimeSpan.FromMinutes(10L);
}
