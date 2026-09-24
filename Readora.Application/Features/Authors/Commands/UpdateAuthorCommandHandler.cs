using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Readora.Application.Common;
using Readora.Application.Interfaces.Persistence;
using Readora.Domain.Entities;

namespace Readora.Application.Features.Authors.Commands;

public class UpdateAuthorCommandHandler : IRequestHandler<UpdateAuthorCommand, Result>
{
	private readonly IUnitOfWork _unitOfWork;

	public UpdateAuthorCommandHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<Result> Handle(UpdateAuthorCommand request, CancellationToken cancellationToken)
	{
		Author author = await _unitOfWork.Repository<Author>().GetByIdAsync(request.Id, cancellationToken);
		if (author == null)
		{
			return Result.Failure(Error.NotFound("Authors.NotFound", "Author not found."));
		}
		author.Name = request.Name;
		author.Bio = request.Bio;
		_unitOfWork.Repository<Author>().Update(author);
		await _unitOfWork.SaveChangesAsync(cancellationToken);
		return Result.Success();
	}
}
