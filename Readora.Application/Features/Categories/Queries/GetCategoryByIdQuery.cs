using MediatR;
using Readora.Application.Common;
using Readora.Application.DTOs.Categories;

namespace Readora.Application.Features.Categories.Queries;

public record GetCategoryByIdQuery(int Id) : IRequest<Result<CategoryDto>>, IBaseRequest;
