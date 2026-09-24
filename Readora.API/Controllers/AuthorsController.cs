using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Readora.API.Extensions;
using Readora.Application.Common;
using Readora.Application.DTOs.Authors;
using Readora.Application.Features.Authors.Commands;
using Readora.Application.Features.Authors.Queries;

namespace Readora.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthorsController : ControllerBase
{
	private readonly IMediator _mediator;

	public AuthorsController(IMediator mediator)
	{
		_mediator = mediator;
	}

	[HttpGet("{id}")]
	public async Task<IActionResult> GetAuthorById(int id)
	{
		return (await _mediator.Send(new GetAuthorByIdQuery(id))).ToActionResult();
	}

	[HttpGet]
	public async Task<IActionResult> GetAuthors([FromQuery] string? searchTerm, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
	{
		return (await _mediator.Send(new GetAuthorsQuery(searchTerm, page, pageSize))).ToActionResult();
	}

	[HttpPost]
	[Authorize(Roles = "Admin,Librarian")]
	public async Task<IActionResult> CreateAuthor(CreateAuthorCommand command)
	{
		return (await _mediator.Send(command)).ToActionResult();
	}

	[HttpPut("{id}")]
	[Authorize(Roles = "Admin,Librarian")]
	public async Task<IActionResult> UpdateAuthor(int id, UpdateAuthorCommand command)
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
	public async Task<IActionResult> DeleteAuthor(int id)
	{
		return (await _mediator.Send(new DeleteAuthorCommand(id))).ToActionResult();
	}
}
