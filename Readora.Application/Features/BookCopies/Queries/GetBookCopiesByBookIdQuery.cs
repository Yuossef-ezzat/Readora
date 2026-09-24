using System.Collections.Generic;
using MediatR;
using Readora.Application.Common;
using Readora.Application.DTOs.BookCopies;

namespace Readora.Application.Features.BookCopies.Queries;

public record GetBookCopiesByBookIdQuery(int BookId) : IRequest<Result<List<BookCopyDto>>>, IBaseRequest;
