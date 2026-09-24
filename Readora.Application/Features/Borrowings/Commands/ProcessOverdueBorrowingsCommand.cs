using MediatR;
using Readora.Application.Common;

namespace Readora.Application.Features.Borrowings.Commands;

public record ProcessOverdueBorrowingsCommand : IRequest<Result>, IBaseRequest;
