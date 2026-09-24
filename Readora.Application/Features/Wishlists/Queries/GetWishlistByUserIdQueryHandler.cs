using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Readora.Application.Common;
using Readora.Application.DTOs.Wishlists;
using Readora.Application.Features.Wishlists.Commands;
using Readora.Application.Interfaces.Persistence;
using Readora.Domain.Entities;

namespace Readora.Application.Features.Wishlists.Queries;

public class GetWishlistByUserIdQueryHandler : IRequestHandler<GetWishlistByUserIdQuery, Result<WishlistDto>>
{
	private readonly IUnitOfWork _unitOfWork;

	public GetWishlistByUserIdQueryHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<Result<WishlistDto>> Handle(GetWishlistByUserIdQuery request, CancellationToken cancellationToken)
	{
		WishlistWithItemsSpecification spec = new WishlistWithItemsSpecification(request.UserId);
		Wishlist wishlist = (await _unitOfWork.Repository<Wishlist>().ListAsync(spec, cancellationToken)).FirstOrDefault();
		if (wishlist == null)
		{
			return Result.Success(new WishlistDto
			{
				UserId = request.UserId,
				Items = new List<WishlistItemDto>()
			});
		}
		WishlistDto dto = new WishlistDto
		{
			Id = wishlist.Id,
			UserId = wishlist.UserId,
			Items = wishlist.Items.Select((WishlistItem i) => new WishlistItemDto
			{
				Id = i.Id,
				BookId = i.BookId,
				AddedAt = i.AddedAt
			}).ToList()
		};
		return Result.Success(dto);
	}
}
