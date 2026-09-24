using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Readora.Application.Common;
using Readora.Application.Interfaces.Persistence;
using Readora.Domain.Entities;

namespace Readora.Application.Features.Wishlists.Commands;

public class AddBookToWishlistCommandHandler : IRequestHandler<AddBookToWishlistCommand, Result>
{
	private readonly IUnitOfWork _unitOfWork;

	public AddBookToWishlistCommandHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<Result> Handle(AddBookToWishlistCommand request, CancellationToken cancellationToken)
	{
		WishlistWithItemsSpecification spec = new WishlistWithItemsSpecification(request.UserId);
		Wishlist wishlist = (await _unitOfWork.Repository<Wishlist>().ListAsync(spec, cancellationToken)).FirstOrDefault();
		if (wishlist == null)
		{
			wishlist = new Wishlist
			{
				UserId = request.UserId
			};
			await _unitOfWork.Repository<Wishlist>().AddAsync(wishlist, cancellationToken);
		}
		if (wishlist.Items.Any((WishlistItem i) => i.BookId == request.BookId))
		{
			return Result.Failure(Error.Validation("Wishlist.AlreadyExists", "Book is already in the wishlist."));
		}
		if (await _unitOfWork.Repository<Book>().GetByIdAsync(request.BookId, cancellationToken) == null)
		{
			return Result.Failure(Error.NotFound("Books.NotFound", "Book not found."));
		}
		wishlist.Items.Add(new WishlistItem
		{
			BookId = request.BookId,
			AddedAt = DateTime.UtcNow
		});
		_unitOfWork.Repository<Wishlist>().Update(wishlist);
		await _unitOfWork.SaveChangesAsync(cancellationToken);
		return Result.Success();
	}
}
