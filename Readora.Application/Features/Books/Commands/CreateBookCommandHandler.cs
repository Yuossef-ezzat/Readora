using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Readora.Application.Common;
using Readora.Application.Interfaces.Persistence;
using Readora.Domain.Entities;

namespace Readora.Application.Features.Books.Commands;

public class CreateBookCommandHandler : IRequestHandler<CreateBookCommand, Result<int>>
{
	private readonly IUnitOfWork _unitOfWork;

	public CreateBookCommandHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<Result<int>> Handle(CreateBookCommand request, CancellationToken cancellationToken)
	{
		if (await _unitOfWork.Repository<Author>().GetByIdAsync(request.AuthorId, cancellationToken) == null)
		{
			return Result.Failure<int>(Error.NotFound("Authors.NotFound", "Author not found."));
		}

		if (await _unitOfWork.Repository<Category>().GetByIdAsync(request.CategoryId, cancellationToken) == null)
		{
			return Result.Failure<int>(Error.NotFound("Categories.NotFound", "Category not found."));
		}

		Book book = new Book
		{
			Title = request.Title,
			ISBN = request.ISBN,
			Description = request.Description,
			PublishedDate = request.PublishedDate,
			CoverImageUrl = request.CoverImageUrl,
			FileKey = request.FileKey,
			AuthorId = request.AuthorId,
			CategoryId = request.CategoryId
		};
		await _unitOfWork.Repository<Book>().AddAsync(book, cancellationToken);
		await _unitOfWork.SaveChangesAsync(cancellationToken);
		return Result.Success(book.Id);
	}
}
