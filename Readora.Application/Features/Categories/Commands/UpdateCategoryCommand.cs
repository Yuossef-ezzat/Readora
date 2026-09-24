using MediatR;
using Readora.Application.Common;

namespace Readora.Application.Features.Categories.Commands;

public record UpdateCategoryCommand(int Id, string Name, string? Description) : IRequest<Result>, IBaseRequest;
