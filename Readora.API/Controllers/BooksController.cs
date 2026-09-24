using System.IO;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Readora.API.Extensions;
using Readora.Application.Common;
using Readora.Application.DTOs.Books;
using Readora.Application.Features.Books.Commands;
using Readora.Application.Features.Books.Queries;

namespace Readora.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[EnableRateLimiting("fixed")]
public class BooksController : ControllerBase
{
	private readonly IMediator _mediator;

	public BooksController(IMediator mediator)
	{
		_mediator = mediator;
	}

	[HttpGet("{id}")]
	public async Task<IActionResult> GetBookById(int id)
	{
		return (await _mediator.Send(new GetBookByIdQuery(id))).ToActionResult();
	}

	[HttpGet]
	public async Task<IActionResult> GetBooks([FromQuery] string? searchTerm, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
	{
		return (await _mediator.Send(new GetBooksQuery(searchTerm, page, pageSize))).ToActionResult();
	}

	[HttpPost]
	[Authorize(Roles = "Admin,Librarian")]
	public async Task<IActionResult> CreateBook(CreateBookCommand command)
	{
		return (await _mediator.Send(command)).ToActionResult();
	}

	[HttpPut("{id}")]
	[Authorize(Roles = "Admin,Librarian")]
	public async Task<IActionResult> UpdateBook(int id, UpdateBookCommand command)
	{
		if (id != command.Id)
		{
			return BadRequest(new
			{
				Message = "Id in route must match Id in body."
			});
		}
		return (await _mediator.Send(command)).ToActionResult();
	}

	[HttpDelete("{id}")]
	[Authorize(Roles = "Admin")]
	public async Task<IActionResult> DeleteBook(int id)
	{
		return (await _mediator.Send(new DeleteBookCommand(id))).ToActionResult();
	}

	[HttpPost("{id}/upload-cover")]
	[Authorize(Roles = "Admin,Librarian")]
	public async Task<IActionResult> UploadCoverImage(int id, IFormFile file)
	{
		using Stream stream = file.OpenReadStream();
		return (await _mediator.Send(new UploadBookFileCommand(id, stream, file.FileName, file.ContentType, IsCoverImage: true))).ToActionResult();
	}

	[HttpPost("{id}/upload-pdf")]
	[Authorize(Roles = "Admin,Librarian")]
	public async Task<IActionResult> UploadPdf(int id, IFormFile file)
	{
		using Stream stream = file.OpenReadStream();
		return (await _mediator.Send(new UploadBookFileCommand(id, stream, file.FileName, file.ContentType, IsCoverImage: false))).ToActionResult();
	}
}
