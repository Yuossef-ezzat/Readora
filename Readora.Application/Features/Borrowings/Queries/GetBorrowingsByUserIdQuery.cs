using System.Collections.Generic;
using MediatR;
using Readora.Application.Common;
using Readora.Application.DTOs.Borrowings;

namespace Readora.Application.Features.Borrowings.Queries;

public record GetBorrowingsByUserIdQuery(int UserId) : IRequest<Result<List<BorrowingDto>>>, IBaseRequest;
