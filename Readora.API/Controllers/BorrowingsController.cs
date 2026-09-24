using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Readora.API.Extensions;
using Readora.API.Filters;
using Readora.Application.Common;
using Readora.Application.DTOs.Borrowings;
using Readora.Application.Features.Borrowings.Commands;
using Readora.Application.Features.Borrowings.Queries;

namespace Readora.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
[EnableRateLimiting("fixed")]
public class BorrowingsController : ControllerBase
{
	private readonly IMediator _mediator;

	public BorrowingsController(IMediator mediator)
	{
		_mediator = mediator;
	}

	[HttpGet("{id}")]
	[Authorize(Roles = "Admin,Librarian")]
	public async Task<IActionResult> GetBorrowingById(int id)
	{
		return (await _mediator.Send(new GetBorrowingByIdQuery(id))).ToActionResult();
	}

	[HttpGet("my-borrowings")]
	public async Task<IActionResult> GetMyBorrowings()
	{
		if (!User.TryGetUserId(out var userId)) return Unauthorized();
		return (await _mediator.Send(new GetBorrowingsByUserIdQuery(userId))).ToActionResult();
	}

	[HttpPost("borrow/{bookId}")]
	[Idempotent]
	[Authorize(Roles = "Member")]
	public async Task<IActionResult> BorrowBook(int bookId)
	{
		if (!User.TryGetUserId(out var userId)) return Unauthorized();
		return (await _mediator.Send(new BorrowBookCommand(userId, bookId))).ToActionResult();
	}

	[HttpPost("return/{borrowingId}")]
	[Idempotent]
	[Authorize(Roles = "Admin,Librarian")]
	public async Task<IActionResult> ReturnBook(int borrowingId)
	{
		return (await _mediator.Send(new ReturnBookCommand(borrowingId))).ToActionResult();
	}
}
