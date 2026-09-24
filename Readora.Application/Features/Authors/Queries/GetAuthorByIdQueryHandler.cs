using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Readora.Application.Common;
using Readora.Application.DTOs.Authors;
using Readora.Application.Interfaces.Persistence;
using Readora.Domain.Entities;

namespace Readora.Application.Features.Authors.Queries;

public class GetAuthorByIdQueryHandler : IRequestHandler<GetAuthorByIdQuery, Result<AuthorDto>>
{
	private readonly IUnitOfWork _unitOfWork;

	public GetAuthorByIdQueryHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<Result<AuthorDto>> Handle(GetAuthorByIdQuery request, CancellationToken cancellationToken)
	{
		Author author = await _unitOfWork.Repository<Author>().GetByIdAsync(request.Id, cancellationToken);
		if (author == null)
		{
			return Result.Failure<AuthorDto>(Error.NotFound("Authors.NotFound", "Author not found."));
		}
		return Result.Success(new AuthorDto
		{
			Id = author.Id,
			Name = author.Name,
			Bio = author.Bio
		});
	}
}
