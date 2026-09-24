using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Readora.Application.Common;
using Readora.Application.Interfaces.Persistence;
using Readora.Domain.Entities;

namespace Readora.Application.Features.Books.Commands;

public class DeleteBookCommandHandler : IRequestHandler<DeleteBookCommand, Result>
{
	private readonly IUnitOfWork _unitOfWork;

	public DeleteBookCommandHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<Result> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
	{
		Book book = await _unitOfWork.Repository<Book>().GetByIdAsync(request.Id, cancellationToken);
		if (book == null)
		{
			return Result.Failure(Error.NotFound("Books.NotFound", $"Book with Id {request.Id} not found."));
		}
		_unitOfWork.Repository<Book>().Delete(book);
		await _unitOfWork.SaveChangesAsync(cancellationToken);
		return Result.Success();
	}
}
