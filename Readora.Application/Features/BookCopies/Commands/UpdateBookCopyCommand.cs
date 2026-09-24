using MediatR;
using Readora.Application.Common;
using Readora.Domain.Enums;

namespace Readora.Application.Features.BookCopies.Commands;

public record UpdateBookCopyCommand(int Id, BookCopyStatus Status) : IRequest<Result>, IBaseRequest;
