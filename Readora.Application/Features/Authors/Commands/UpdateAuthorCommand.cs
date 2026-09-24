using MediatR;
using Readora.Application.Common;

namespace Readora.Application.Features.Authors.Commands;

public record UpdateAuthorCommand(int Id, string Name, string? Bio) : IRequest<Result>, IBaseRequest;
