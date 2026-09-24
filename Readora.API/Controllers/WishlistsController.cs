using System.Security.Claims;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Readora.API.Extensions;
using Readora.Application.Common;
using Readora.Application.DTOs.Wishlists;
using Readora.Application.Features.Wishlists.Commands;
using Readora.Application.Features.Wishlists.Queries;

namespace Readora.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = "Member")]
public class WishlistsController : ControllerBase
{
	private readonly IMediator _mediator;

	public WishlistsController(IMediator mediator)
	{
		_mediator = mediator;
	}

	[HttpGet("my-wishlist")]
	public async Task<IActionResult> GetMyWishlist()
	{
		if (!User.TryGetUserId(out var userId)) return Unauthorized();
		return (await _mediator.Send(new GetWishlistByUserIdQuery(userId))).ToActionResult();
	}

	[HttpPost("add/{bookId}")]
	public async Task<IActionResult> AddToWishlist(int bookId)
	{
		if (!User.TryGetUserId(out var userId)) return Unauthorized();
		return (await _mediator.Send(new AddBookToWishlistCommand(userId, bookId))).ToActionResult();
	}

	[HttpDelete("remove/{bookId}")]
	public async Task<IActionResult> RemoveFromWishlist(int bookId)
	{
		if (!User.TryGetUserId(out var userId)) return Unauthorized();
		return (await _mediator.Send(new RemoveBookFromWishlistCommand(userId, bookId))).ToActionResult();
	}
}
