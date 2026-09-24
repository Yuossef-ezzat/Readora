using MediatR;
using Readora.Application.Common;
using Readora.Application.DTOs.BookCopies;

namespace Readora.Application.Features.BookCopies.Queries;

public record GetBookCopyByIdQuery(int Id) : IRequest<Result<BookCopyDto>>, IBaseRequest;
