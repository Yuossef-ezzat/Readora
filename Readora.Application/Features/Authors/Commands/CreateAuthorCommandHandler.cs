using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Readora.Application.Common;
using Readora.Application.Interfaces.Persistence;
using Readora.Domain.Entities;

namespace Readora.Application.Features.Authors.Commands;

public class CreateAuthorCommandHandler : IRequestHandler<CreateAuthorCommand, Result<int>>
{
	private readonly IUnitOfWork _unitOfWork;

	public CreateAuthorCommandHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<Result<int>> Handle(CreateAuthorCommand request, CancellationToken cancellationToken)
	{
		Author author = new Author
		{
			Name = request.Name,
			Bio = request.Bio
		};
		await _unitOfWork.Repository<Author>().AddAsync(author, cancellationToken);
		await _unitOfWork.SaveChangesAsync(cancellationToken);
		return Result.Success(author.Id);
	}
}
