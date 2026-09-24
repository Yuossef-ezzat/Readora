using MediatR;
using Readora.Application.Common;

namespace Readora.Application.Features.Reviews.Commands;

public record AddReviewCommand(int BookId, int UserId, int Rating, string? Comment) : IRequest<Result<int>>, IBaseRequest;
