using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Readora.Application.Common;
using Readora.Application.Common.Exceptions;
using Readora.Application.Interfaces.Persistence;
using Readora.Domain.Entities;
using Readora.Domain.Enums;

namespace Readora.Application.Features.Borrowings.Commands;

public class ReturnBookCommandHandler : IRequestHandler<ReturnBookCommand, Result>
{
	private readonly IUnitOfWork _unitOfWork;

	public ReturnBookCommandHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<Result> Handle(ReturnBookCommand request, CancellationToken cancellationToken)
	{
		Borrowing borrowing = await _unitOfWork.Repository<Borrowing>().GetByIdAsync(request.BorrowingId, cancellationToken);
		if (borrowing == null)
		{
			return Result.Failure(Error.NotFound("Borrowings.NotFound", "Borrowing record not found."));
		}
		if (borrowing.Status != BorrowingStatus.Active)
		{
			return Result.Failure(Error.Validation("Borrowings.NotActive", "This borrowing is already completed or cancelled."));
		}
		BookCopy copy = await _unitOfWork.Repository<BookCopy>().GetByIdAsync(borrowing.BookCopyId, cancellationToken);
		if (copy == null)
		{
			return Result.Failure(Error.NotFound("BookCopies.NotFound", "Book copy not found."));
		}
		DateTime now = DateTime.UtcNow;
		borrowing.ReturnedAt = now;
		borrowing.Status = BorrowingStatus.Returned;
		_unitOfWork.Repository<Borrowing>().Update(borrowing);
		copy.Status = BookCopyStatus.Available;
		_unitOfWork.Repository<BookCopy>().Update(copy);
		try
		{
			await _unitOfWork.SaveChangesAsync(cancellationToken);
		}
		catch (ConcurrencyException)
		{
			return Result.Failure(Error.Conflict("Borrowings.ConcurrencyConflict", "Concurrency conflict while returning book."));
		}
		return Result.Success();
	}
}
