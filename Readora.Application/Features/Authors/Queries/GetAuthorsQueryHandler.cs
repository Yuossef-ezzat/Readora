using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Readora.Application.Common;
using Readora.Application.DTOs.Authors;
using Readora.Application.Interfaces.Persistence;
using Readora.Domain.Entities;

namespace Readora.Application.Features.Authors.Queries;

public class GetAuthorsQueryHandler : IRequestHandler<GetAuthorsQuery, Result<PagedResult<AuthorDto>>>
{
	private readonly IUnitOfWork _unitOfWork;

	public GetAuthorsQueryHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<Result<PagedResult<AuthorDto>>> Handle(GetAuthorsQuery request, CancellationToken cancellationToken)
	{
		AuthorsSpecification spec = new AuthorsSpecification(request.SearchTerm, request.Page, request.PageSize);
		AuthorsSpecification countSpec = new AuthorsSpecification(request.SearchTerm, null, null);
		IReadOnlyList<Author> authors = await _unitOfWork.Repository<Author>().ListAsync(spec, cancellationToken);
		int total = await _unitOfWork.Repository<Author>().CountAsync(countSpec, cancellationToken);
		List<AuthorDto> dtos = authors.Select((Author a) => new AuthorDto
		{
			Id = a.Id,
			Name = a.Name,
			Bio = a.Bio
		}).ToList();
		return Result.Success(new PagedResult<AuthorDto>(dtos, total, request.Page, request.PageSize));
	}
}
