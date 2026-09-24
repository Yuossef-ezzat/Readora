using MediatR;
using Readora.Application.Common;

namespace Readora.Application.Features.Authors.Commands;

public record DeleteAuthorCommand(int Id) : IRequest<Result>, IBaseRequest;
