using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Readora.Application.Common;
using Readora.Application.Interfaces.Persistence;
using Readora.Domain.Entities;

namespace Readora.Application.Features.Wishlists.Commands;

public class RemoveBookFromWishlistCommandHandler : IRequestHandler<RemoveBookFromWishlistCommand, Result>
{
	private readonly IUnitOfWork _unitOfWork;

	public RemoveBookFromWishlistCommandHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<Result> Handle(RemoveBookFromWishlistCommand request, CancellationToken cancellationToken)
	{
		WishlistWithItemsSpecification spec = new WishlistWithItemsSpecification(request.UserId);
		Wishlist wishlist = (await _unitOfWork.Repository<Wishlist>().ListAsync(spec, cancellationToken)).FirstOrDefault();
		if (wishlist == null)
		{
			return Result.Failure(Error.NotFound("Wishlist.NotFound", "Wishlist not found."));
		}
		WishlistItem item = wishlist.Items.FirstOrDefault((WishlistItem i) => i.BookId == request.BookId);
		if (item == null)
		{
			return Result.Failure(Error.NotFound("Wishlist.ItemNotFound", "Book is not in the wishlist."));
		}
		wishlist.Items.Remove(item);
		_unitOfWork.Repository<Wishlist>().Update(wishlist);
		await _unitOfWork.SaveChangesAsync(cancellationToken);
		return Result.Success();
	}
}
