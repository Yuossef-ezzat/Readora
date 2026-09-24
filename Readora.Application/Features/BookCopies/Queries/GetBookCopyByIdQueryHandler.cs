using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Readora.Application.Common;
using Readora.Application.DTOs.BookCopies;
using Readora.Application.Interfaces.Persistence;
using Readora.Domain.Entities;

namespace Readora.Application.Features.BookCopies.Queries;

public class GetBookCopyByIdQueryHandler : IRequestHandler<GetBookCopyByIdQuery, Result<BookCopyDto>>
{
	private readonly IUnitOfWork _unitOfWork;

	public GetBookCopyByIdQueryHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<Result<BookCopyDto>> Handle(GetBookCopyByIdQuery request, CancellationToken cancellationToken)
	{
		BookCopy bookCopy = await _unitOfWork.Repository<BookCopy>().GetByIdAsync(request.Id, cancellationToken);
		if (bookCopy == null)
		{
			return Result.Failure<BookCopyDto>(Error.NotFound("BookCopies.NotFound", "Book copy not found."));
		}
		return Result.Success(new BookCopyDto
		{
			Id = bookCopy.Id,
			BookId = bookCopy.BookId,
			Status = bookCopy.Status
		});
	}
}
