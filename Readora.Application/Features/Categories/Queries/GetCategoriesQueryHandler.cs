using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Readora.Application.Common;
using Readora.Application.DTOs.Categories;
using Readora.Application.Interfaces.Persistence;
using Readora.Domain.Entities;

namespace Readora.Application.Features.Categories.Queries;

public class GetCategoriesQueryHandler : IRequestHandler<GetCategoriesQuery, Result<PagedResult<CategoryDto>>>
{
	private readonly IUnitOfWork _unitOfWork;

	public GetCategoriesQueryHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<Result<PagedResult<CategoryDto>>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
	{
		CategoriesSpecification spec = new CategoriesSpecification(request.SearchTerm, request.Page, request.PageSize);
		CategoriesSpecification countSpec = new CategoriesSpecification(request.SearchTerm, null, null);
		IReadOnlyList<Category> categories = await _unitOfWork.Repository<Category>().ListAsync(spec, cancellationToken);
		int total = await _unitOfWork.Repository<Category>().CountAsync(countSpec, cancellationToken);
		List<CategoryDto> dtos = categories.Select((Category c) => new CategoryDto
		{
			Id = c.Id,
			Name = c.Name,
			Description = c.Description
		}).ToList();
		return Result.Success(new PagedResult<CategoryDto>(dtos, total, request.Page, request.PageSize));
	}
}
