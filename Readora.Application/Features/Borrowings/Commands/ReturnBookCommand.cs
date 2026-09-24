using MediatR;
using Readora.Application.Common;

namespace Readora.Application.Features.Borrowings.Commands;

public record ReturnBookCommand(int BorrowingId) : IRequest<Result>, IBaseRequest;
