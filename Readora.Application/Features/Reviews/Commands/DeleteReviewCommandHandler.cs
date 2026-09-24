using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Readora.Application.Common;
using Readora.Application.Interfaces.Persistence;
using Readora.Domain.Entities;

namespace Readora.Application.Features.Reviews.Commands;

public class DeleteReviewCommandHandler : IRequestHandler<DeleteReviewCommand, Result>
{
	private readonly IUnitOfWork _unitOfWork;

	public DeleteReviewCommandHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<Result> Handle(DeleteReviewCommand request, CancellationToken cancellationToken)
	{
		Review review = await _unitOfWork.Repository<Review>().GetByIdAsync(request.Id, cancellationToken);
		if (review == null)
		{
			return Result.Failure(Error.NotFound("Reviews.NotFound", "Review not found."));
		}
		if (review.UserId != request.UserId && !request.IsAdmin)
		{
			return Result.Failure(Error.Unauthorized("Reviews.Unauthorized", "You can only delete your own review."));
		}
		_unitOfWork.Repository<Review>().Delete(review);
		await _unitOfWork.SaveChangesAsync(cancellationToken);
		return Result.Success();
	}
}
