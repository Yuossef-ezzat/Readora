using System.Collections.Generic;
using MediatR;
using Readora.Application.Common;
using Readora.Application.DTOs.Reviews;

namespace Readora.Application.Features.Reviews.Queries;

public record GetReviewsByBookIdQuery(int BookId) : IRequest<Result<List<ReviewDto>>>, IBaseRequest;
