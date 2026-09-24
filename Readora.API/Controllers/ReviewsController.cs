using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Readora.API.Extensions;
using Readora.Application.Common;
using Readora.Application.DTOs.Reviews;
using Readora.Application.Features.Reviews.Commands;
using Readora.Application.Features.Reviews.Queries;

namespace Readora.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ReviewsController : ControllerBase
{
	private readonly IMediator _mediator;

	public ReviewsController(IMediator mediator)
	{
		_mediator = mediator;
	}

	[HttpGet("book/{bookId}")]
	public async Task<IActionResult> GetReviewsByBookId(int bookId)
	{
		return (await ((ISender)_mediator).Send<Result<List<ReviewDto>>>((IRequest<Result<List<ReviewDto>>>)(object)new GetReviewsByBookIdQuery(bookId))).ToActionResult();
	}

	[HttpPost]
	[Authorize]
	public async Task<IActionResult> AddReview(AddReviewCommand command)
	{
		if (!User.TryGetUserId(out var userId)) return Unauthorized();
		if (command.UserId != userId)
		{
			return Forbid();
		}
		return (await _mediator.Send(command)).ToActionResult();
	}

	[HttpPut("{id}")]
	[Authorize]
	public async Task<IActionResult> UpdateReview(int id, UpdateReviewCommand command)
	{
		if (id != command.Id)
		{
			return BadRequest(new
			{
				Message = "Id mismatch."
			});
		}
		if (!User.TryGetUserId(out var userId)) return Unauthorized();
		if (command.UserId != userId)
		{
			return Forbid();
		}
		return (await _mediator.Send(command)).ToActionResult();
	}

	[HttpDelete("{id}")]
	[Authorize]
	public async Task<IActionResult> DeleteReview(int id)
	{
		if (!User.TryGetUserId(out var userId)) return Unauthorized();
		bool isAdmin = User.IsInRole("Admin");
		return (await _mediator.Send(new DeleteReviewCommand(id, userId, isAdmin))).ToActionResult();
	}
}
