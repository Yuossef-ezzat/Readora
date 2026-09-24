using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Readora.Application.Common;
using Readora.Application.DTOs.BookCopies;
using Readora.Application.Interfaces.Persistence;
using Readora.Domain.Entities;

namespace Readora.Application.Features.BookCopies.Queries;

public class GetBookCopiesByBookIdQueryHandler : IRequestHandler<GetBookCopiesByBookIdQuery, Result<List<BookCopyDto>>>
{
	private readonly IUnitOfWork _unitOfWork;

	public GetBookCopiesByBookIdQueryHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<Result<List<BookCopyDto>>> Handle(GetBookCopiesByBookIdQuery request, CancellationToken cancellationToken)
	{
		BookCopiesByBookIdSpecification spec = new BookCopiesByBookIdSpecification(request.BookId);
		List<BookCopyDto> dtos = (await _unitOfWork.Repository<BookCopy>().ListAsync(spec, cancellationToken)).Select((BookCopy bc) => new BookCopyDto
		{
			Id = bc.Id,
			BookId = bc.BookId,
			Status = bc.Status
		}).ToList();
		return Result.Success(dtos);
	}
}
