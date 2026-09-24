using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Readora.API.Extensions;
using Readora.Application.Common;
using Readora.Application.DTOs.BookCopies;
using Readora.Application.Features.BookCopies.Commands;
using Readora.Application.Features.BookCopies.Queries;

namespace Readora.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BookCopiesController : ControllerBase
{
	private readonly IMediator _mediator;

	public BookCopiesController(IMediator mediator)
	{
		_mediator = mediator;
	}

	[HttpGet("{id}")]
	public async Task<IActionResult> GetBookCopyById(int id)
	{
		return (await _mediator.Send(new GetBookCopyByIdQuery(id))).ToActionResult();
	}

	[HttpGet("book/{bookId}")]
	public async Task<IActionResult> GetBookCopiesByBookId(int bookId)
	{
		return (await _mediator.Send(new GetBookCopiesByBookIdQuery(bookId))).ToActionResult();
	}

	[HttpPost]
	[Authorize(Roles = "Admin,Librarian")]
	public async Task<IActionResult> CreateBookCopy(CreateBookCopyCommand command)
	{
		return (await _mediator.Send(command)).ToActionResult();
	}

	[HttpPut("{id}")]
	[Authorize(Roles = "Admin,Librarian")]
	public async Task<IActionResult> UpdateBookCopy(int id, UpdateBookCopyCommand command)
	{
		if (id != command.Id)
		{
			return BadRequest(new
			{
				Message = "Id mismatch."
			});
		}
		return (await _mediator.Send(command)).ToActionResult();
	}

	[HttpDelete("{id}")]
	[Authorize(Roles = "Admin")]
	public async Task<IActionResult> DeleteBookCopy(int id)
	{
		return (await _mediator.Send(new DeleteBookCopyCommand(id))).ToActionResult();
	}
}
