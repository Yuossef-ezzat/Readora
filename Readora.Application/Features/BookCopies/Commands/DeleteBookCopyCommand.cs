using MediatR;
using Readora.Application.Common;

namespace Readora.Application.Features.BookCopies.Commands;

public record DeleteBookCopyCommand(int Id) : IRequest<Result>, IBaseRequest;
