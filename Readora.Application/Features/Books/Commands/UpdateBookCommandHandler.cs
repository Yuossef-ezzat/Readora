using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Readora.Application.Common;
using Readora.Application.Interfaces.Persistence;
using Readora.Domain.Entities;

namespace Readora.Application.Features.Books.Commands;

public class UpdateBookCommandHandler : IRequestHandler<UpdateBookCommand, Result>
{
	private readonly IUnitOfWork _unitOfWork;

	public UpdateBookCommandHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<Result> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
	{
		Book book = await _unitOfWork.Repository<Book>().GetByIdAsync(request.Id, cancellationToken);
		if (book == null)
		{
			return Result.Failure(Error.NotFound("Books.NotFound", $"Book with Id {request.Id} not found."));
		}
		book.Title = request.Title;
		book.ISBN = request.ISBN;
		book.Description = request.Description;
		book.PublishedDate = request.PublishedDate;
		book.CoverImageUrl = request.CoverImageUrl;
		book.FileKey = request.FileKey;
		book.AuthorId = request.AuthorId;
		book.CategoryId = request.CategoryId;
		_unitOfWork.Repository<Book>().Update(book);
		await _unitOfWork.SaveChangesAsync(cancellationToken);
		return Result.Success();
	}
}
