using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Readora.Application.Common;
using Readora.Application.DTOs.Books;
using Readora.Application.Interfaces.Persistence;
using Readora.Domain.Entities;

namespace Readora.Application.Features.Books.Queries;

public class GetBookByIdQueryHandler : IRequestHandler<GetBookByIdQuery, Result<BookDto>>
{
	private readonly IUnitOfWork _unitOfWork;

	public GetBookByIdQueryHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<Result<BookDto>> Handle(GetBookByIdQuery request, CancellationToken cancellationToken)
	{
		Book book = await _unitOfWork.Repository<Book>().GetByIdAsync(request.Id, cancellationToken);
		if (book == null)
		{
			return Result.Failure<BookDto>(Error.NotFound("Books.NotFound", $"Book with Id {request.Id} not found."));
		}
		BookDto dto = new BookDto
		{
			Id = book.Id,
			Title = book.Title,
			ISBN = book.ISBN,
			Description = book.Description,
			PublishedDate = book.PublishedDate,
			CoverImageUrl = book.CoverImageUrl,
			FileKey = book.FileKey,
			AuthorId = book.AuthorId,
			CategoryId = book.CategoryId
		};
		return Result.Success(dto);
	}
}
