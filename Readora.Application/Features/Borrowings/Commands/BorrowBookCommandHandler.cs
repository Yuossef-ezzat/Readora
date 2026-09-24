using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Readora.Application.Common;
using Readora.Application.Common.Exceptions;
using Readora.Application.Interfaces.Persistence;
using Readora.Domain.Entities;
using Readora.Domain.Enums;

namespace Readora.Application.Features.Borrowings.Commands;

public class BorrowBookCommandHandler : IRequestHandler<BorrowBookCommand, Result<int>>
{
	private readonly IUnitOfWork _unitOfWork;

	public BorrowBookCommandHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<Result<int>> Handle(BorrowBookCommand request, CancellationToken cancellationToken)
	{
		BookCopy copy = (await _unitOfWork.Repository<BookCopy>().ListAsync(new AvailableBookCopySpecification(request.BookId), cancellationToken)).FirstOrDefault();
		if (copy == null)
		{
			return Result.Failure<int>(Error.Validation("Borrowings.NoCopyAvailable", "No available copies for this book."));
		}
		copy.Status = BookCopyStatus.Borrowed;
		_unitOfWork.Repository<BookCopy>().Update(copy);
		Borrowing borrowing = new Borrowing
		{
			UserId = request.UserId,
			BookCopyId = copy.Id,
			BorrowedAt = DateTime.UtcNow,
			DueDate = DateTime.UtcNow.AddDays(14.0),
			Status = BorrowingStatus.Active
		};
		await _unitOfWork.Repository<Borrowing>().AddAsync(borrowing, cancellationToken);
		try
		{
			await _unitOfWork.SaveChangesAsync(cancellationToken);
		}
		catch (ConcurrencyException)
		{
			return Result.Failure<int>(Error.Conflict("Borrowings.ConcurrencyConflict", "The book copy was borrowed by someone else just now. Please try again."));
		}
		return Result.Success(borrowing.Id);
	}
}
