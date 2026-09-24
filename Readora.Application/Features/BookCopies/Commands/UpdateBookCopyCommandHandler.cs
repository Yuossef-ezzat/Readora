using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Readora.Application.Common;
using Readora.Application.Interfaces.Persistence;
using Readora.Domain.Entities;

namespace Readora.Application.Features.BookCopies.Commands;

public class UpdateBookCopyCommandHandler : IRequestHandler<UpdateBookCopyCommand, Result>
{
	private readonly IUnitOfWork _unitOfWork;

	public UpdateBookCopyCommandHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<Result> Handle(UpdateBookCopyCommand request, CancellationToken cancellationToken)
	{
		BookCopy bookCopy = await _unitOfWork.Repository<BookCopy>().GetByIdAsync(request.Id, cancellationToken);
		if (bookCopy == null)
		{
			return Result.Failure(Error.NotFound("BookCopies.NotFound", "Book copy not found."));
		}
		bookCopy.Status = request.Status;
		_unitOfWork.Repository<BookCopy>().Update(bookCopy);
		await _unitOfWork.SaveChangesAsync(cancellationToken);
		return Result.Success();
	}
}
