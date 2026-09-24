using MediatR;
using Readora.Application.Common;
using Readora.Application.DTOs.Borrowings;

namespace Readora.Application.Features.Borrowings.Queries;

public record GetBorrowingByIdQuery(int Id) : IRequest<Result<BorrowingDto>>, IBaseRequest;
