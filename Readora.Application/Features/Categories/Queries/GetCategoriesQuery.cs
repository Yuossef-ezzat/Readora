using MediatR;
using Readora.Application.Common;
using Readora.Application.DTOs.Categories;

namespace Readora.Application.Features.Categories.Queries;

public record GetCategoriesQuery(string? SearchTerm, int Page = 1, int PageSize = 10) : IRequest<Result<PagedResult<CategoryDto>>>, IBaseRequest;
