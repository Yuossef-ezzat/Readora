using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Readora.Application.Common;
using Readora.Application.DTOs.Books;
using Readora.Application.Interfaces.Persistence;
using Readora.Domain.Entities;

namespace Readora.Application.Features.Books.Queries;

public class GetBooksQueryHandler : IRequestHandler<GetBooksQuery, Result<PagedResult<BookDto>>>
{
	private readonly IUnitOfWork _unitOfWork;

	public GetBooksQueryHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<Result<PagedResult<BookDto>>> Handle(GetBooksQuery request, CancellationToken cancellationToken)
	{
		BooksSpecification spec = new BooksSpecification(request.SearchTerm, request.Page, request.PageSize);
		BooksSpecification countSpec = new BooksSpecification(request.SearchTerm, null, null);
		IReadOnlyList<Book> books = await _unitOfWork.Repository<Book>().ListAsync(spec, cancellationToken);
		int totalCount = await _unitOfWork.Repository<Book>().CountAsync(countSpec, cancellationToken);
		List<BookDto> dtos = books.Select((Book b) => new BookDto
		{
			Id = b.Id,
			Title = b.Title,
			ISBN = b.ISBN,
			Description = b.Description,
			PublishedDate = b.PublishedDate,
			CoverImageUrl = b.CoverImageUrl,
			FileKey = b.FileKey,
			AuthorId = b.AuthorId,
			CategoryId = b.CategoryId
		}).ToList();
		PagedResult<BookDto> pagedResult = new PagedResult<BookDto>(dtos, totalCount, request.Page, request.PageSize);
		return Result.Success(pagedResult);
	}
}
