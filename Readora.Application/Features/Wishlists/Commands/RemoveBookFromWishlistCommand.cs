using MediatR;
using Readora.Application.Common;

namespace Readora.Application.Features.Wishlists.Commands;

public record RemoveBookFromWishlistCommand(int UserId, int BookId) : IRequest<Result>, IBaseRequest;
