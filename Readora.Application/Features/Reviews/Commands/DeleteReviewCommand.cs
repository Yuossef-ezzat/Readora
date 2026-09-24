using MediatR;
using Readora.Application.Common;

namespace Readora.Application.Features.Reviews.Commands;

public record DeleteReviewCommand(int Id, int UserId, bool IsAdmin = false) : IRequest<Result>, IBaseRequest;
