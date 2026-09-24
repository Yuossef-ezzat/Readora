using System;
using MediatR;
using Readora.Application.Common;
using Readora.Application.Common.Caching;

namespace Readora.Application.Features.Books.Commands;

public class UpdateBookCommand : IRequest<Result>, IBaseRequest, ICacheInvalidatorCommand
{
	public int Id { get; set; }

	public string Title { get; set; } = string.Empty;

	public string ISBN { get; set; } = string.Empty;

	public string? Description { get; set; }

	public DateTime? PublishedDate { get; set; }

	public string? CoverImageUrl { get; set; }

	public string? FileKey { get; set; }

	public int AuthorId { get; set; }

	public int CategoryId { get; set; }

	public string[] CacheKeys => new string[1] { $"Book_{Id}" };
}
