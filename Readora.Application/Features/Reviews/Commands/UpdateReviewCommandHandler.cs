using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Readora.Application.Common;
using Readora.Application.Interfaces.Persistence;
using Readora.Domain.Entities;

namespace Readora.Application.Features.Reviews.Commands;

public class UpdateReviewCommandHandler : IRequestHandler<UpdateReviewCommand, Result>
{
	private readonly IUnitOfWork _unitOfWork;

	public UpdateReviewCommandHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<Result> Handle(UpdateReviewCommand request, CancellationToken cancellationToken)
	{
		Review review = await _unitOfWork.Repository<Review>().GetByIdAsync(request.Id, cancellationToken);
		if (review == null)
		{
			return Result.Failure(Error.NotFound("Reviews.NotFound", "Review not found."));
		}
		if (review.UserId != request.UserId)
		{
			return Result.Failure(Error.Unauthorized("Reviews.Unauthorized", "You can only update your own review."));
		}
		if (request.Rating < 1 || request.Rating > 5)
		{
			return Result.Failure(Error.Validation("Reviews.InvalidRating", "Rating must be between 1 and 5."));
		}
		review.Rating = request.Rating;
		review.Comment = request.Comment;
		_unitOfWork.Repository<Review>().Update(review);
		await _unitOfWork.SaveChangesAsync(cancellationToken);
		return Result.Success();
	}
}
