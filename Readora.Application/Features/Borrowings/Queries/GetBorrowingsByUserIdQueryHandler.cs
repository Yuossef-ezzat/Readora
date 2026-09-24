using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Readora.Application.Common;
using Readora.Application.DTOs.Borrowings;
using Readora.Application.Interfaces.Persistence;
using Readora.Domain.Entities;

namespace Readora.Application.Features.Borrowings.Queries;

public class GetBorrowingsByUserIdQueryHandler : IRequestHandler<GetBorrowingsByUserIdQuery, Result<List<BorrowingDto>>>
{
	private readonly IUnitOfWork _unitOfWork;

	public GetBorrowingsByUserIdQueryHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<Result<List<BorrowingDto>>> Handle(GetBorrowingsByUserIdQuery request, CancellationToken cancellationToken)
	{
		BorrowingsByUserIdSpecification spec = new BorrowingsByUserIdSpecification(request.UserId);
		List<BorrowingDto> dtos = (await _unitOfWork.Repository<Borrowing>().ListAsync(spec, cancellationToken)).Select((Borrowing b) => new BorrowingDto
		{
			Id = b.Id,
			UserId = b.UserId,
			BookCopyId = b.BookCopyId,
			BorrowedAt = b.BorrowedAt,
			DueDate = b.DueDate,
			ReturnedAt = b.ReturnedAt,
			Status = b.Status
		}).ToList();
		return Result.Success(dtos);
	}
}
