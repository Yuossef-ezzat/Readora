using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Readora.Application.Common;
using Readora.Application.Interfaces.Persistence;
using Readora.Domain.Entities;

namespace Readora.Application.Features.Reviews.Commands;

public class AddReviewCommandHandler : IRequestHandler<AddReviewCommand, Result<int>>
{
	private readonly IUnitOfWork _unitOfWork;

	public AddReviewCommandHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<Result<int>> Handle(AddReviewCommand request, CancellationToken cancellationToken)
	{
		if (request.Rating < 1 || request.Rating > 5)
		{
			return Result.Failure<int>(Error.Validation("Reviews.InvalidRating", "Rating must be between 1 and 5."));
		}
		Review review = new Review
		{
			BookId = request.BookId,
			UserId = request.UserId,
			Rating = request.Rating,
			Comment = request.Comment,
			CreatedAt = DateTime.UtcNow
		};
		await _unitOfWork.Repository<Review>().AddAsync(review, cancellationToken);
		await _unitOfWork.SaveChangesAsync(cancellationToken);
		return Result.Success(review.Id);
	}
}
