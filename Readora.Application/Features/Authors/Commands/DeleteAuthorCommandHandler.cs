using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Readora.Application.Common;
using Readora.Application.Interfaces.Persistence;
using Readora.Domain.Entities;

namespace Readora.Application.Features.Authors.Commands;

public class DeleteAuthorCommandHandler : IRequestHandler<DeleteAuthorCommand, Result>
{
	private readonly IUnitOfWork _unitOfWork;

	public DeleteAuthorCommandHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<Result> Handle(DeleteAuthorCommand request, CancellationToken cancellationToken)
	{
		Author author = await _unitOfWork.Repository<Author>().GetByIdAsync(request.Id, cancellationToken);
		if (author == null)
		{
			return Result.Failure(Error.NotFound("Authors.NotFound", "Author not found."));
		}
		_unitOfWork.Repository<Author>().Delete(author);
		await _unitOfWork.SaveChangesAsync(cancellationToken);
		return Result.Success();
	}
}
