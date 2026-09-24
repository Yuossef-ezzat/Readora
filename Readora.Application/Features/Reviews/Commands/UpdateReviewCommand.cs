using MediatR;
using Readora.Application.Common;

namespace Readora.Application.Features.Reviews.Commands;

public record UpdateReviewCommand(int Id, int UserId, int Rating, string? Comment) : IRequest<Result>, IBaseRequest;
