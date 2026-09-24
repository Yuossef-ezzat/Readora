using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Readora.Application.Common;
using Readora.Application.Interfaces.Persistence;
using Readora.Domain.Entities;

namespace Readora.Application.Features.BookCopies.Commands;

public class DeleteBookCopyCommandHandler : IRequestHandler<DeleteBookCopyCommand, Result>
{
	private readonly IUnitOfWork _unitOfWork;

	public DeleteBookCopyCommandHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<Result> Handle(DeleteBookCopyCommand request, CancellationToken cancellationToken)
	{
		BookCopy bookCopy = await _unitOfWork.Repository<BookCopy>().GetByIdAsync(request.Id, cancellationToken);
		if (bookCopy == null)
		{
			return Result.Failure(Error.NotFound("BookCopies.NotFound", "Book copy not found."));
		}
		_unitOfWork.Repository<BookCopy>().Delete(bookCopy);
		await _unitOfWork.SaveChangesAsync(cancellationToken);
		return Result.Success();
	}
}
