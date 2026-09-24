using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Readora.Application.Common;
using Readora.Application.Interfaces.Persistence;
using Readora.Domain.Entities;

namespace Readora.Application.Features.BookCopies.Commands;

public class CreateBookCopyCommandHandler : IRequestHandler<CreateBookCopyCommand, Result<int>>
{
	private readonly IUnitOfWork _unitOfWork;

	public CreateBookCopyCommandHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<Result<int>> Handle(CreateBookCopyCommand request, CancellationToken cancellationToken)
	{
		if (await _unitOfWork.Repository<Book>().GetByIdAsync(request.BookId, cancellationToken) == null)
		{
			return Result.Failure<int>(Error.NotFound("Books.NotFound", "Book not found."));
		}
		BookCopy bookCopy = new BookCopy
		{
			BookId = request.BookId,
			Status = request.Status
		};
		await _unitOfWork.Repository<BookCopy>().AddAsync(bookCopy, cancellationToken);
		await _unitOfWork.SaveChangesAsync(cancellationToken);
		return Result.Success(bookCopy.Id);
	}
}
