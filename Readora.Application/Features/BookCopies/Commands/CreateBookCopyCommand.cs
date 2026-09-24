using MediatR;
using Readora.Application.Common;
using Readora.Domain.Enums;

namespace Readora.Application.Features.BookCopies.Commands;

public record CreateBookCopyCommand(int BookId, BookCopyStatus Status = BookCopyStatus.Available) : IRequest<Result<int>>, IBaseRequest;
