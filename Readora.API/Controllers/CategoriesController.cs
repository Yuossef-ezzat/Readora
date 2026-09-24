using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Readora.API.Extensions;
using Readora.Application.Common;
using Readora.Application.DTOs.Categories;
using Readora.Application.Features.Categories.Commands;
using Readora.Application.Features.Categories.Queries;

namespace Readora.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoriesController : ControllerBase
{
	private readonly IMediator _mediator;

	public CategoriesController(IMediator mediator)
	{
		_mediator = mediator;
	}

	[HttpGet("{id}")]
	public async Task<IActionResult> GetCategoryById(int id)
	{
		return (await _mediator.Send(new GetCategoryByIdQuery(id))).ToActionResult();
	}

	[HttpGet]
	public async Task<IActionResult> GetCategories([FromQuery] string? searchTerm, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
	{
		return (await _mediator.Send(new GetCategoriesQuery(searchTerm, page, pageSize))).ToActionResult();
	}

	[HttpPost]
	[Authorize(Roles = "Admin,Librarian")]
	public async Task<IActionResult> CreateCategory(CreateCategoryCommand command)
	{
		return (await _mediator.Send(command)).ToActionResult();
	}

	[HttpPut("{id}")]
	[Authorize(Roles = "Admin,Librarian")]
	public async Task<IActionResult> UpdateCategory(int id, UpdateCategoryCommand command)
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
	public async Task<IActionResult> DeleteCategory(int id)
	{
		return (await _mediator.Send(new DeleteCategoryCommand(id))).ToActionResult();
	}
}
