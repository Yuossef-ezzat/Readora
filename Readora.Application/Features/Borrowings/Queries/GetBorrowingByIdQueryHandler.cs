using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Readora.Application.Common;
using Readora.Application.DTOs.Borrowings;
using Readora.Application.Interfaces.Persistence;
using Readora.Domain.Entities;

namespace Readora.Application.Features.Borrowings.Queries;

public class GetBorrowingByIdQueryHandler : IRequestHandler<GetBorrowingByIdQuery, Result<BorrowingDto>>
{
	private readonly IUnitOfWork _unitOfWork;

	public GetBorrowingByIdQueryHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<Result<BorrowingDto>> Handle(GetBorrowingByIdQuery request, CancellationToken cancellationToken)
	{
		Borrowing borrowing = await _unitOfWork.Repository<Borrowing>().GetByIdAsync(request.Id, cancellationToken);
		if (borrowing == null)
		{
			return Result.Failure<BorrowingDto>(Error.NotFound("Borrowings.NotFound", "Borrowing not found."));
		}
		return Result.Success(MapToDto(borrowing));
	}

	private static BorrowingDto MapToDto(Borrowing b)
	{
		return new BorrowingDto
		{
			Id = b.Id,
			UserId = b.UserId,
			BookCopyId = b.BookCopyId,
			BorrowedAt = b.BorrowedAt,
			DueDate = b.DueDate,
			ReturnedAt = b.ReturnedAt,
			Status = b.Status
		};
	}
}
