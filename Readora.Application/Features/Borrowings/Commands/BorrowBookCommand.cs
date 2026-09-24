using MediatR;
using Readora.Application.Common;

namespace Readora.Application.Features.Borrowings.Commands;

public record BorrowBookCommand(int UserId, int BookId) : IRequest<Result<int>>, IBaseRequest;
