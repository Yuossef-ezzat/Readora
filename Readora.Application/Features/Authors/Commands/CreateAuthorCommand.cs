using MediatR;
using Readora.Application.Common;

namespace Readora.Application.Features.Authors.Commands;

public record CreateAuthorCommand(string Name, string? Bio) : IRequest<Result<int>>, IBaseRequest;
