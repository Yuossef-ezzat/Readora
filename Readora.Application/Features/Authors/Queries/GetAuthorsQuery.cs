using MediatR;
using Readora.Application.Common;
using Readora.Application.DTOs.Authors;

namespace Readora.Application.Features.Authors.Queries;

public record GetAuthorsQuery(string? SearchTerm, int Page = 1, int PageSize = 10) : IRequest<Result<PagedResult<AuthorDto>>>, IBaseRequest;
