using MediatR;
using Readora.Application.Common;

namespace Readora.Application.Features.Categories.Commands;

public record DeleteCategoryCommand(int Id) : IRequest<Result>, IBaseRequest;
