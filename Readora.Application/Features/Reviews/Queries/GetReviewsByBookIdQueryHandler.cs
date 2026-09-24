using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Readora.Application.Common;
using Readora.Application.DTOs.Reviews;
using Readora.Application.Interfaces.Persistence;
using Readora.Domain.Entities;

namespace Readora.Application.Features.Reviews.Queries;

public class GetReviewsByBookIdQueryHandler : IRequestHandler<GetReviewsByBookIdQuery, Result<List<ReviewDto>>>
{
	private readonly IUnitOfWork _unitOfWork;

	public GetReviewsByBookIdQueryHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<Result<List<ReviewDto>>> Handle(GetReviewsByBookIdQuery request, CancellationToken cancellationToken)
	{
		ReviewsByBookIdSpecification spec = new ReviewsByBookIdSpecification(request.BookId);
		List<ReviewDto> dtos = (await _unitOfWork.Repository<Review>().ListAsync(spec, cancellationToken)).Select((Review r) => new ReviewDto
		{
			Id = r.Id,
			BookId = r.BookId,
			UserId = r.UserId,
			Rating = r.Rating,
			Comment = r.Comment,
			CreatedAt = r.CreatedAt
		}).ToList();
		return Result.Success(dtos);
	}
}
