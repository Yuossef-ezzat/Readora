using MediatR;
using Readora.Application.Common;
using Readora.Application.DTOs.Wishlists;

namespace Readora.Application.Features.Wishlists.Queries;

public record GetWishlistByUserIdQuery(int UserId) : IRequest<Result<WishlistDto>>, IBaseRequest;
